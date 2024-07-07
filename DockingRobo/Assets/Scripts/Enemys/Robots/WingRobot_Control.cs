using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle;
    public GameObject bullet;
    GameObject Wing_right;
    GameObject Wing_left;
    float bullet_serialspeed = 2.0f;
    bool lockon_flag = false;
    bool firing_flag = true;
    bool leftrotation_flag = true;
    float speed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Wing_right = transform.Find("Backpack/Wing/Wing_right").gameObject;
        Wing_left = transform.Find("Backpack/Wing/Wing_left").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 2.0f)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_serialspeed = 0;
                firing_flag = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
        if (!firing_flag)
        {
            rb.AddForce(transform.up * 8000, ForceMode.Impulse);
            firing_flag = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
}
