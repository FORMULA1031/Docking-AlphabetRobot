using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    Slider slider;
    GameObject Player;
    Status_Control Status_Control;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Player = GameObject.Find("ZeroRobot");
        Status_Control = Player.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)Status_Control.stamina / (float)Status_Control.stamina_max;
    }
}
