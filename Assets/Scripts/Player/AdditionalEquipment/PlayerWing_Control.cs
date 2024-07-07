using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerWing_Control : MonoBehaviour
{
    int stamina = 10;   //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 3f; //�s����̒x������
    Slider slider;  //�ϋv�l�p�̃o�[
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Rigidbody Player_Rigidbody; //�v���C���[���R���|�[�l���g���Ă���Player_Rigidbody
    GameObject Wing_right;  //�E��
    GameObject Wing_left;   //����
    bool leftrotation_flag = true;  //����]���邩�̃t���O
    float speed = 1.0f; //���쑬�x
    bool castof_flag = false;   //���̃p�[�c���p�[�W�������̃t���O
    bool pusharmbutton_flag = false;    //�A�[���{�^�������������̃t���O
    bool pushheadbutton_flag = false;   //�w�b�h�{�^�������������̃t���O

    // Start is called before the first frame update
    void Start()    //�E�B���O�p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Player = transform.root.gameObject;
        Player_Rigidbody = Player.GetComponent<Rigidbody>();
        Wing_right = transform.Find("Wing_right").gameObject;
        Wing_left = transform.Find("Wing_left").gameObject;

        EventTrigger.Entry entry = new EventTrigger.Entry();    //�A�[���{�^���̐ݒ�
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);

        EventTrigger.Entry entry_head = new EventTrigger.Entry();   //�w�b�h�{�^���̐ݒ�
        entry_head.eventID = EventTriggerType.PointerDown;
        entry_head.callback.AddListener((x) => PushDown_HeadButton());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry_head = new EventTrigger.Entry();
        entry_head.eventID = EventTriggerType.PointerUp;
        entry_head.callback.AddListener((x) => PushUp_HeadButton());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        serial_time += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushheadbutton_flag || Input.GetKey(KeyCode.A) || pusharmbutton_flag)    //�W�����v���鏈��
        {
            if (serial_time >= 2f)  //�x������
            {
                Player_Rigidbody.AddForce(transform.up * 8, ForceMode.Impulse);
                stamina--;
                serial_time = 0;
            }
        }

        if (stamina <= 0)   //�ϋv�l�������Ȃ����ꍇ
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //�ϋv�l�p�̃o�[�̍X�V
    }

    private void FixedUpdate()
    {
        //�v���C���[�̋@���͂̏㏸
        if (transform.root.gameObject.GetComponent<Status_Control>().speed == transform.root.gameObject.GetComponent<Status_Control>().original_speed)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Speed(1);
        }
        if (!leftrotation_flag) //�E��]����
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)  //����]����
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
    }

    public void PushDown_ArmButton()    //�A�[���{�^�����������ꍇ
    {
        pusharmbutton_flag = true;
    }

    public void PushUp_ArmButton()  //�A�[���{�^���𗣂����ꍇ
    {
        pusharmbutton_flag = false;
    }

    public void PushDown_HeadButton()   //�w�b�h�{�^�����������ꍇ
    {
        pushheadbutton_flag = true;
    }

    public void PushUp_HeadButton() //�w�b�h�{�^���𗣂����ꍇ
    {
        pushheadbutton_flag = false;
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //���̃p�[�c���p�[�W���Ă��Ȃ��ꍇ
        {
            transform.root.gameObject.GetComponent<Status_Control>().Return_Speed();
            castof_flag = true;
        }
    }
}
