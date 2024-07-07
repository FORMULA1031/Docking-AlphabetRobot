using UnityEngine;

public class Player_Settings : MonoBehaviour
{
    public static string[] core_obtaining = { "null", "null", "null", "null", "null", "Z" };    //�R�A�p�[�c�ꗗ
    public static string[] head_obtaining = { "C", "D", "F", "O", "L", "R" };   //�w�b�h�p�[�c�ꗗ
    public static string[] arm_obtaining = { "A", "B", "G", "K", "P", "H", "N" };   //�A�[���p�[�c�ꗗ
    public static string[] backpack_obtaining = { "E", "I", "J", "M", "S", "T", "W" };  //�o�b�N�p�b�N�p�[�c�ꗗ
    public static string core_setting = "Z";    //�I�𒆂̃R�A�p�[�c
    public static string head_setting = ""; //�I�𒆂̃w�b�h�p�[�c
    public static string arm_setting = "";  //�I�𒆂̃A�[���p�[�c
    public static string backpack_setting = ""; //�I�𒆂̃o�b�N�p�b�N�p�[�c
    public string last_weapon = ""; //�Ō�ɑI�������p�[�c

    // Start is called before the first frame update
    void Start()
    {
        load(); //�I�����Ă��������̃��[�h
    }

    public void Change_Core(string _core_setting)   //�R�A�p�[�c�̕ύX
    {
        core_setting = _core_setting;
        save(); //�I�����������̃Z�[�u
    }

    public void Change_Head(string _head_setting)   //�w�b�h�p�[�c�̕ύX
    {
        head_setting = _head_setting;
        last_weapon = _head_setting;
        save(); //�I�����������̃Z�[�u
    }

    public void Change_Arm(string _arm_setting) //�A�[���p�[�c�̕ύX
    {
        arm_setting = _arm_setting;
        last_weapon = _arm_setting;
        save(); //�I�����������̃Z�[�u
    }

    public void Change_Backpack(string _backpack_setting)   //�o�b�N�p�b�N�p�[�c�̕ύX
    {
        backpack_setting = _backpack_setting;
        last_weapon = _backpack_setting;
        save(); //�I�����������̃Z�[�u
    }

    public static void save()   //�I�����������̃Z�[�u
    {
        PlayerPrefs.SetString("head_setting", head_setting);
        PlayerPrefs.SetString("arm_setting", arm_setting);
        PlayerPrefs.SetString("backpack_setting", backpack_setting);
        PlayerPrefs.Save();
    }

    public static void load()   //�I�����Ă��������̃��[�h
    {
        head_setting = PlayerPrefs.GetString("head_setting", "");
        arm_setting = PlayerPrefs.GetString("arm_setting", "");
        backpack_setting = PlayerPrefs.GetString("backpack_setting", "");
    }
}
