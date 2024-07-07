using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Core_Control : MonoBehaviour
{
    public GameObject bullet;   //生成する弾
    public GameObject cannonstreet_effect;  //弾の発射後の煙エフェクト
    GameObject Arm_left;    //自オブジェクトの左腕
    GameObject Arm_right;   //自オブジェクトの右腕
    GameObject Head;    //自オブジェクトの頭
    GameObject Backpack;    //自オブジェクトのバックパック
    bool addarm_flag = false;   //追加アームパーツを装備しているかのフラグ
    bool addhead_flag = false;  //追加ヘッドパーツを装備しているかのフラグ
    bool addback_flag = false;  //追加バックパックパーツを装備しているかのフラグ
    Text WeaponNumberArm_text;  //表示するアームパーツ用の弾数テキスト
    Text WeaponNumberHead_text; //表示するヘッドパーツ用の弾数テキスト
    bool pusharmbutton_flag = false;    //アームボタンを押したかのフラグ
    Slider slider;  //追加バックパックパーツ用の耐久値ゲージ
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力
    AudioSource AudioSource;    //自オブジェクト用のAudioSource
    public AudioClip mounting_se;   //追加パーツの装着音
    public GameObject A_left;   //アックスパーツの左腕
    public GameObject A_right;  //アックスパーツの右腕
    public GameObject B_left;   //バズーカパーツの左腕
    public GameObject B_right;  //バズーカパーツの右腕
    public GameObject C;    //キャノンパーツ
    public GameObject D;    //ドリルパーツ
    public GameObject E;    //エンハンスメントパーツ
    public GameObject F;    //ファイヤーパーツ
    public GameObject G_left;   //ガトリングパーツの左腕
    public GameObject G_right;  //ガトリングパーツの右腕
    public GameObject H_left;   //ハンマーパーツの左腕
    public GameObject H_right;  //ハンマーパーツの右腕
    public GameObject I;    //インビジブルパーツ
    public GameObject J;    //ジェットパーツ
    public GameObject K_left;   //ナイフパーツの左腕
    public GameObject K_right;  //ナイフパーツの右腕
    public GameObject L;    //レーザーパーツ
    public GameObject M;    //ミサイルパーツ
    public GameObject N;    //ニードルパーツ
    public GameObject O;    //ハット―パーツ
    public GameObject P;    //ピストルパーツ
    public GameObject Q;    //クアンタムパーツ
    public GameObject R;    //リペアパーツ
    public GameObject S;    //シールドパーツ
    public GameObject T;    //テールパーツ
    public GameObject U;    //ユーズアリーパーツ
    public GameObject V;    //バリエーションパーツ
    public GameObject W;    //ウィングパーツ
    public GameObject X;    //エクストラパーツ
    public GameObject Y;    //エクストラパーツ

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.Find("Head").gameObject;
        Backpack = transform.Find("Backpack/Backpack_Weapons").gameObject;
        Arm_left = transform.Find("Arm_left/Cylinder (1)").gameObject;
        Arm_right = transform.Find("Arm_right/Cylinder (1)").gameObject;
        WeaponNumberArm_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        WeaponNumberHead_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());    //アームボタン設定
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Status_Control = gameObject.GetComponent<Status_Control>();
        AudioSource = GetComponent<AudioSource>();
        AddPart_load(); //設定していた追加パーツの装着
    }

    // Update is called once per frame
    void Update()
    {
        Key_Outputs();  //プレイヤー操作処理
        if (!addarm_flag)   //初期アーム装備の弾数テキスト
        {
            WeaponNumberArm_text.text = "-";
        }
        if (!addhead_flag)  //初期ヘッド装備の弾数テキスト
        {
            WeaponNumberHead_text.text = "NONE";
        }
        if (!addback_flag)  //初期バックパック装備の弾数テキスト
        {
            slider.value = 0;
        }
        add_power = Status_Control.add_power;   //強化する攻撃力の更新
    }

    private void FixedUpdate()
    {
        if(transform.position.y < 0.7f) //自オブジェクトの移動処理
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0.701f, transform.position.z);
        }
    }

    void Key_Outputs()  //プレイヤー操作処理
    {
        if (!addarm_flag)   //初期装備のアーム攻撃処理
        {
            if (Input.GetKey(KeyCode.A) || pusharmbutton_flag)
            {
                GameObject bullet_instance1 = Instantiate(bullet, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                GameObject bullet_instance2 = Instantiate(bullet, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                bullet_instance1.GetComponent<Bullet_Control>().Enhancement(add_power);
                bullet_instance2.GetComponent<Bullet_Control>().Enhancement(add_power);
                bullet_instance1.GetComponent<Bullet_Control>().Player_flag(true);
                bullet_instance2.GetComponent<Bullet_Control>().Player_flag(true);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
            }
        }
    }

    public  void PushDown_ArmButton()   //アームボタンを押した場合
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = true;
        }
    }

    public void PushUp_ArmButton()  //アームボタンを離した場合
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = false;
        }
    }

    public void AdditionalEquipment(string position, GameObject weapon , GameObject weapon2)    //追加装備の装着処理
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (position)
        {
            case "arm": //アーム装備の場合
                GameObject Add_ArmWeapon_L = Instantiate(weapon, Arm_left.transform.position, Quaternion.identity);
                Add_ArmWeapon_L.transform.parent = Arm_left.transform;
                Vector3 rotation = Add_ArmWeapon_L.transform.localRotation.eulerAngles;
                if (weapon != weapon2)
                {
                    rotation.x -= 90;
                    rotation.y -= 90;
                    rotation.z -= 90;
                }
                Add_ArmWeapon_L.transform.localRotation = Quaternion.Euler(rotation);
                GameObject Add_ArmWeapon_R = Instantiate(weapon2, Arm_right.transform.position, Quaternion.identity);
                Add_ArmWeapon_R.transform.parent = Arm_right.transform;
                addarm_flag = true;
                break;
            case "head":    //ヘッド装備の場合
                GameObject Add_HeadWeapon = Instantiate(weapon, Head.transform.position, Quaternion.identity);
                Add_HeadWeapon.transform.parent = Head.transform;
                addhead_flag = true;
                break;
            case "backpack":    //バックパック装備の場合
                GameObject Add_BackpackWeapon = Instantiate(weapon, Backpack.transform.position, Quaternion.identity);
                Add_BackpackWeapon.transform.parent = Backpack.transform;
                addback_flag = true;
                break;
        }
    }

    public void CastOf(string position) //追加装備のパージ
    {
        switch (position)
        {
            case "arm":
                addarm_flag = false;
                break;
            case "head":
                addhead_flag = false;
                break;
            case "backpack":
                addback_flag = false;
                break;
        }
    }

    private void AddPart_load() //設定していた追加装備の装着処理
    {
        switch (Player_Settings.head_setting)   //ヘッド装備
        {
            case "C":
                AdditionalEquipment("head", C, null);
                break;
            case "D":
                AdditionalEquipment("head", D, null);
                break;
            case "F":
                AdditionalEquipment("head", F, null);
                break;
            case "O":
                AdditionalEquipment("head", O, null);
                break;
            case "L":
                AdditionalEquipment("head", L, null);
                break;
            case "R":
                AdditionalEquipment("head", R, null);
                break;
        }

        switch (Player_Settings.arm_setting)    //アーム装備
        {
            case "A":
                AdditionalEquipment("arm", A_left, A_right);
                break;
            case "B":
                AdditionalEquipment("arm", B_left, B_right);
                break;
            case "G":
                AdditionalEquipment("arm", G_left, G_right);
                break;
            case "K":
                AdditionalEquipment("arm", K_left, K_right);
                break;
            case "P":
                AdditionalEquipment("arm", P, P);
                break;
            case "H":
                AdditionalEquipment("arm", H_left, H_right);
                break;
            case "N":
                AdditionalEquipment("arm", N, N);
                break;
        }

        switch (Player_Settings.backpack_setting)   //バックパック装備
        {
            case "E":
                AdditionalEquipment("backpack", E, null);
                break;
            case "I":
                AdditionalEquipment("backpack", I, null);
                break;
            case "J":
                AdditionalEquipment("backpack", J, null);
                break;
            case "M":
                AdditionalEquipment("backpack", M, null);
                break;
            case "S":
                AdditionalEquipment("backpack", S, null);
                break;
            case "T":
                AdditionalEquipment("backpack", T, null);
                break;
            case "W":
                AdditionalEquipment("backpack", W, null);
                break;
        }
    }
}
