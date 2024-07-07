using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJet_Control : MonoBehaviour
{
    int stamina = 100;
    int stamina_max;
    float serial_time = 0;
    bool castof_flag = false;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = gameObject.transform.parent;
        Transform[] brotrans = new Transform[parent.childCount];
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
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.root.gameObject.GetComponent<Status_Control>().speed == transform.root.gameObject.GetComponent<Status_Control>().original_speed)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Speed(3);
        }
        serial_time += Time.deltaTime;
        if(serial_time >= 0.3f)
        {
            stamina--;
            serial_time = 0;
        }

        if(stamina <= 0)
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max;
    }

    private void OnDestroy()
    {
        if (!castof_flag)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Return_Speed();
            castof_flag = true;
        }
    }
}
