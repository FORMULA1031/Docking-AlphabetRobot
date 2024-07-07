using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHammer_Control : MonoBehaviour
{
    public GameObject Hammer;
    GameObject Muzzle;
    GameObject Hammer_Instance;
    GameObject Hammer_grip;
    float revolution_time = 0.0f;
    int bullets_number = 20;
    Text WeaponNumber_text;
    GameObject Player;
    Status_Control Status_Control;
    int add_power = 0;
    float speed = -0.8f;
    bool leftrotation_flag = true;

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
        Muzzle = transform.Find("Hammer/Muzzle").gameObject;
        Hammer_grip = transform.Find("Hammer").gameObject;
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.x -= 90;
        rotation.y -= 90;
        rotation.z -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
        GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = false;
        Instance_Bullets();
    }

    // Update is called once per frame
    void Update()
    {
        Hammer_Instance.transform.position = Muzzle.transform.position;
        Hammer_Instance.transform.rotation = Muzzle.transform.rotation;
        Vector3 rotation_right = Hammer_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        Hammer_Instance.transform.localRotation = Quaternion.Euler(rotation_right);

        if (leftrotation_flag)
        {
            if (Hammer_grip.transform.localEulerAngles.z > 270 && Hammer_grip.transform.localEulerAngles.z <= 280)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        if (!leftrotation_flag)
        {
            if (Hammer_grip.transform.localEulerAngles.z >= 0 && Hammer_grip.transform.localEulerAngles.z < 100)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        Hammer_grip.transform.Rotate(new Vector3(0, 0, speed));

        add_power = Status_Control.add_power;
        if (add_power != 0)
        {
            Hammer_Instance.GetComponent<Hammer_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Hammer_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();
    }

    void Instance_Bullets()
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.00f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        Hammer_Instance = Instantiate(Hammer, Muzzle.transform.position, muzzle_quaternion);
    }

    void Display_BulletsNumber()
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (GameObject.Find("Canvas/ArmButton") != null)
        {
            GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = true;
        }
        Destroy(Hammer_Instance);
    }
}
