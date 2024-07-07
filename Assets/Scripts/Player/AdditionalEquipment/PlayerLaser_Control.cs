using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerLaser_Control : MonoBehaviour
{
    public GameObject bullet;   //��������e
    GameObject Muzzle;  //��������e�̍��W�I�u�W�F�N�g
    int bullets_number = 3; //�e��
    float bullet_serialspeed = 1f;  //�A�ˑ��x
    Text WeaponNumber_text; //�\������e���e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //���[�U�[�p�[�c�̒ǉ�����
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
        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || pushbutton_flag) //�U������
        {
            if (bullet_serialspeed >= 1f)   //�A�ˑ��x
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.x = 0.05f;
                muzzle_quaternion.y = 0;
                muzzle_quaternion.z = 0;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Player_flag(true);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_Instance.GetComponent<CannonBullet_Control>().Enhancement(add_power);
                bullet_serialspeed = 0;
                bullets_number--;
            }
        }

        if (bullets_number <= 0)    //�e���������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������e���e�L�X�g�̍X�V
    }

    void Display_BulletsNumber()    //�\������e���e�L�X�g�̍X�V
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    public void PushDown_Button()   //�{�^���������Ă���ꍇ
    {
        pushbutton_flag = true;
    }

    public void PushUp_Button() //�{�^���𗣂����ꍇ
    {
        pushbutton_flag = false;
    }
}
