using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillRobot_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Muzzle;
    public GameObject Effect;
    GameObject Effect_Instance;
    bool lockon_flag = false;
    bool effect_flag = false;
    bool move_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rotation.y -= 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)
        {
            Effect_Instance.transform.position = Muzzle.transform.position;
            if (Effect_Instance.GetComponent<DrillEffect_Control>().hit_flag)
            {
                gameObject.GetComponent<Status_Control>().Damage(100);
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
