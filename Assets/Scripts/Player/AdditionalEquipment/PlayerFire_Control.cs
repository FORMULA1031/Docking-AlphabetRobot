using UnityEngine;
using UnityEngine.UI;

public class PlayerFire_Control : MonoBehaviour
{
    public GameObject Effect;   //�t�@�C���[�p�̃G�t�F�N�g
    GameObject Effect_Instance; //���������t�@�C���[�G�t�F�N�g
    GameObject Muzzle;  //��������t�@�C���[�G�t�F�N�g�̍��W�I�u�W�F�N�g
    int bullets_number = 15;    //�ϋv�l
    float revolution_time = 0f; //�ϋv�l�̌����̒x������
    Text WeaponNumber_text; //�\������ϋv�l�e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    int rotation_speed = 1; //��]���x
    bool leftrotation_flag = true;  //����]���邩�̃t���O
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Statu_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�t�@�C���[�p�[�c�̒ǉ�����
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
        Quaternion muzzle_quaternion = transform.rotation;
        Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
        Vector3 fireeffect_rotation = Effect_Instance.transform.localRotation.eulerAngles;
        fireeffect_rotation.y += 90;
        Effect_Instance.transform.localRotation = Quaternion.Euler(fireeffect_rotation);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = false;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        if (add_power != 0)
        {
            Effect_Instance.GetComponent<FireEffect_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Effect_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)  //�ϋv�l�̌���
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)    //�ϋv�l�������Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
            Destroy(Effect_Instance);
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������c��ϋv�l�̍X�V
    }

    private void FixedUpdate()
    {
        if (leftrotation_flag)  //����]����
        {
            if (transform.localEulerAngles.y >= 30 && transform.localEulerAngles.y < 90)
            {
                rotation_speed *= -1;
                leftrotation_flag = false;
            }
        }
        if (!leftrotation_flag) //�E��]����
        {
            if (transform.localEulerAngles.y <= 330 && transform.localEulerAngles.y > 270)
            {
                rotation_speed *= -1;
                leftrotation_flag = true;
            }
        }
        transform.Rotate(new Vector3(0, rotation_speed, 0));
        Effect_Instance.transform.position = Muzzle.transform.position;
        Effect_Instance.transform.rotation = Muzzle.transform.rotation;
        Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
        rotation.y += 90;
        Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    void Display_BulletsNumber()    //�\������c��ϋv�l�̍X�V
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (Effect_Instance != null)
        {
            Effect_Instance.GetComponent<FireEffect_Control>().Destroy_Flag();
        }
    }
}
