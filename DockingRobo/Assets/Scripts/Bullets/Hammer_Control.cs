using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_Control : MonoBehaviour
{
    int power = 25;
    bool enhancement_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Enhancement(int _add_power)
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    public void Destroy_Flag()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
    }
}
