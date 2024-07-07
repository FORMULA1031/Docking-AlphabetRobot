using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerNeedle_Control : MonoBehaviour
{
    public GameObject bullet;   //生成する針
    public GameObject cannonstreet_effect;  //針を発射後の煙エフェクト
    GameObject Muzzle;  //針を生成する座標オブジェクト
    float bullet_serialspeed = 0.8f;    //連射速度
    int bullets_number = 10;    //弾数
    Text WeaponNumber_text; //表示する弾数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    bool pushbutton_flag = false;   //ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //ニードルパーツの追加処理
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
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || pushbutton_flag) //攻撃処理
        {
            if (bullet_serialspeed >= 0.8f) //連射速度
            {
                Instance_Bullets();
                bullet_serialspeed = 0.0f;
                bullets_number--;
            }
        }
        if (bullets_number <= 0)    //弾数が無くなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する弾数テキストの更新
    }

    void Instance_Bullets() //針の生成処理
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.03f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
        bullet_Instance.GetComponent<Bullet_Control>().Player_flag(true);
        bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
        bullet_Instance.GetComponent<Bullet_Control>().Enhancement(add_power);
        Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
    }

    void Display_BulletsNumber()    //表示する弾数テキストの更新
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
}
