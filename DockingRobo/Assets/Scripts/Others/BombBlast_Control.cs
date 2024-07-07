using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlast_Control : MonoBehaviour
{
    int power = 3;
    float serial_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        serial_time += Time.deltaTime;
        if(serial_time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
