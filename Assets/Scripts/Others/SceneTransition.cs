using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string SceneName;    //遷移するシーン名

    public void OnClick()   //シーン遷移する
    {
        SceneManager.LoadScene(SceneName);
    }
}
