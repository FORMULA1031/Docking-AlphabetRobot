using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour
{
    GameObject ResultPanel;
    GameObject Time_text;
    GameObject Defeated_text;
    GameObject ScoreNumber_text;
    GameObject GameResultText;
    GameObject FixedJoystick;
    GameObject ArmButton;
    GameObject HeadButton;
    float time = 0;
    int defeated_number = 0;
    bool gameclear_flag = false;
    bool gamefinish_flag = false;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResultPanel = GameObject.Find("Canvas/ResultPanel");
        Time_text = ResultPanel.transform.Find("Panel/TimeNumberText").gameObject;
        Defeated_text = ResultPanel.transform.Find("Panel/DefeatedNumberText").gameObject;
        ScoreNumber_text = ResultPanel.transform.Find("Panel/ScoreNumberText").gameObject;
        GameResultText = ResultPanel.transform.Find("Panel/GameResultText").gameObject;
        FixedJoystick = GameObject.Find("Canvas/FixedJoystick");
        ArmButton = GameObject.Find("Canvas/ArmButton");
        HeadButton = GameObject.Find("Canvas/HeadButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamefinish_flag)
        {
            time += Time.deltaTime;
        }
        score = (int)time + defeated_number * 10;
        if (gamefinish_flag)
        {
            Destroy(FixedJoystick);
            Destroy(ArmButton);
            Destroy(HeadButton);
            if (ResultPanel.transform.localScale.y < 1f)
            {
                ResultPanel.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.001f, transform.localScale.z);
            }
        }
    }

    public void Defeated()
    {
        defeated_number++;
    }

    public void GameOver(bool game_clear)
    {
        TextUpdate();
        gamefinish_flag = true;
        gameclear_flag = game_clear;
        if (game_clear && GameResultText != null)
        {
            GameResultText.GetComponent<Text>().text = "SUCCESS";
            GameResultText.GetComponent<Outline>().effectColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        else if (!game_clear && GameResultText != null)
        {
            GameResultText.GetComponent<Text>().text = "FAILURE";
            GameResultText.GetComponent<Outline>().effectColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    private void TextUpdate()
    {
        if (Time_text != null)
        {
            Time_text.GetComponent<Text>().text = time.ToString("f2");
        }
        if (Defeated_text != null)
        {
            Defeated_text.GetComponent<Text>().text = "" + defeated_number;
        }
        if (ScoreNumber_text != null)
        {
            ScoreNumber_text.GetComponent<Text>().text = "" + score;
        }
    }
}
