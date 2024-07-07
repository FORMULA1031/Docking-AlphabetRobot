using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle;
    public GameObject Effect;
    GameObject Effect_Instance;
    bool lockon_flag = false;
    bool effect_flag = false;
    float jump_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Effect_Instance.transform.parent = Muzzle.transform;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)
        {
            jump_time += Time.deltaTime;
            if (jump_time >= 2f)
            {
                rb.AddForce(transform.up * 8000, ForceMode.Impulse);
                jump_time = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)
        {
            transform.Rotate(new Vector3(0, -5f, 0));
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
        Destroy(Effect_Instance);
    }
}
