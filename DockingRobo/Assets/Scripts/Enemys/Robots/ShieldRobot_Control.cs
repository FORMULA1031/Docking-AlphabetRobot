using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle;
    GameObject Muzzle_Shield;
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    public GameObject ShieldBarrier;
    GameObject Shield_Instance;
    float bullet_serialspeed = 0f;
    float bullet_stoptime = 0f;
    bool lockon_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Muzzle_Shield = transform.Find("Backpack/Shield/Muzzle").gameObject;
        Shield_Instance = Instantiate(ShieldBarrier, Muzzle_Shield.transform.position, transform.rotation);
        Vector3 rotation = Shield_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Shield_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.2f && bullet_stoptime <= 1)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
            else if (bullet_stoptime >= 2)
            {
                bullet_stoptime = 0;
            }
        }
        Shield_Instance.transform.position = Muzzle_Shield.transform.position;
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
