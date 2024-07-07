using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerLaser_Control : MonoBehaviour
{
    public GameObject bullet;   //生成する弾
    GameObject Muzzle;  //生成する弾の座標オブジェクト
    int bullets_number = 3; //弾数
    float bullet_serialspeed = 1f;  //連射速度
    Text WeaponNumber_text; //表示する弾数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    bool pushbutton_flag = false;   //攻撃ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //レーザーパーツの追加処理
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
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //攻撃処理
        {
            if (bullet_serialspeed >= 1f)   //連射速度
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.x = 0.05f;
                muzzle_quaternion.y = 0;
                muzzle_quaternion.z = 0;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Player_flag(true);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_Instance.GetComponent<CannonBullet_Control>().Enhancement(add_power);
                bullet_serialspeed = 0;
                bullets_number--;
            }
        }

        if (bullets_number <= 0)    //弾数が無くなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する弾数テキストの更新
    }

    void Display_BulletsNumber()    //表示する弾数テキストの更新
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    public void PushDown_Button()   //ボタンを押している場合
    {
        pushbutton_flag = true;
    }

    public void PushUp_Button() //ボタンを離した場合
    {
        pushbutton_flag = false;
    }
}
