using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAgain_Control : MonoBehaviour
{
    string scenename;   //���݂̃V�[����

    // Start is called before the first frame update
    void Start()
    {
        scenename = SceneManager.GetActiveScene().name; //�擾
    }

    public void OnClick()   //�R���e�B�j���[����
    {
        SceneManager.LoadScene(scenename);
    }
}
