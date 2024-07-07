using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHat_Control : MonoBehaviour
{
    GameObject Muzzle;  //生成するハットの座標オブジェクト
    public GameObject Hat;  //生成するハット
    GameObject Hat_Instance;    //生成したハット
    float serial_time = 0.0f;   //ハット飛ばしの遅延時間
    int bullets_number = 20;    //ハット飛ばしの残り回数
    Text WeaponNumber_text; //表示するハット飛ばしの残り回数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    Status_Control Status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Controlスクリプト
    bool pushbutton_flag = false;   //攻撃ボタンを押しているかのフラグ
    bool atack_flag = false;    //攻撃中かのフラグ
    int add_power = 0;  //強化する攻撃力の値
    Vector3 hat_position;   //ハットの正位置

    // Start is called before the first frame update
    void Start()    //ハットパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
        Transform[] brotrans = new Transform[parent.childCount];
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Muzzle = transform.Find("Muzzle").gameObject;
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
        Player = transform.root.gameObject;
        Status_Control = Player.GetComponent<Status_Control>();
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        if (add_power != 0)
        {
            Hat_Instance.GetComponent<Hat_Control>().Enhancement(add_power);
        }
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //攻撃処理
        {
            atack_flag = true;
        }
        if (atack_flag)
        {
            serial_time += Time.deltaTime;
        }
        if(transform.position.z > Hat_Instance.transform.position.z && atack_flag)
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            bullets_number--;
            atack_flag = false;
        }
        if (bullets_number <= 0)    //残りハット飛ばしの回数が無くなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示するハット飛ばしの残り回数の更新
    }

    private void FixedUpdate()
    {
        if (atack_flag) //攻撃中の処理
        {
            if (serial_time < 1.0f) //前へ飛ばす
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * 3f * Status_Control.speed;
            }
            else if (serial_time >= 1.0f && serial_time < 2.0f) //プレイヤーに戻る
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * -3f * Status_Control.speed;
            }
            else if (serial_time >= 2.0f && serial_time < 3.0f) //ハットを正位置に移動
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f);
                Hat_Instance.transform.position = hat_position;
            }
            else if (serial_time >= 3f) //攻撃モーション終了
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f);
                Hat_Instance.transform.position = hat_position;
                serial_time = 0;
                bullets_number--;
                atack_flag = false;
            }
            if (transform.position.z + 1.5f < Hat_Instance.transform.position.z && Hat_Instance.transform.position.y > 1f)  //正位置範囲にいない場合
            {
                Hat_Instance.transform.position = 
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y - 0.4f, Hat_Instance.transform.position.z);
                Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
            }
            else if (transform.position.z + 1.5f > Hat_Instance.transform.position.z && Hat_Instance.transform.position.y < 1.4f)   //正位置範囲にいた場合
            {
                Hat_Instance.transform.position =
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y + 0.4f, Hat_Instance.transform.position.z);
            }
        }
        else
        {
            hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f * Time.deltaTime);
            Hat_Instance.transform.position = hat_position;
        }
    }

    void Display_BulletsNumber()    //表示するハット飛ばしの残り回数の更新
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    public void PushDown_Button()   //ボタンを押した場合
    {
        pushbutton_flag = true;
    }

    public void PushUp_Button() //ボタンを離した場合
    {
        pushbutton_flag = false;
    }

    private void OnDestroy()
    {
        Destroy(Hat_Instance);
    }
}
