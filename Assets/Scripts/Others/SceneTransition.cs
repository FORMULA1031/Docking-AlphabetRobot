using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string SceneName;    //�J�ڂ���V�[����

    public void OnClick()   //�V�[���J�ڂ���
    {
        SceneManager.LoadScene(SceneName);
    }
}
