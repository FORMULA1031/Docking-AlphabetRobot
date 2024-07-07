using UnityEngine;

public class Setting_Control : MonoBehaviour
{
    public GameObject SettingPanel; //設定パネル
    public GameObject back_button;  //スタート画面に戻るボタン
    Vector3 OpenScale = new Vector3(1, 1, 1);   //パネルを開いた時の大きさ
    Vector3 CloseScale = new Vector3(0, 0, 0);  //パネルを閉じたときの大きさ

    // Start is called before the first frame update
    void Start()
    {
        SettingPanel.GetComponent<RectTransform>().localScale = CloseScale;
        back_button.GetComponent<RectTransform>().localScale = CloseScale;
    }

    public void SettingButton() //設定パネルを開く
    {
        SettingPanel.GetComponent<RectTransform>().localScale = OpenScale;
        back_button.GetComponent<RectTransform>().localScale = OpenScale;
    }

    public void BackButton()    //設定パネルを閉じる
    {
        SettingPanel.GetComponent<RectTransform>().localScale = CloseScale;
        back_button.GetComponent<RectTransform>().localScale = CloseScale;
    }
}
