using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerKnife_Control : MonoBehaviour
{
    public GameObject knife_effect; //生成する斬撃エフェクト
    GameObject Muzzle;  //斬撃エフェクトを生成する座標オブジェクト
    float bullet_serialspeed = 1f;  //斬撃エフェクトを生成する遅延時間
    bool action_flag = false;   //攻撃可能かのフラグ
    int bullets_number = 15;    //弾数
    Text WeaponNumber_text; //表示する攻撃回数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    GameObject Arm_left;    //プレイヤーの左腕
    bool pushbutton_flag = false;   //攻撃ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //ナイフパーツの追加処理
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
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Knife_Player_L(Clone)").gameObject;
        if (transform.parent.gameObject.transform.parent.gameObject.name == "Arm_right")
        {
            action_flag = true;
        }
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        if (action_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || pushbutton_flag) //攻撃処理
            {
                if (bullet_serialspeed >= 0.3f) //連射速度
                {
                    Instance_Effect();
                    bullet_serialspeed = 0f;
                    bullets_number--;
                }
            }
            Display_BulletsNumber();    //表示する攻撃の残り回数の更新
        }
        if (bullets_number <= 0)    //
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
    }

    void Instance_Effect()  //斬撃エフェクトの生成処理
    {
        GameObject Effect_Instance = Instantiate(knife_effect, Muzzle.transform.position, Quaternion.identity);
        Effect_Instance.GetComponent<PlayerKnifeEffect_Control>().Enhancement(add_power);
    }

    void Display_BulletsNumber()    //表示する攻撃の残り回数の更新
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
