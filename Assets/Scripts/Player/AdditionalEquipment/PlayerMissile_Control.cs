using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMissile_Control : MonoBehaviour
{
    int stamina = 10;   //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間
    Slider slider;  //耐久値用のバー
    GameObject Muzzle_left; //左側のミサイル生成用の座標
    GameObject Muzzle_right;    //右側のミサイル生成用の座標
    public GameObject missile;  //生成するミサイル
    public GameObject cannonstreet_effect;  //ミサイルの発射後の煙エフェクト
    bool pusharmbutton_flag = false;    //アームボタンを押しているかのフラグ
    bool pushheadbutton_flag = false;   //ヘッドボタンを押しているかのフラグ

    // Start is called before the first frame update
    void Start()    //ミサイルパーツの追加処理
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
        Muzzle_left = transform.Find("Muzzle_left").gameObject;
        Muzzle_right = transform.Find("Muzzle_right").gameObject;

        EventTrigger.Entry entry = new EventTrigger.Entry();    //アームボタン設定
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);

        EventTrigger.Entry entry_head = new EventTrigger.Entry();   //ヘッドボタン設定
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
        if (Input.GetKey(KeyCode.S) || pushheadbutton_flag || Input.GetKey(KeyCode.A) || pusharmbutton_flag)    //攻撃処理
        {
            if (serial_time >= 1.5f)    //連射速度
            {
                Quaternion muzzle_quaternion = Muzzle_left.transform.rotation;
                muzzle_quaternion.y += 90;
                muzzle_quaternion.z += 50;
                GameObject Missile_Instance = Instantiate(missile, Muzzle_left.transform.position, muzzle_quaternion);
                Missile_Instance.GetComponent<Missile_Control>().Change_Power(120);
                Instantiate(cannonstreet_effect, Muzzle_left.transform.position, muzzle_quaternion);
                Missile_Instance = Instantiate(missile, Muzzle_right.transform.position, muzzle_quaternion);
                Missile_Instance.GetComponent<Missile_Control>().Change_Power(120);
                Instantiate(cannonstreet_effect, Muzzle_right.transform.position, muzzle_quaternion);
                serial_time = 0;
                stamina--;
            }
        }

        if (stamina <= 0)   //耐久値が無くなった場合
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //表示する残り耐久値の更新
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
}
