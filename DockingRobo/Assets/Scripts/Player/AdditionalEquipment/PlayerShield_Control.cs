using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShield_Control : MonoBehaviour
{
    public GameObject Shield;
    GameObject Shield_Instance;
    GameObject Muzzle;
    Slider slider;
    int stamina = 80;
    int stamina_max;
    float serial_time = 0;
    Vector3 rotation_shield;

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
        Muzzle = transform.Find("Muzzle").gameObject;
        Shield_Instance = Instantiate(Shield, new Vector3(Muzzle.transform.position.x, Muzzle.transform.position.y, Muzzle.transform.position.z), transform.rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        rotation_shield = Shield_Instance.transform.localRotation.eulerAngles;
        rotation_shield.y += 90;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate()
    {
        Shield_Instance.transform.position = new Vector3(Muzzle.transform.position.x, Muzzle.transform.position.y, Muzzle.transform.position.z);
        Shield_Instance.transform.localRotation = Quaternion.Euler(rotation_shield);
    }

    private void OnDestroy()
    {
        Destroy(Shield_Instance);
    }
}
