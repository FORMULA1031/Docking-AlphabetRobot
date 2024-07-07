using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    Slider slider;  //�\������ϋv�l�Q�[�W
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Status_Control Status_Control;  //�v���C���[���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    public Slider slider_red;   //�\������ϋv�l�Q�[�W�̒x���Q�[�W
    float damage_amount = 0;    //�x������_���[�W��
    float time = 0.0f;  //�_���[�W�����̒x������
    bool downgauge_flag = true; //�x���Q�[�W���������Ă悢���̃t���O
    bool startdowngauge_flag = true;    //�_���[�W�������������̃t���O
    float criterion = 0.0f; //�x���Q�[�W�̌���������ݒ�̍X�V�������邩�̊

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Player = GameObject.Find("ZeroRobot");
        Status_Control = Player.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)Status_Control.stamina / (float)Status_Control.stamina_max;   //�ϋv�l�̍X�V

        if (slider_red.value > slider.value && downgauge_flag)  //�x�������ϋv�l���ϋv�l���������ꍇ
        {
            if (startdowngauge_flag || (slider_red.value - slider.value) / 5 > criterion)   //�x���Q�[�W�̌���������ݒ�̍X�V
            {
                damage_amount = (slider_red.value - slider.value) / 5;
                criterion = damage_amount;
                startdowngauge_flag = false;
            }
            slider_red.value -= damage_amount;  //�x���Q�[�W�̌���
            downgauge_flag = false;
        }
        else if (!startdowngauge_flag)  //�x���Q�[�W�����̃��Z�b�g
        {
            startdowngauge_flag = true;
        }

        if (!downgauge_flag)    //�Q�[�W�����̒x������
        {
            time += Time.deltaTime;
            if(time >= 0.1f)
            {
                downgauge_flag = true;
                time = 0;
            }
        }
    }
}
