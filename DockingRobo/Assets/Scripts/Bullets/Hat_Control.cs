using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_Control : MonoBehaviour
{
    int power = 5;
    public bool hit_flag = false;
    bool enhancement_flag = false;

    private void Update()
    {

    }

    public void Hit_Reset()
    {
        hit_flag = false;
    }

    public void Enhancement(int _add_power)
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (!hit_flag)
            {
                if (other.gameObject.GetComponent<Status_Control>() != null)
                {
                    other.gameObject.GetComponent<Status_Control>().Damage(power);
                    hit_flag = true;
                }
            }
        }
        else
        {
            hit_flag = true;
        }
    }
}
