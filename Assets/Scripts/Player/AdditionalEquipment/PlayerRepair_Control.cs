using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerRepair_Control : MonoBehaviour
{
    int bullets_number = 2; //使用回数
    float bullet_serialspeed = 1f;  //使用後による遅延時間
    Text WeaponNumber_text; //表示する残り使用回数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    bool pushbutton_flag = false;   //ボタンを押しているかのフラグ
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト

    // Start is called before the first frame update
    void Start()    //リペアパーツの追加処理
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
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //リペアの使用処理
        {
            if (bullet_serialspeed >= 1f)   //遅延時間
            {
                Status_Control.Stamina_Repair(10);
                bullet_serialspeed = 0;
                bullets_number--;
            }
        }

        if (bullets_number <= 0)    //残り使用回数が無くなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する残り使用回数テキストの更新
    }

    void Display_BulletsNumber()    //表示する残り使用回数テキストの更新
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
