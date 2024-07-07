using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatRobot_Control : MonoBehaviour
{
    GameObject Muzzle;
    public GameObject Hat;
    GameObject Hat_Instance;
    bool lockon_flag = false;
    float serial_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Head/Muzzle").gameObject;
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            serial_time += Time.deltaTime;
        }
        if (Hat_Instance.GetComponent<Hat_Control>().hit_flag)
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
        }
    }

    private void FixedUpdate()
    {
        if (!lockon_flag)
        {
            Hat_Instance.transform.position = Muzzle.transform.position;
        }

        if (lockon_flag)
        {
            if (serial_time >= 2 && serial_time < 3.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * 8f;
            }
            else if (serial_time >= 3.0f && serial_time < 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * -8f;
            }
            else if(serial_time >= 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Hat_Instance.transform.position = Muzzle.transform.position;
                serial_time = 0;
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

    public void OnDestroy()
    {
        Destroy(Hat_Instance);
    }
}
