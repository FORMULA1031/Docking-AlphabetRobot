using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnifeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;
    GameObject Player;
    float time = 0;
    int power = 15;
    int speed = 0;
    bool enhancement_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.2f)
        {
            Destroy(gameObject);
        }
        speed = Player.GetComponent<Status_Control>().speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(-20, rb.velocity.y, speed);
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

        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Destroy(gameObject);
        }
    }
}
