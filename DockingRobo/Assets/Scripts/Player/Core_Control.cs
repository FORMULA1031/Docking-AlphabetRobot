using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Core_Control : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    GameObject Arm_left;
    GameObject Arm_right;
    GameObject Head;
    GameObject Backpack;
    float bullet_serialspeed = 0;
    bool addarm_flag = false;
    bool addhead_flag = false;
    bool addback_flag = false;
    Text WeaponNumberArm_text;
    Text WeaponNumberHead_text;
    bool pusharmbutton_flag = false;
    Slider slider;
    Status_Control Status_Control;
    int add_power = 0;
    AudioSource AudioSource;
    public AudioClip mounting_se;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Head = transform.Find("Head").gameObject;
        Backpack = transform.Find("Backpack/Backpack_Weapons").gameObject;
        Arm_left = transform.Find("Arm_left/Cylinder (1)").gameObject;
        Arm_right = transform.Find("Arm_right/Cylinder (1)").gameObject;
        WeaponNumberArm_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        WeaponNumberHead_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Status_Control = gameObject.GetComponent<Status_Control>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Key_Outputs();
        if (!addarm_flag)
        {
            WeaponNumberArm_text.text = "-";
        }
        if (!addhead_flag)
        {
            WeaponNumberHead_text.text = "NONE";
        }
        if (!addback_flag)
        {
            slider.value = 0;
        }
        add_power = Status_Control.add_power;
    }

    void Key_Outputs()
    {
        if (!addarm_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            if(bullet_serialspeed >= 0.2f)
            if (Input.GetKey(KeyCode.A) || pusharmbutton_flag)
            {
                GameObject bullet_instance1 = Instantiate(bullet, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                GameObject bullet_instance2 = Instantiate(bullet, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                bullet_instance1.GetComponent<Bullet_Control>().Enhancement(add_power);
                bullet_instance2.GetComponent<Bullet_Control>().Enhancement(add_power);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                bullet_serialspeed = 0;
            }
        }
    }

    public  void PushDown_ArmButton()
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = true;
        }
    }

    public void PushUp_ArmButton()
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = false;
        }
    }

    public void AdditionalEquipment(string position, GameObject weapon , GameObject weapon2)
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (position)
        {
            case "arm":
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
            case "head":
                GameObject Add_HeadWeapon = Instantiate(weapon, Head.transform.position, Quaternion.identity);
                Add_HeadWeapon.transform.parent = Head.transform;
                addhead_flag = true;
                break;
            case "backpack":
                GameObject Add_BackpackWeapon = Instantiate(weapon, Backpack.transform.position, Quaternion.identity);
                Add_BackpackWeapon.transform.parent = Backpack.transform;
                addback_flag = true;
                break;
        }
    }

    public void CastOf(string position)
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
}
