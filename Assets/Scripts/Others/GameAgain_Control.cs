using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAgain_Control : MonoBehaviour
{
    string scenename;   //現在のシーン名

    // Start is called before the first frame update
    void Start()
    {
        scenename = SceneManager.GetActiveScene().name; //取得
    }

    public void OnClick()   //コンティニュー処理
    {
        SceneManager.LoadScene(scenename);
    }
}
