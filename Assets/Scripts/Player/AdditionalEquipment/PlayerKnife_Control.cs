using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerKnife_Control : MonoBehaviour
{
    public GameObject knife_effect; //��������a���G�t�F�N�g
    GameObject Muzzle;  //�a���G�t�F�N�g�𐶐�������W�I�u�W�F�N�g
    float bullet_serialspeed = 1f;  //�a���G�t�F�N�g�𐶐�����x������
    bool action_flag = false;   //�U���\���̃t���O
    int bullets_number = 15;    //�e��
    Text WeaponNumber_text; //�\������U���񐔃e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    GameObject Arm_left;    //�v���C���[�̍��r
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�i�C�t�p�[�c�̒ǉ�����
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
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Knife_Player_L(Clone)").gameObject;
        if (transform.parent.gameObject.transform.parent.gameObject.name == "Arm_right")
        {
            action_flag = true;
        }
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        if (action_flag)
        {
            bullet_serialspeed += Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || pushbutton_flag) //�U������
            {
                if (bullet_serialspeed >= 0.3f) //�A�ˑ��x
                {
                    Instance_Effect();
                    bullet_serialspeed = 0f;
                    bullets_number--;
                }
            }
            Display_BulletsNumber();    //�\������U���̎c��񐔂̍X�V
        }
        if (bullets_number <= 0)    //
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
    }

    void Instance_Effect()  //�a���G�t�F�N�g�̐�������
    {
        GameObject Effect_Instance = Instantiate(knife_effect, Muzzle.transform.position, Quaternion.identity);
        Effect_Instance.GetComponent<PlayerKnifeEffect_Control>().Enhancement(add_power);
    }

    void Display_BulletsNumber()    //�\������U���̎c��񐔂̍X�V
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
