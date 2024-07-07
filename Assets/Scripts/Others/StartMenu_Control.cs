using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu_Control : MonoBehaviour
{
    public void OnPlayButton()  //ゲームの開始
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void OnCustomButton()    //カスタマイズ画面に遷移
    {
        SceneManager.LoadScene("CustomizeScene");
    }
}
