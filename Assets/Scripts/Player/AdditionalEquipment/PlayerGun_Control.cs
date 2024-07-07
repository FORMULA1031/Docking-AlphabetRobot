using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerGun_Control : MonoBehaviour
{
    public GameObject bullet;   //生成する弾
    public GameObject cannonstreet_effect;  //弾の発射後の煙エフェクト
    GameObject Muzzle;  //生成する弾の座標オブジェクト
    float bullet_serialspeed = 0.6f;    //弾を生成する遅延時間
    bool firstbullet_flag = false;  //弾の1発目を撃ったかのフラグ
    bool secondbullet_flag = false; //弾の2発目を撃ったかのフラグ
    int bullets_number = 20;    //弾の弾数
    Text WeaponNumber_text; //表示する弾数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    bool pushbutton_flag = false;   //攻撃ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //ピストルパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
        Transform[] brotrans = new Transform[parent.childCount];
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if(parent.GetChild(i).gameObject != gameObject)
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
        add_power = Status_Control.add_power;
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || pushbutton_flag) //攻撃処理
        {
            if (bullet_serialspeed >= 0.6f && !firstbullet_flag)    //1発目の弾を生成
            {
                Instance_Bullets();
                firstbullet_flag = true;
                bullet_serialspeed = 0.6f;
                bullets_number--;
            }
            else if (bullet_serialspeed >= 0.8f && !secondbullet_flag)  //2発目の弾を生成
            {
                Instance_Bullets();
                secondbullet_flag = true;
                bullets_number--;
            }
        }
        if (secondbullet_flag)
        {
            bullet_serialspeed = 0;
            firstbullet_flag = false;
            secondbullet_flag = false;
        }
        if(bullets_number <= 0) //残り弾数が無くなった場合
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
        bullet_Instance.GetComponent<Bullet_Control>().Player_flag(true);
        bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
        bullet_Instance.GetComponent<Bullet_Control>().Enhancement(add_power);
        Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
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
}
