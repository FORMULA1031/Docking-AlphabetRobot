using UnityEngine;

public class Setting_Control : MonoBehaviour
{
    public GameObject SettingPanel; //�ݒ�p�l��
    public GameObject back_button;  //�X�^�[�g��ʂɖ߂�{�^��
    Vector3 OpenScale = new Vector3(1, 1, 1);   //�p�l�����J�������̑傫��
    Vector3 CloseScale = new Vector3(0, 0, 0);  //�p�l��������Ƃ��̑傫��

    // Start is called before the first frame update
    void Start()
    {
        SettingPanel.GetComponent<RectTransform>().localScale = CloseScale;
        back_button.GetComponent<RectTransform>().localScale = CloseScale;
    }

    public void SettingButton() //�ݒ�p�l�����J��
    {
        SettingPanel.GetComponent<RectTransform>().localScale = OpenScale;
        back_button.GetComponent<RectTransform>().localScale = OpenScale;
    }

    public void BackButton()    //�ݒ�p�l�������
    {
        SettingPanel.GetComponent<RectTransform>().localScale = CloseScale;
        back_button.GetComponent<RectTransform>().localScale = CloseScale;
    }
}
