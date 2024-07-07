using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHat_Control : MonoBehaviour
{
    GameObject Muzzle;
    public GameObject Hat;
    GameObject Hat_Instance;
    float serial_time = 0.0f;
    int bullets_number = 20;
    Text WeaponNumber_text;
    GameObject Player;
    Status_Control Status_Control;
    bool pushbutton_flag = false;
    bool atack_flag = false;
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
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
        Player = transform.root.gameObject;
        Status_Control = Player.GetComponent<Status_Control>();
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;
        if (add_power != 0)
        {
            Hat_Instance.GetComponent<Hat_Control>().Enhancement(add_power);
        }
        if (Input.GetKey(KeyCode.S) || pushbutton_flag)
        {
            atack_flag = true;
        }
        if (atack_flag)
        {
            serial_time += Time.deltaTime;
        }
        if(transform.position.z > Hat_Instance.transform.position.z && atack_flag)
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            bullets_number--;
            atack_flag = false;
        }
        if (bullets_number <= 0)
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();
    }

    private void FixedUpdate()
    {
        if (atack_flag)
        {
            if (serial_time < 1.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * 3f * Status_Control.speed;
            }
            else if (serial_time >= 1.0f && serial_time < 2.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * -3f * Status_Control.speed;
            }
            else if (serial_time >= 2.0f && serial_time < 3.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Hat_Instance.transform.position = Muzzle.transform.position;
            }
            else if (serial_time >= 3f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Hat_Instance.transform.position = Muzzle.transform.position;
                serial_time = 0;
                bullets_number--;
                atack_flag = false;
            }
            if (transform.position.z + 1.5f < Hat_Instance.transform.position.z && Hat_Instance.transform.position.y > 1f)
            {
                Hat_Instance.transform.position = 
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y - 0.4f, Hat_Instance.transform.position.z);
                Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
            }
            else if (transform.position.z + 1.5f > Hat_Instance.transform.position.z && Hat_Instance.transform.position.y < 1.4f)
            {
                Hat_Instance.transform.position =
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y + 0.4f, Hat_Instance.transform.position.z);
            }
        }
        else
        {
            Hat_Instance.transform.position = Muzzle.transform.position;
        }
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

    private void OnDestroy()
    {
        Destroy(Hat_Instance);
    }
}
