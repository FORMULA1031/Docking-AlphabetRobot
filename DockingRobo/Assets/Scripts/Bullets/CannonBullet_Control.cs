using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet_Control : MonoBehaviour
{
    private Rigidbody rb;
    public int speed;
    public int power;
    float launch_time = 0;
    bool induction_flag = false;
    float induction_time = 0.1f;
    GameObject Player;
    bool hit_flag = false;
    bool enhancement_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {
        launch_time += Time.deltaTime;
        if (induction_flag && Player != null)
        {
            if (launch_time >= induction_time && transform.position.z > Player.transform.position.z + 1.5f)
            {
                transform.LookAt(Player.transform);
                induction_time = launch_time + 1.2f;
            }
            if(transform.position.z < Player.transform.position.z - 1f)
            {
                Destroy(gameObject);
            }
        }
        if (launch_time >= 3)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    public void Induction(bool flag)
    {
        induction_flag = flag;
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
                    Destroy(gameObject);
                    hit_flag = true;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Barrier")
        {
            Destroy(gameObject);
        }
    }
}
