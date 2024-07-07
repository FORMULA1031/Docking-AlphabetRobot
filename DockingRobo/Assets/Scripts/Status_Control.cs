using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Control : MonoBehaviour
{
    private Rigidbody rb;
    public int stamina;
    public int stamina_max;
    public GameObject Explosion;
    public GameObject DropItem;
    FixedJoystick joystick;
    int random_number;
    public int speed;
    public int original_speed;
    int rotation_speed = 0;
    public int add_power = 0;
    public int original_addpower = 0;
    public float stop_position;
    GameFinish GameFinish;
    float invincible_time = 0;
    bool invincible_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        random_number = Random.Range(1, 3);
        stamina_max = stamina;
        original_speed = speed;
        joystick = GameObject.Find("Canvas/FixedJoystick").GetComponent<FixedJoystick>();
        GameFinish = GameObject.Find("EventSystem").GetComponent<GameFinish>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stamina > stamina_max)
        {
            stamina = stamina_max;
        }
        if(stamina <= 0)
        {
            if(DropItem != null && random_number == 1)
            {
                Instantiate(DropItem, transform.position, Quaternion.identity);
            }
            if(gameObject.tag == "Player")
            {
                GameFinish.GameOver(false);
            }
            else if(gameObject.tag == "Enemy")
            {
                GameFinish.Defeated();
            }
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (invincible_flag)
        {
            invincible_time += Time.deltaTime;
            if(invincible_time >= 0.01f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
        Key_Outputs();
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "Player")
            rb.velocity = new Vector3(rotation_speed, rb.velocity.y, speed);
        else
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }

    void Key_Outputs()
    {
        if (gameObject.tag == "Player")
        {
            if(joystick.Horizontal < 0 && transform.position.x > -2.5f)
            {
                rotation_speed = -5;
            }
            else if (joystick.Horizontal > 0 && transform.position.x < 2.5f)
            {
                rotation_speed = 5;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -2.5f)
            {
                rotation_speed = -5;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 2.5f)
            {
                rotation_speed = 5;
            }
            else
            {
                rotation_speed = 0;
            }
        }
    }

    public void Stamina_Repair(int repair_amount)
    {
        stamina += repair_amount;
    }

    public void Damage(int damage_amount)
    {
        if (!invincible_flag)
        {
            stamina -= damage_amount;
            invincible_flag = true;
        }
    }

    public void Add_Speed(int _speed)
    {
        speed += _speed;
    }

    public void Return_Speed()
    {
        speed = original_speed;
    }

    public void Add_Power(int _power)
    {
        add_power += _power;
    }

    public void Return_Power()
    {
        add_power = original_addpower;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!invincible_flag)
        {
            if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
            {
                stamina -= 10;
                invincible_flag = true;
            }
            if (other.gameObject.tag == "Player")
            {
                stamina = 0;
                invincible_flag = true;
            }
        }
    }
}
