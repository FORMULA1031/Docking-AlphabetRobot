using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTail_Control : MonoBehaviour
{
    public GameObject Tail;
    Tail_Control Tail_Control;
    GameObject Tail_Instance;
    Slider slider;
    int stamina = 100;
    int stamina_max;
    float serial_time = 0;

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
        Tail_Instance = Instantiate(Tail, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation);
        Tail_Control = Tail_Instance.GetComponent<Tail_Control>();
        Tail_Control.Set_Action(true);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
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
        Tail_Instance.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1);
    }

    private void OnDestroy()
    {
        Destroy(Tail_Instance);
    }
}
