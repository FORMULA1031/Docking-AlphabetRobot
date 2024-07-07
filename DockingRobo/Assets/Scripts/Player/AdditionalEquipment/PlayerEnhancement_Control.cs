using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnhancement_Control : MonoBehaviour
{
    int stamina = 80;
    int stamina_max;
    float serial_time = 0;
    bool castof_flag = false;
    Slider slider;
    Status_Control status_Control;

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
        status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status_Control.add_power == status_Control.original_addpower)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Power(5);
        }
        serial_time += Time.deltaTime;
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

    private void OnDestroy()
    {
        if (!castof_flag)
        {
            if (status_Control != null)
            {
                status_Control.Return_Power();
                castof_flag = true;
            }
        }
    }
}
