using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBazooka_Control : MonoBehaviour
{
    public GameObject bullet;   //��������e
    GameObject Muzzle;  //��������e�̍��W�I�u�W�F�N�g
    float bullet_serialspeed = 1.5f;    //�e�𐶐�����x������
    int bullets_number = 3; //�e��
    Text WeaponNumber_text; //�\������c��e���̍X�V
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    GameObject Arm_left;    //�v���C���[�I�u�W�F�N�g�̍��r
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�o�Y�[�J�p�[�c�̒ǉ�����
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
        Arm_left = gameObject.transform.root.gameObject.transform.Find("Arm_left/Cylinder (1)/Bazooka_Player_L(Clone)").gameObject;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || pushbutton_flag) //�U������
        {
            if (bullet_serialspeed >= 1.5f)
            {
                Instance_Bullets(); //�e�̐���
                bullet_serialspeed = 0.0f;
                bullets_number--;
            }
        }
        if (bullets_number <= 0)    //�c��e���������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������c��e���̍X�V
    }

    void Instance_Bullets() //�e�̐�������
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.03f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
        bullet_Instance.GetComponent<CannonBullet_Control>().Player_flag(true);
        bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
        bullet_Instance.GetComponent<CannonBullet_Control>().Enhancement(add_power);
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
