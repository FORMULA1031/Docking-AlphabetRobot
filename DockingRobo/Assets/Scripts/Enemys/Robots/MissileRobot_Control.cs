using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle_left;
    GameObject Muzzle_right;
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    float bullet_serialspeed = 2f;
    bool lockon_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle_left = transform.Find("Backpack/Missile/Muzzle_left").gameObject;
        Muzzle_right = transform.Find("Backpack/Missile/Muzzle_right").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 3.0f)
            {
                Quaternion muzzle_quaternion = Muzzle_left.transform.rotation;
                muzzle_quaternion.y += 90;
                muzzle_quaternion.z -= 30;
                Instantiate(bullet, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(bullet, Muzzle_right.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_right.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
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
