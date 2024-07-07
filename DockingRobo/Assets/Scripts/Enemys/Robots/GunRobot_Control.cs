using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRobot_Control : MonoBehaviour
{
    GameObject Muzzle;
    GameObject Muzzle2;
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    float bullet_serialspeed = 0f;
    float bullet_stoptime = 0f;
    bool lockon_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_left/Muzzle").gameObject;
        Muzzle2 = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.4f && bullet_stoptime <= 1)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);

                GameObject bullet_Instance2 = Instantiate(bullet, Muzzle2.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle2.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
            else if (bullet_stoptime >= 2)
            {
                bullet_stoptime = 0;
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
