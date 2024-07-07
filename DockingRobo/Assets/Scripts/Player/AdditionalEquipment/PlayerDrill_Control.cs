using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerDrill_Control : MonoBehaviour
{
    public GameObject Effect;
    GameObject Effect_Instance;
    GameObject Muzzle;
    int bullets_number = 20;
    float revolution_time = 0f;
    Text WeaponNumber_text;
    GameObject Player;
    Status_Control Status_Control;
    int add_power = 0;

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
        Muzzle = transform.Find("Muzzle").gameObject;
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        Quaternion muzzle_quaternion = transform.rotation;
        Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
        Vector3 drilleffect_rotation = Effect_Instance.transform.localRotation.eulerAngles;
        drilleffect_rotation.y -= 90;
        Effect_Instance.transform.localRotation = Quaternion.Euler(drilleffect_rotation);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = false;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;
        if (add_power != 0)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Effect_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
            Destroy(Effect_Instance);
            Destroy(gameObject);
        }
        Display_BulletsNumber();
    }

    void Display_BulletsNumber()
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (Effect_Instance != null)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Destroy_Flag();
        }
    }
}
