using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMissile_Control : MonoBehaviour
{
    int stamina = 10;   //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 0;  //�ϋv�l�̌����̒x������
    Slider slider;  //�ϋv�l�p�̃o�[
    GameObject Muzzle_left; //�����̃~�T�C�������p�̍��W
    GameObject Muzzle_right;    //�E���̃~�T�C�������p�̍��W
    public GameObject missile;  //��������~�T�C��
    public GameObject cannonstreet_effect;  //�~�T�C���̔��ˌ�̉��G�t�F�N�g
    bool pusharmbutton_flag = false;    //�A�[���{�^���������Ă��邩�̃t���O
    bool pushheadbutton_flag = false;   //�w�b�h�{�^���������Ă��邩�̃t���O

    // Start is called before the first frame update
    void Start()    //�~�T�C���p�[�c�̒ǉ�����
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
        Muzzle_left = transform.Find("Muzzle_left").gameObject;
        Muzzle_right = transform.Find("Muzzle_right").gameObject;

        EventTrigger.Entry entry = new EventTrigger.Entry();    //�A�[���{�^���ݒ�
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((x) => PushDown_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((x) => PushUp_ArmButton());
        GameObject.Find("Canvas/ArmButton").AddComponent<EventTrigger>().triggers.Add(entry);

        EventTrigger.Entry entry_head = new EventTrigger.Entry();   //�w�b�h�{�^���ݒ�
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
        if (Input.GetKey(KeyCode.S) || pushheadbutton_flag || Input.GetKey(KeyCode.A) || pusharmbutton_flag)    //�U������
        {
            if (serial_time >= 1.5f)    //�A�ˑ��x
            {
                Quaternion muzzle_quaternion = Muzzle_left.transform.rotation;
                muzzle_quaternion.y += 90;
                muzzle_quaternion.z += 50;
                GameObject Missile_Instance = Instantiate(missile, Muzzle_left.transform.position, muzzle_quaternion);
                Missile_Instance.GetComponent<Missile_Control>().Change_Power(120);
                Instantiate(cannonstreet_effect, Muzzle_left.transform.position, muzzle_quaternion);
                Missile_Instance = Instantiate(missile, Muzzle_right.transform.position, muzzle_quaternion);
                Missile_Instance.GetComponent<Missile_Control>().Change_Power(120);
                Instantiate(cannonstreet_effect, Muzzle_right.transform.position, muzzle_quaternion);
                serial_time = 0;
                stamina--;
            }
        }

        if (stamina <= 0)   //�ϋv�l�������Ȃ����ꍇ
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //�\������c��ϋv�l�̍X�V
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
}
