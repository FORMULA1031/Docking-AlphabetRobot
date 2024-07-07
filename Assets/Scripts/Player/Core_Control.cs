using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Core_Control : MonoBehaviour
{
    public GameObject bullet;   //��������e
    public GameObject cannonstreet_effect;  //�e�̔��ˌ�̉��G�t�F�N�g
    GameObject Arm_left;    //���I�u�W�F�N�g�̍��r
    GameObject Arm_right;   //���I�u�W�F�N�g�̉E�r
    GameObject Head;    //���I�u�W�F�N�g�̓�
    GameObject Backpack;    //���I�u�W�F�N�g�̃o�b�N�p�b�N
    bool addarm_flag = false;   //�ǉ��A�[���p�[�c�𑕔����Ă��邩�̃t���O
    bool addhead_flag = false;  //�ǉ��w�b�h�p�[�c�𑕔����Ă��邩�̃t���O
    bool addback_flag = false;  //�ǉ��o�b�N�p�b�N�p�[�c�𑕔����Ă��邩�̃t���O
    Text WeaponNumberArm_text;  //�\������A�[���p�[�c�p�̒e���e�L�X�g
    Text WeaponNumberHead_text; //�\������w�b�h�p�[�c�p�̒e���e�L�X�g
    bool pusharmbutton_flag = false;    //�A�[���{�^�������������̃t���O
    Slider slider;  //�ǉ��o�b�N�p�b�N�p�[�c�p�̑ϋv�l�Q�[�W
    Status_Control Status_Control;  //�v���C���[���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U����
    AudioSource AudioSource;    //���I�u�W�F�N�g�p��AudioSource
    public AudioClip mounting_se;   //�ǉ��p�[�c�̑�����
    public GameObject A_left;   //�A�b�N�X�p�[�c�̍��r
    public GameObject A_right;  //�A�b�N�X�p�[�c�̉E�r
    public GameObject B_left;   //�o�Y�[�J�p�[�c�̍��r
    public GameObject B_right;  //�o�Y�[�J�p�[�c�̉E�r
    public GameObject C;    //�L���m���p�[�c
    public GameObject D;    //�h�����p�[�c
    public GameObject E;    //�G���n���X�����g�p�[�c
    public GameObject F;    //�t�@�C���[�p�[�c
    public GameObject G_left;   //�K�g�����O�p�[�c�̍��r
    public GameObject G_right;  //�K�g�����O�p�[�c�̉E�r
    public GameObject H_left;   //�n���}�[�p�[�c�̍��r
    public GameObject H_right;  //�n���}�[�p�[�c�̉E�r
    public GameObject I;    //�C���r�W�u���p�[�c
    public GameObject J;    //�W�F�b�g�p�[�c
    public GameObject K_left;   //�i�C�t�p�[�c�̍��r
    public GameObject K_right;  //�i�C�t�p�[�c�̉E�r
    public GameObject L;    //���[�U�[�p�[�c
    public GameObject M;    //�~�T�C���p�[�c
    public GameObject N;    //�j�[�h���p�[�c
    public GameObject O;    //�n�b�g�\�p�[�c
    public GameObject P;    //�s�X�g���p�[�c
    public GameObject Q;    //�N�A���^���p�[�c
    public GameObject R;    //���y�A�p�[�c
    public GameObject S;    //�V�[���h�p�[�c
    public GameObject T;    //�e�[���p�[�c
    public GameObject U;    //���[�Y�A���[�p�[�c
    public GameObject V;    //�o���G�[�V�����p�[�c
    public GameObject W;    //�E�B���O�p�[�c
    public GameObject X;    //�G�N�X�g���p�[�c
    public GameObject Y;    //�G�N�X�g���p�[�c

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.Find("Head").gameObject;
        Backpack = transform.Find("Backpack/Backpack_Weapons").gameObject;
        Arm_left = transform.Find("Arm_left/Cylinder (1)").gameObject;
        Arm_right = transform.Find("Arm_right/Cylinder (1)").gameObject;
        WeaponNumberArm_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        WeaponNumberHead_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());    //�A�[���{�^���ݒ�
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Status_Control = gameObject.GetComponent<Status_Control>();
        AudioSource = GetComponent<AudioSource>();
        AddPart_load(); //�ݒ肵�Ă����ǉ��p�[�c�̑���
    }

    // Update is called once per frame
    void Update()
    {
        Key_Outputs();  //�v���C���[���쏈��
        if (!addarm_flag)   //�����A�[�������̒e���e�L�X�g
        {
            WeaponNumberArm_text.text = "-";
        }
        if (!addhead_flag)  //�����w�b�h�����̒e���e�L�X�g
        {
            WeaponNumberHead_text.text = "NONE";
        }
        if (!addback_flag)  //�����o�b�N�p�b�N�����̒e���e�L�X�g
        {
            slider.value = 0;
        }
        add_power = Status_Control.add_power;   //��������U���͂̍X�V
    }

    private void FixedUpdate()
    {
        if(transform.position.y < 0.7f) //���I�u�W�F�N�g�̈ړ�����
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0.701f, transform.position.z);
        }
    }

    void Key_Outputs()  //�v���C���[���쏈��
    {
        if (!addarm_flag)   //���������̃A�[���U������
        {
            if (Input.GetKey(KeyCode.A) || pusharmbutton_flag)
            {
                GameObject bullet_instance1 = Instantiate(bullet, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                GameObject bullet_instance2 = Instantiate(bullet, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                bullet_instance1.GetComponent<Bullet_Control>().Enhancement(add_power);
                bullet_instance2.GetComponent<Bullet_Control>().Enhancement(add_power);
                bullet_instance1.GetComponent<Bullet_Control>().Player_flag(true);
                bullet_instance2.GetComponent<Bullet_Control>().Player_flag(true);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
                Instantiate(cannonstreet_effect, new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z + 1), Quaternion.identity);
            }
        }
    }

    public  void PushDown_ArmButton()   //�A�[���{�^�����������ꍇ
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = true;
        }
    }

    public void PushUp_ArmButton()  //�A�[���{�^���𗣂����ꍇ
    {
        if (!addarm_flag)
        {
            pusharmbutton_flag = false;
        }
    }

    public void AdditionalEquipment(string position, GameObject weapon , GameObject weapon2)    //�ǉ������̑�������
    {
        AudioSource.PlayOneShot(mounting_se);
        switch (position)
        {
            case "arm": //�A�[�������̏ꍇ
                GameObject Add_ArmWeapon_L = Instantiate(weapon, Arm_left.transform.position, Quaternion.identity);
                Add_ArmWeapon_L.transform.parent = Arm_left.transform;
                Vector3 rotation = Add_ArmWeapon_L.transform.localRotation.eulerAngles;
                if (weapon != weapon2)
                {
                    rotation.x -= 90;
                    rotation.y -= 90;
                    rotation.z -= 90;
                }
                Add_ArmWeapon_L.transform.localRotation = Quaternion.Euler(rotation);
                GameObject Add_ArmWeapon_R = Instantiate(weapon2, Arm_right.transform.position, Quaternion.identity);
                Add_ArmWeapon_R.transform.parent = Arm_right.transform;
                addarm_flag = true;
                break;
            case "head":    //�w�b�h�����̏ꍇ
                GameObject Add_HeadWeapon = Instantiate(weapon, Head.transform.position, Quaternion.identity);
                Add_HeadWeapon.transform.parent = Head.transform;
                addhead_flag = true;
                break;
            case "backpack":    //�o�b�N�p�b�N�����̏ꍇ
                GameObject Add_BackpackWeapon = Instantiate(weapon, Backpack.transform.position, Quaternion.identity);
                Add_BackpackWeapon.transform.parent = Backpack.transform;
                addback_flag = true;
                break;
        }
    }

    public void CastOf(string position) //�ǉ������̃p�[�W
    {
        switch (position)
        {
            case "arm":
                addarm_flag = false;
                break;
            case "head":
                addhead_flag = false;
                break;
            case "backpack":
                addback_flag = false;
                break;
        }
    }

    private void AddPart_load() //�ݒ肵�Ă����ǉ������̑�������
    {
        switch (Player_Settings.head_setting)   //�w�b�h����
        {
            case "C":
                AdditionalEquipment("head", C, null);
                break;
            case "D":
                AdditionalEquipment("head", D, null);
                break;
            case "F":
                AdditionalEquipment("head", F, null);
                break;
            case "O":
                AdditionalEquipment("head", O, null);
                break;
            case "L":
                AdditionalEquipment("head", L, null);
                break;
            case "R":
                AdditionalEquipment("head", R, null);
                break;
        }

        switch (Player_Settings.arm_setting)    //�A�[������
        {
            case "A":
                AdditionalEquipment("arm", A_left, A_right);
                break;
            case "B":
                AdditionalEquipment("arm", B_left, B_right);
                break;
            case "G":
                AdditionalEquipment("arm", G_left, G_right);
                break;
            case "K":
                AdditionalEquipment("arm", K_left, K_right);
                break;
            case "P":
                AdditionalEquipment("arm", P, P);
                break;
            case "H":
                AdditionalEquipment("arm", H_left, H_right);
                break;
            case "N":
                AdditionalEquipment("arm", N, N);
                break;
        }

        switch (Player_Settings.backpack_setting)   //�o�b�N�p�b�N����
        {
            case "E":
                AdditionalEquipment("backpack", E, null);
                break;
            case "I":
                AdditionalEquipment("backpack", I, null);
                break;
            case "J":
                AdditionalEquipment("backpack", J, null);
                break;
            case "M":
                AdditionalEquipment("backpack", M, null);
                break;
            case "S":
                AdditionalEquipment("backpack", S, null);
                break;
            case "T":
                AdditionalEquipment("backpack", T, null);
                break;
            case "W":
                AdditionalEquipment("backpack", W, null);
                break;
        }
    }
}
