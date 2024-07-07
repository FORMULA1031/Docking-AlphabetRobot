using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailRobot_Control : MonoBehaviour
{
    bool lockon_flag = false;
    GameObject Tail;
    Tail_Control Tail_Control;

    // Start is called before the first frame update
    void Start()
    {
        Tail = gameObject.transform.Find("Backpack/Tail/Sphere").gameObject;
        Tail_Control = Tail.GetComponent<Tail_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        Tail_Control.Set_Action(lockon_flag);
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
