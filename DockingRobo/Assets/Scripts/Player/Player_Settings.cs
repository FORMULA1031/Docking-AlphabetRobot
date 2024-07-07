using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Settings : MonoBehaviour
{
    public static string[] core_obtaining = { "null", "null", "null", "null", "null", "Z" };
    public static string[] head_obtaining = { "C", "D", "F", "O", "L", "R" };
    public static string[] arm_obtaining = { "A", "B", "G", "K", "P", "H", "N" };
    public static string[] backpack_obtaining = { "E", "I", "J", "M", "S", "T", "W" };
    public static string core_setting = "Z";
    public static string head_setting = "";
    public static string arm_setting = "";
    public static string backpack_setting = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change_Core(string _core_setting)
    {
        core_setting = _core_setting;
    }

    public void Change_Head(string _head_setting)
    {
        head_setting = _head_setting;
    }

    public void Change_Arm(string _arm_setting)
    {
        arm_setting = _arm_setting;
    }

    public void Change_Backpack(string _backpack_setting)
    {
        backpack_setting = _backpack_setting;
    }
}
