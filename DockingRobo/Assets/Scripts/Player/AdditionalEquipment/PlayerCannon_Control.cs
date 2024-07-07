using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCannon_Control : MonoBehaviour
{
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    GameObject Muzzle;
    int bullets_number = 10;
    float bullet_serialspeed = 1f;
    Text WeaponNumber_text;
    GameObject Player;
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
        add_power = Status_Control.add_power;
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushbutton_flag)
        {
            if (bullet_serialspeed >= 1f)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.x = 0.05f;
                muzzle_quaternion.y = 0;
                muzzle_quaternion.z = 0;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_Instance.GetComponent<CannonBullet_Control>().Enhancement(add_power);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
                bullets_number--;
            }
        }

        if (bullets_number <= 0)
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();
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
}
