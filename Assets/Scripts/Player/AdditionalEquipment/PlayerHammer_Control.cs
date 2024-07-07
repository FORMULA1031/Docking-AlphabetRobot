using UnityEngine;
using UnityEngine.UI;

public class PlayerHammer_Control : MonoBehaviour
{
    public GameObject Hammer;   //��������n���}�[
    GameObject Muzzle;  //��������n���}�[�̍��W�I�u�W�F�N�g
    GameObject Hammer_Instance; //���������n���}�[
    GameObject Hammer_grip; //�n���}�[�̈��蕔��
    float revolution_time = 0.0f;   //�ϋv�l�̌����̒x������
    int bullets_number = 20;    //�ϋv�l
    Text WeaponNumber_text; //�\������ϋv�l�e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l
    float speed = -150.0f;  //���쑬�x
    bool leftrotation_flag = true;  //����]���邩�̃t���O

    // Start is called before the first frame update
    void Start()    //�n���}�[�p�[�c�̒ǉ�����
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
        Muzzle = transform.Find("Hammer/Muzzle").gameObject;
        Hammer_grip = transform.Find("Hammer").gameObject;
        if (gameObject.name == "Hammer_Player(Clone)")
        {
            Vector3 rotation = this.transform.localRotation.eulerAngles;
            rotation.x -= 90;
            rotation.y -= 90;
            rotation.z -= 90;
            transform.localRotation = Quaternion.Euler(rotation);
        }
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
        GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = false;
        Instance_Bullets();
    }

    // Update is called once per frame
    void Update()
    {
        Hammer_Instance.transform.position = Muzzle.transform.position; //���������n���}�[�̈ʒu�p�x�̍X�V
        Hammer_Instance.transform.rotation = Muzzle.transform.rotation;
        Vector3 rotation_right = Hammer_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        Hammer_Instance.transform.localRotation = Quaternion.Euler(rotation_right);

        if (leftrotation_flag)  //����]����
        {
            if (Hammer_grip.transform.localEulerAngles.z > 250 && Hammer_grip.transform.localEulerAngles.z <= 280)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        if (!leftrotation_flag) //�E��]����
        {
            if (Hammer_grip.transform.localEulerAngles.z >= 350 && Hammer_grip.transform.localEulerAngles.z < 360)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
            else if (Hammer_grip.transform.localEulerAngles.z >= 0 && Hammer_grip.transform.localEulerAngles.z < 20)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        Hammer_grip.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        if (add_power != 0)
        {
            Hammer_Instance.GetComponent<Hammer_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Hammer_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)  //�ϋv�l�̌���
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)    //�ϋv�l���Ȃ��Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������c��ϋv�l�̍X�V
    }

    void Instance_Bullets() //�n���}�[��������
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.00f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        Hammer_Instance = Instantiate(Hammer, Muzzle.transform.position, muzzle_quaternion);
    }

    void Display_BulletsNumber()    //�\������c��ϋv�l�̍X�V
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (GameObject.Find("Canvas/ArmButton") != null)
        {
            GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = true;
        }
        Destroy(Hammer_Instance);
    }
}
