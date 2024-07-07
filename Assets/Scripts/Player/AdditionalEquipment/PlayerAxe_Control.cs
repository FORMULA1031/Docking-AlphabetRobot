using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerAxe_Control : MonoBehaviour
{
    public GameObject axe_effect;   //��������a���G�t�F�N�g
    GameObject Muzzle;  //��������a���G�t�F�N�g�̍��W�I�u�W�F�N�g
    float bullet_serialspeed = 1f;  //�a���G�t�F�N�g�𐶐�����x������
    bool action_flag = false;   //�U���A�N�V�������Ă悢���̃t���O
    int bullets_number = 10;    //�e��
    Text WeaponNumber_text; //�\������e���e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    GameObject Arm_left;    //�v���C���[�I�u�W�F�N�g�̍��r
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Conrol�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�A�b�N�X�p�[�c�̒ǉ�����
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
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.x -= 90;
        rotation.y -= 90;
        rotation.z -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_Button());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_Button());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Axe_Player_L(Clone)").gameObject;
        if (transform.parent.gameObject.transform.parent.gameObject.name == "Arm_right")
        {
            action_flag = true;
        }
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (action_flag)
        {
            add_power = Status_Control.add_power;
            bullet_serialspeed += Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || pushbutton_flag) //�U������
            {
                if (bullet_serialspeed >= 0.5f)
                {
                    Instance_Effect();  //�a���G�t�F�N�g�̐���
                    bullet_serialspeed = 0f;
                    bullets_number--;
                }
            }
            Display_BulletsNumber();    //�\������c��e���̍X�V
        }
        if (bullets_number <= 0)    //�c��e���������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
    }

    void Instance_Effect()  //�a���G�t�F�N�g�̐���
    {
        GameObject Effect_Instance = Instantiate(axe_effect, Muzzle.transform.position, Quaternion.identity);
        Effect_Instance.GetComponent<PlayerAxeEffect_Control>().Enhancement(add_power);
    }

    void Display_BulletsNumber()    //�\������c��e���̍X�V
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

    public void OnDestroy()
    {
        Destroy(Arm_left);
    }
}
