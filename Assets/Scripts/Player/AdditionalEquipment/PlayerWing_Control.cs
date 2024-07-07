using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerWing_Control : MonoBehaviour
{
    int stamina = 10;   //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 3f; //行動後の遅延時間
    Slider slider;  //耐久値用のバー
    GameObject Player;  //プレイヤーオブジェクト
    Rigidbody Player_Rigidbody; //プレイヤーがコンポーネントしているPlayer_Rigidbody
    GameObject Wing_right;  //右翼
    GameObject Wing_left;   //左翼
    bool leftrotation_flag = true;  //左回転するかのフラグ
    float speed = 1.0f; //動作速度
    bool castof_flag = false;   //このパーツをパージしたかのフラグ
    bool pusharmbutton_flag = false;    //アームボタンを押したかのフラグ
    bool pushheadbutton_flag = false;   //ヘッドボタンを押したかのフラグ

    // Start is called before the first frame update
    void Start()    //ウィングパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Player = transform.root.gameObject;
        Player_Rigidbody = Player.GetComponent<Rigidbody>();
        Wing_right = transform.Find("Wing_right").gameObject;
        Wing_left = transform.Find("Wing_left").gameObject;

        EventTrigger.Entry entry = new EventTrigger.Entry();    //アームボタンの設定
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);

        EventTrigger.Entry entry_head = new EventTrigger.Entry();   //ヘッドボタンの設定
        entry_head.eventID = EventTriggerType.PointerDown;
        entry_head.callback.AddListener((x) => PushDown_HeadButton());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry_head = new EventTrigger.Entry();
        entry_head.eventID = EventTriggerType.PointerUp;
        entry_head.callback.AddListener((x) => PushUp_HeadButton());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        serial_time += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushheadbutton_flag || Input.GetKey(KeyCode.A) || pusharmbutton_flag)    //ジャンプする処理
        {
            if (serial_time >= 2f)  //遅延時間
            {
                Player_Rigidbody.AddForce(transform.up * 8, ForceMode.Impulse);
                stamina--;
                serial_time = 0;
            }
        }

        if (stamina <= 0)   //耐久値が無くなった場合
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //耐久値用のバーの更新
    }

    private void FixedUpdate()
    {
        //プレイヤーの機動力の上昇
        if (transform.root.gameObject.GetComponent<Status_Control>().speed == transform.root.gameObject.GetComponent<Status_Control>().original_speed)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Speed(1);
        }
        if (!leftrotation_flag) //右回転する
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)  //左回転する
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
    }

    public void PushDown_ArmButton()    //アームボタンを押した場合
    {
        pusharmbutton_flag = true;
    }

    public void PushUp_ArmButton()  //アームボタンを離した場合
    {
        pusharmbutton_flag = false;
    }

    public void PushDown_HeadButton()   //ヘッドボタンを押した場合
    {
        pushheadbutton_flag = true;
    }

    public void PushUp_HeadButton() //ヘッドボタンを離した場合
    {
        pushheadbutton_flag = false;
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //このパーツがパージしていない場合
        {
            transform.root.gameObject.GetComponent<Status_Control>().Return_Speed();
            castof_flag = true;
        }
    }
}
