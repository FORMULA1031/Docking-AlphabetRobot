using UnityEngine;

public class Player_Settings : MonoBehaviour
{
    public static string[] core_obtaining = { "null", "null", "null", "null", "null", "Z" };    //コアパーツ一覧
    public static string[] head_obtaining = { "C", "D", "F", "O", "L", "R" };   //ヘッドパーツ一覧
    public static string[] arm_obtaining = { "A", "B", "G", "K", "P", "H", "N" };   //アームパーツ一覧
    public static string[] backpack_obtaining = { "E", "I", "J", "M", "S", "T", "W" };  //バックパックパーツ一覧
    public static string core_setting = "Z";    //選択中のコアパーツ
    public static string head_setting = ""; //選択中のヘッドパーツ
    public static string arm_setting = "";  //選択中のアームパーツ
    public static string backpack_setting = ""; //選択中のバックパックパーツ
    public string last_weapon = ""; //最後に選択したパーツ

    // Start is called before the first frame update
    void Start()
    {
        load(); //選択していた装備のロード
    }

    public void Change_Core(string _core_setting)   //コアパーツの変更
    {
        core_setting = _core_setting;
        save(); //選択した装備のセーブ
    }

    public void Change_Head(string _head_setting)   //ヘッドパーツの変更
    {
        head_setting = _head_setting;
        last_weapon = _head_setting;
        save(); //選択した装備のセーブ
    }

    public void Change_Arm(string _arm_setting) //アームパーツの変更
    {
        arm_setting = _arm_setting;
        last_weapon = _arm_setting;
        save(); //選択した装備のセーブ
    }

    public void Change_Backpack(string _backpack_setting)   //バックパックパーツの変更
    {
        backpack_setting = _backpack_setting;
        last_weapon = _backpack_setting;
        save(); //選択した装備のセーブ
    }

    public static void save()   //選択した装備のセーブ
    {
        PlayerPrefs.SetString("head_setting", head_setting);
        PlayerPrefs.SetString("arm_setting", arm_setting);
        PlayerPrefs.SetString("backpack_setting", backpack_setting);
        PlayerPrefs.Save();
    }

    public static void load()   //選択していた装備のロード
    {
        head_setting = PlayerPrefs.GetString("head_setting", "");
        arm_setting = PlayerPrefs.GetString("arm_setting", "");
        backpack_setting = PlayerPrefs.GetString("backpack_setting", "");
    }
}
