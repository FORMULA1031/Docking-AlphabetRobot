using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu_Control : MonoBehaviour
{
    public void OnPlayButton()  //�Q�[���̊J�n
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void OnCustomButton()    //�J�X�^�}�C�Y��ʂɑJ��
    {
        SceneManager.LoadScene("CustomizeScene");
    }
}
