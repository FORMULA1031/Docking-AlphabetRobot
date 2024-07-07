using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail_Control : MonoBehaviour
{
    int power = 20;
    int speed = 3;
    bool action_flag = false;
    bool leftrotation_flag = true;
    float stop_time = 0;
    Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = this.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (action_flag)
        {
            if (leftrotation_flag)
            {
                if (transform.localEulerAngles.x < 100 && transform.localEulerAngles.x >= 80)
                {
                    speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if(!leftrotation_flag)
            {
                if (transform.localEulerAngles.x > 260 && transform.localEulerAngles.x <= 280)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
            }
            transform.Rotate(new Vector3(speed, 0, 0));
        }
    }

    public void Set_Action(bool _actionflag)
    {
        action_flag = _actionflag;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
        if (other.gameObject.tag == "Player" && gameObject.tag != "Untagged")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                Destroy(gameObject.transform.root.gameObject);
            }
        }

        if(gameObject.tag == "Untagged")
        {
            if (leftrotation_flag)
            {
                leftrotation_flag = false;
            }
            else
            {
                leftrotation_flag = true;
            }
            speed *= -1;
        }
    }
}
