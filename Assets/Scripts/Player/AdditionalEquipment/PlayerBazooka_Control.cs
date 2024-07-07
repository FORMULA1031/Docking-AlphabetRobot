using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBazooka_Control : MonoBehaviour
{
    public GameObject bullet;   //生成する弾
    GameObject Muzzle;  //生成する弾の座標オブジェクト
    float bullet_serialspeed = 1.5f;    //弾を生成する遅延時間
    int bullets_number = 3; //弾数
    Text WeaponNumber_text; //表示する残り弾数の更新
    GameObject Player;  //プレイヤーオブジェクト
    GameObject Arm_left;    //プレイヤーオブジェクトの左腕
    bool pushbutton_flag = false;   //攻撃ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Control
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //バズーカパーツの追加処理
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
        rotation.x -= 90;
        rotation.y -= 90;
        rotation.z -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Bazooka_Player_L(Clone)").gameObject;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || pushbutton_flag) //攻撃処理
        {
            if (bullet_serialspeed >= 1.5f)
            {
                Instance_Bullets(); //弾の生成
                bullet_serialspeed = 0.0f;
                bullets_number--;
            }
        }
        if (bullets_number <= 0)    //残り弾数が無くなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する残り弾数の更新
    }

    void Instance_Bullets() //弾の生成処理
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.03f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
        bullet_Instance.GetComponent<CannonBullet_Control>().Player_flag(true);
        bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
        bullet_Instance.GetComponent<CannonBullet_Control>().Enhancement(add_power);
    }

    void Display_BulletsNumber()    //表示する残り弾数の更新
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

    public void OnDestroy()
    {
        Destroy(Arm_left);
    }
}
