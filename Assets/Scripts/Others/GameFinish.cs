using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour
{
    GameObject ResultPanel; //リザルトパネル
    GameObject Time_text_result;    //表示しているTimeNumberText
    GameObject Time_text;   //表示している経過時間
    GameObject Defeated_text;   //倒した敵の数を表示するテキスト
    GameObject ScoreNumber_text;    //スコアを表示するテキスト
    GameObject GameResultText;  //ゲーム結果を表示するテキスト
    GameObject FixedJoystick;   //表示しているジョイスティック
    GameObject ArmButton;   //表示しているアームボタン
    GameObject HeadButton;  //表示しているヘッドボタン
    float time = 0; //ゲーム開始からの経過時間
    int defeated_number = 0;    //倒した敵の数
    bool gamefinish_flag = false;   //ゲームが終了しているかのフラグ
    int score = 0;  //スコア数

    // Start is called before the first frame update
    void Start()
    {
        ResultPanel = GameObject.Find("Canvas/ResultPanel");
        Time_text_result = ResultPanel.transform.Find("Panel/TimeNumberText").gameObject;
        Time_text = GameObject.Find("Canvas/ElapsedTimePanel/NumberText").gameObject;
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
        if (!gamefinish_flag)   //ゲーム開始中
        {
            time += Time.deltaTime;
            if (Time_text != null)
            {
                Time_text.GetComponent<Text>().text = time.ToString("f2");
            }
        }
        score = (int)time + defeated_number * 10;   //スコアの更新
        if (gamefinish_flag)    //ゲーム終了後
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

    public void Defeated()  //敵を倒した場合
    {
        defeated_number++;
    }

    public void GameOver(bool game_clear)   //ゲーム結果の更新
    {
        TextUpdate();
        gamefinish_flag = true;
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

    private void TextUpdate()   //表示するテキストの更新
    {
        if (Time_text_result != null)   //ゲーム結果のテキスト更新
        {
            Time_text_result.GetComponent<Text>().text = time.ToString("f2");
        }
        if (Defeated_text != null)  //倒した敵の数のテキスト更新
        {
            Defeated_text.GetComponent<Text>().text = "" + defeated_number;
        }
        if (ScoreNumber_text != null)   //スコアのテキスト更新
        {
            ScoreNumber_text.GetComponent<Text>().text = "" + score;
        }
    }
}
