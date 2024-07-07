using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject HammerR_grip;
    GameObject HammerL_grip;
    GameObject Hammer_right;
    GameObject Hammer_left;
    public GameObject Hammer;
    GameObject HammerR_Instance;
    GameObject HammerL_Instance;
    bool lockon_flag = false;
    bool move_flag = false;
    float speed = -0.8f;
    bool leftrotation_flag = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        HammerR_grip = transform.Find("Arm_right/Hammer").gameObject;
        HammerL_grip = transform.Find("Arm_left/Hammer").gameObject;
        Hammer_right = transform.Find("Arm_right/Hammer/Muzzle").gameObject;
        Hammer_left = transform.Find("Arm_left/Hammer/Muzzle").gameObject;

        Quaternion muzzle_quaternion = transform.rotation;
        HammerR_Instance = Instantiate(Hammer, Hammer_right.transform.position, muzzle_quaternion);
        Vector3 rotation_right = HammerR_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        HammerR_Instance.transform.localRotation = Quaternion.Euler(rotation_right);

        HammerL_Instance = Instantiate(Hammer, Hammer_left.transform.position, muzzle_quaternion);
        Vector3 rotation_left = HammerL_Instance.transform.localRotation.eulerAngles;
        rotation_left.z -= 90;
        HammerL_Instance.transform.localRotation = Quaternion.Euler(rotation_left);
    }

    // Update is called once per frame
    void Update()
    {
        HammerR_Instance.transform.position = Hammer_right.transform.position;
        HammerR_Instance.transform.rotation = Hammer_right.transform.rotation;
        Vector3 rotation_right = HammerR_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        HammerR_Instance.transform.localRotation = Quaternion.Euler(rotation_right);
        HammerL_Instance.transform.position = Hammer_left.transform.position;
        HammerL_Instance.transform.rotation = Hammer_left.transform.rotation;
        Vector3 rotation_left = HammerL_Instance.transform.localRotation.eulerAngles;
        rotation_left.z -= 90;
        HammerL_Instance.transform.localRotation = Quaternion.Euler(rotation_left);
        if (lockon_flag)
        {
            if (leftrotation_flag)
            {
                if (HammerR_grip.transform.localEulerAngles.z > 270 && HammerR_grip.transform.localEulerAngles.z <= 280)
                {
                    speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if (!leftrotation_flag)
            {
                if (HammerR_grip.transform.localEulerAngles.z >= 0 && HammerR_grip.transform.localEulerAngles.z < 100)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
            }
            HammerR_grip.transform.Rotate(new Vector3(0, 0, speed));
            HammerL_grip.transform.Rotate(new Vector3(0, 0, -speed));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-1);
                move_flag = true;
            }
            lockon_flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockon_flag = false;
        }
    }

    public void OnDestroy()
    {
        Destroy(HammerR_Instance);
        Destroy(HammerL_Instance);
    }
}
