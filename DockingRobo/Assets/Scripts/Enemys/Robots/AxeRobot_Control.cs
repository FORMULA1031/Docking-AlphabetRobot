using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle;
    public GameObject Effect;
    GameObject Effect_Instance;
    bool lockon_flag = false;
    bool move_flag = false;
    float atack_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)
        {
            atack_time += Time.deltaTime;
            if (atack_time >= 1)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
                Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
                rotation.y -= 90;
                Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
                atack_time = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-3);
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
        Destroy(Effect_Instance);
    }
}
