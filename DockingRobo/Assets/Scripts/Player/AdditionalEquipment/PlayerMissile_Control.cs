using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMissile_Control : MonoBehaviour
{
    int stamina = 10;
    int stamina_max;
    float serial_time = 0;
    Slider slider;
    GameObject Muzzle_left;
    GameObject Muzzle_right;
    public GameObject missile;
    public GameObject cannonstreet_effect;
    bool pusharmbutton_flag = false;
    bool pushheadbutton_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = gameObject.transform.parent;
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

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);

        EventTrigger.Entry entry_head = new EventTrigger.Entry();
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
        if (Input.GetKey(KeyCode.S) || pushheadbutton_flag || Input.GetKey(KeyCode.A) || pusharmbutton_flag)
        {
            if (serial_time >= 2f)
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

        if (stamina <= 0)
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max;
    }

    public void PushDown_ArmButton()
    {
        pusharmbutton_flag = true;
    }

    public void PushUp_ArmButton()
    {
        pusharmbutton_flag = false;
    }

    public void PushDown_HeadButton()
    {
        pushheadbutton_flag = true;
    }

    public void PushUp_HeadButton()
    {
        pushheadbutton_flag = false;
    }
}
