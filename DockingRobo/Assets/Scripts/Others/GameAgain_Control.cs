using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAgain_Control : MonoBehaviour
{
    string scenename;

    // Start is called before the first frame update
    void Start()
    {
        scenename = SceneManager.GetActiveScene().name;
    }

    public void OnClick()
    {
        SceneManager.LoadScene(scenename);
    }
}
