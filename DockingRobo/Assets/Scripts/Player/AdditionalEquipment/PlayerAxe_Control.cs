using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerAxe_Control : MonoBehaviour
{
    public GameObject axe_effect;
    GameObject Muzzle;
    float bullet_serialspeed = 1f;
    bool action_flag = false;
    int bullets_number = 5;
    Text WeaponNumber_text;
    GameObject Player;
    GameObject Arm_left;
    bool pushbutton_flag = false;
    Status_Control Status_Control;
    int add_power = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = gameObject.transform.parent;
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
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Axe_Player_L(Clone)").gameObject;
        if (transform.parent.gameObject.transform.parent.gameObject.name == "Arm_right")
        {
            action_flag = true;
        }
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (action_flag)
        {
            add_power = Status_Control.add_power;
            bullet_serialspeed += Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || pushbutton_flag)
            {
                if (bullet_serialspeed >= 1.0f)
                {
                    Instance_Effect();
                    bullet_serialspeed = 0f;
                    bullets_number--;
                }
            }
            Display_BulletsNumber();
        }
        if (bullets_number <= 0)
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
    }

    void Instance_Effect()
    {
        GameObject Effect_Instance = Instantiate(axe_effect, Muzzle.transform.position, Quaternion.identity);
        Effect_Instance.GetComponent<PlayerAxeEffect_Control>().Enhancement(add_power);
    }

    void Display_BulletsNumber()
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    public void PushDown_Button()
    {
        pushbutton_flag = true;
    }

    public void PushUp_Button()
    {
        pushbutton_flag = false;
    }

    public void OnDestroy()
    {
        Destroy(Arm_left);
    }
}
