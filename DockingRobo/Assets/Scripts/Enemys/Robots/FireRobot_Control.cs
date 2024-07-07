using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Head;
    GameObject Muzzle;
    public GameObject Effect;
    GameObject Effect_Instance;
    bool lockon_flag = false;
    int rotation_speed = 1;
    bool leftrotation_flag = true;
    bool effect_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.Find("Head").gameObject;
        Muzzle = transform.Find("Head/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            //rotation.y -= 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)
        {
            if (leftrotation_flag)
            {
                if (Head.transform.localEulerAngles.y >= 30 && Head.transform.localEulerAngles.y < 90)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if (!leftrotation_flag)
            {
                if (Head.transform.localEulerAngles.y <= 330 && Head.transform.localEulerAngles.y > 270)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = true;
                }
            }
            Head.transform.Rotate(new Vector3(0, rotation_speed, 0));
            Effect_Instance.transform.position = Muzzle.transform.position;
            Effect_Instance.transform.rotation = Head.transform.rotation;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
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
