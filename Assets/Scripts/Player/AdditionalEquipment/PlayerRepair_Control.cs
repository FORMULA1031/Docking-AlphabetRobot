using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerRepair_Control : MonoBehaviour
{
    int bullets_number = 2; //�g�p��
    float bullet_serialspeed = 1f;  //�g�p��ɂ��x������
    Text WeaponNumber_text; //�\������c��g�p�񐔃e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    bool pushbutton_flag = false;   //�{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[���R���|�[�l���g���Ă���Status_Control�X�N���v�g

    // Start is called before the first frame update
    void Start()    //���y�A�p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
        Transform[] brotrans = new Transform[parent.childCount];
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
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //���y�A�̎g�p����
        {
            if (bullet_serialspeed >= 1f)   //�x������
            {
                Status_Control.Stamina_Repair(10);
                bullet_serialspeed = 0;
                bullets_number--;
            }
        }

        if (bullets_number <= 0)    //�c��g�p�񐔂������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������c��g�p�񐔃e�L�X�g�̍X�V
    }

    void Display_BulletsNumber()    //�\������c��g�p�񐔃e�L�X�g�̍X�V
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    public void PushDown_Button()   //�{�^�����������ꍇ
    {
        pushbutton_flag = true;
    }

    public void PushUp_Button() //�{�^���𗣂����ꍇ
    {
        pushbutton_flag = false;
    }
}
