using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour
{
    GameObject ResultPanel; //���U���g�p�l��
    GameObject Time_text_result;    //�\�����Ă���TimeNumberText
    GameObject Time_text;   //�\�����Ă���o�ߎ���
    GameObject Defeated_text;   //�|�����G�̐���\������e�L�X�g
    GameObject ScoreNumber_text;    //�X�R�A��\������e�L�X�g
    GameObject GameResultText;  //�Q�[�����ʂ�\������e�L�X�g
    GameObject FixedJoystick;   //�\�����Ă���W���C�X�e�B�b�N
    GameObject ArmButton;   //�\�����Ă���A�[���{�^��
    GameObject HeadButton;  //�\�����Ă���w�b�h�{�^��
    float time = 0; //�Q�[���J�n����̌o�ߎ���
    int defeated_number = 0;    //�|�����G�̐�
    bool gamefinish_flag = false;   //�Q�[�����I�����Ă��邩�̃t���O
    int score = 0;  //�X�R�A��

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
        if (!gamefinish_flag)   //�Q�[���J�n��
        {
            time += Time.deltaTime;
            if (Time_text != null)
            {
                Time_text.GetComponent<Text>().text = time.ToString("f2");
            }
        }
        score = (int)time + defeated_number * 10;   //�X�R�A�̍X�V
        if (gamefinish_flag)    //�Q�[���I����
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

    public void Defeated()  //�G��|�����ꍇ
    {
        defeated_number++;
    }

    public void GameOver(bool game_clear)   //�Q�[�����ʂ̍X�V
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

    private void TextUpdate()   //�\������e�L�X�g�̍X�V
    {
        if (Time_text_result != null)   //�Q�[�����ʂ̃e�L�X�g�X�V
        {
            Time_text_result.GetComponent<Text>().text = time.ToString("f2");
        }
        if (Defeated_text != null)  //�|�����G�̐��̃e�L�X�g�X�V
        {
            Defeated_text.GetComponent<Text>().text = "" + defeated_number;
        }
        if (ScoreNumber_text != null)   //�X�R�A�̃e�L�X�g�X�V
        {
            ScoreNumber_text.GetComponent<Text>().text = "" + score;
        }
    }
}
