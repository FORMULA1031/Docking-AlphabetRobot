using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRobot_Control : MonoBehaviour
{
    GameObject Muzzle;
    public GameObject bullet;
    public GameObject cannonstreet_effect;
    GameObject Player;
    float bullet_serialspeed = 0.5f;
    bool lockon_flag = false;
    Quaternion original_angle;

    // Start is called before the first frame update
    void Start()
    {
        original_angle = transform.rotation;
        Muzzle = transform.Find("Head/Muzzle").gameObject;
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && Player != null)
        {
            this.transform.LookAt(Player.transform);
            Vector3 rotation = this.transform.localRotation.eulerAngles;
            rotation.x = 0;
            rotation.y -= 90;
            rotation.x -= 5;
            transform.localRotation = Quaternion.Euler(rotation);
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 1.5 && transform.position.z > Player.transform.position.z + 3f)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(true);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
        }
        else
        {
            transform.rotation = original_angle;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
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
