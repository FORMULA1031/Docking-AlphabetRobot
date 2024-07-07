using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHat_Control : MonoBehaviour
{
    GameObject Muzzle;  //��������n�b�g�̍��W�I�u�W�F�N�g
    public GameObject Hat;  //��������n�b�g
    GameObject Hat_Instance;    //���������n�b�g
    float serial_time = 0.0f;   //�n�b�g��΂��̒x������
    int bullets_number = 20;    //�n�b�g��΂��̎c���
    Text WeaponNumber_text; //�\������n�b�g��΂��̎c��񐔃e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    bool atack_flag = false;    //�U�������̃t���O
    int add_power = 0;  //��������U���͂̒l
    Vector3 hat_position;   //�n�b�g�̐��ʒu

    // Start is called before the first frame update
    void Start()    //�n�b�g�p�[�c�̒ǉ�����
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
        Muzzle = transform.Find("Muzzle").gameObject;
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
        Player = transform.root.gameObject;
        Status_Control = Player.GetComponent<Status_Control>();
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/HeadButton").AddComponent<EventTrigger>().triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        if (add_power != 0)
        {
            Hat_Instance.GetComponent<Hat_Control>().Enhancement(add_power);
        }
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //�U������
        {
            atack_flag = true;
        }
        if (atack_flag)
        {
            serial_time += Time.deltaTime;
        }
        if(transform.position.z > Hat_Instance.transform.position.z && atack_flag)
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            bullets_number--;
            atack_flag = false;
        }
        if (bullets_number <= 0)    //�c��n�b�g��΂��̉񐔂������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������n�b�g��΂��̎c��񐔂̍X�V
    }

    private void FixedUpdate()
    {
        if (atack_flag) //�U�����̏���
        {
            if (serial_time < 1.0f) //�O�֔�΂�
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * 3f * Status_Control.speed;
            }
            else if (serial_time >= 1.0f && serial_time < 2.0f) //�v���C���[�ɖ߂�
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.forward * -3f * Status_Control.speed;
            }
            else if (serial_time >= 2.0f && serial_time < 3.0f) //�n�b�g�𐳈ʒu�Ɉړ�
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f);
                Hat_Instance.transform.position = hat_position;
            }
            else if (serial_time >= 3f) //�U�����[�V�����I��
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f);
                Hat_Instance.transform.position = hat_position;
                serial_time = 0;
                bullets_number--;
                atack_flag = false;
            }
            if (transform.position.z + 1.5f < Hat_Instance.transform.position.z && Hat_Instance.transform.position.y > 1f)  //���ʒu�͈͂ɂ��Ȃ��ꍇ
            {
                Hat_Instance.transform.position = 
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y - 0.4f, Hat_Instance.transform.position.z);
                Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
            }
            else if (transform.position.z + 1.5f > Hat_Instance.transform.position.z && Hat_Instance.transform.position.y < 1.4f)   //���ʒu�͈͂ɂ����ꍇ
            {
                Hat_Instance.transform.position =
                    new Vector3(Hat_Instance.transform.position.x, Hat_Instance.transform.position.y + 0.4f, Hat_Instance.transform.position.z);
            }
        }
        else
        {
            hat_position = Muzzle.transform.position + new Vector3(0, 0, 0.1f * Time.deltaTime);
            Hat_Instance.transform.position = hat_position;
        }
    }

    void Display_BulletsNumber()    //�\������n�b�g��΂��̎c��񐔂̍X�V
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

    private void OnDestroy()
    {
        Destroy(Hat_Instance);
    }
}
