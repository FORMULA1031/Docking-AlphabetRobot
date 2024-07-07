using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInvisible_Control : MonoBehaviour
{
    int stamina = 40;
    int stamina_max;
    float serial_time = 0;
    bool castof_flag = false;
    Slider slider;
    GameObject Player;
    GameObject Pressure;
    Vector3 pressure_range;

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
        Player.GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        Color color = Player.GetComponent<Renderer>().material.color;
        color.a = 0.1f;
        Player.GetComponent<Renderer>().material.color = color;
        Pressure = transform.root.gameObject.transform.Find("Pressure").gameObject;
        pressure_range = Pressure.GetComponent<BoxCollider>().size;
        Pressure.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        SetActive(Player, 0.1f, "Legacy Shaders/Transparent/Diffuse");
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


    private void SetActive(GameObject _gameObject, float transparency, string shader_name)
    {
        if (_gameObject != null)
        {
            // 現オブジェクトの全ての子オブジェクトのアクティブを切り替える
            RecursiveSetActive(_gameObject, transparency, shader_name);
        }
    }

    private void RecursiveSetActive(GameObject a_CheckObject, float transparency, string shader_name)
    {
        // 対象オブジェクトの子オブジェクトをチェックする
        foreach (Transform child in a_CheckObject.transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                child.GetComponent<Renderer>().material.shader = Shader.Find(shader_name);
                Color color = child.GetComponent<Renderer>().material.color;
                color.a = transparency;
                child.GetComponent<Renderer>().material.color = color;
            }
            if (child.GetComponent<ParticleSystem>() != null && shader_name == "Standard")
            {
                child.GetComponent<Renderer>().material.shader = Shader.Find("Particles/Standard Unlit");
            }
            // 子オブジェクトのアクティブを切り替える
            GameObject childObject = child.gameObject;
            if (childObject != null)
            {
                SetActive(childObject, transparency, shader_name);
            }
        }
    }

    private void OnDestroy()
    {
        SetActive(Player, 1f, "Standard");
        if (Player != null)
        {
            if (Player.GetComponent<Renderer>().material != null)
            {
                Player.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                Color color = Player.GetComponent<Renderer>().material.color;
                color.a = 1f;
                Pressure.GetComponent<BoxCollider>().size = pressure_range;
            }
        }
    }
}
