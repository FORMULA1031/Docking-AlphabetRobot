using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerGun_Control : MonoBehaviour
{
    public GameObject bullet;   //��������e
    public GameObject cannonstreet_effect;  //�e�̔��ˌ�̉��G�t�F�N�g
    GameObject Muzzle;  //��������e�̍��W�I�u�W�F�N�g
    float bullet_serialspeed = 0.6f;    //�e�𐶐�����x������
    bool firstbullet_flag = false;  //�e��1���ڂ����������̃t���O
    bool secondbullet_flag = false; //�e��2���ڂ����������̃t���O
    int bullets_number = 20;    //�e�̒e��
    Text WeaponNumber_text; //�\������e���e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    bool pushbutton_flag = false;   //�U���{�^���������Ă��邩�̃t���O
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�s�X�g���p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
        Transform[] brotrans = new Transform[parent.childCount];
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if(parent.GetChild(i).gameObject != gameObject)
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
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;
        bullet_serialspeed += Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || pushbutton_flag) //�U������
        {
            if (bullet_serialspeed >= 0.6f && !firstbullet_flag)    //1���ڂ̒e�𐶐�
            {
                Instance_Bullets();
                firstbullet_flag = true;
                bullet_serialspeed = 0.6f;
                bullets_number--;
            }
            else if (bullet_serialspeed >= 0.8f && !secondbullet_flag)  //2���ڂ̒e�𐶐�
            {
                Instance_Bullets();
                secondbullet_flag = true;
                bullets_number--;
            }
        }
        if (secondbullet_flag)
        {
            bullet_serialspeed = 0;
            firstbullet_flag = false;
            secondbullet_flag = false;
        }
        if(bullets_number <= 0) //�c��e���������Ȃ����ꍇ
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
        bullet_Instance.GetComponent<Bullet_Control>().Player_flag(true);
        bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
        bullet_Instance.GetComponent<Bullet_Control>().Enhancement(add_power);
        Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
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
}
