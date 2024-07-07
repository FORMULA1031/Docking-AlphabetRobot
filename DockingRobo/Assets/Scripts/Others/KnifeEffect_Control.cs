using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEffect_Control : MonoBehaviour
{
    int power = 20;
    public bool hit_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }

        if(other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            hit_flag = true;
        }
    }
}
