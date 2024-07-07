using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWing_Control : MonoBehaviour
{
    int stamina = 80;
    int stamina_max;
    float serial_time = 0;
    Slider slider;
    GameObject Player;
    Rigidbody Player_Rigidbody;
    GameObject Wing_right;
    GameObject Wing_left;
    float jump_time = 2;
    bool leftrotation_flag = true;
    float speed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = gameObject.transform.parent;
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Player = transform.root.gameObject;
        Player_Rigidbody = Player.GetComponent<Rigidbody>();
        Wing_right = transform.Find("Wing_right").gameObject;
        Wing_left = transform.Find("Wing_left").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        serial_time += Time.deltaTime;
        jump_time += Time.deltaTime;
        if (serial_time >= 0.3f)
        {
            stamina--;
            serial_time = 0;
        }

        if (stamina <= 0)
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max;
    }

    private void FixedUpdate()
    {
        if(jump_time >= 3)
        {
            Player_Rigidbody.AddForce(transform.up * 8, ForceMode.Impulse);
            jump_time = 0;
        }
        if (!leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
    }
}
