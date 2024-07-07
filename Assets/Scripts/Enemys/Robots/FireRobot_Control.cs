using UnityEngine;

public class FireRobot_Control : MonoBehaviour
{
    GameObject Head;    //�q�I�u�W�F�N�g��Head�I�u�W�F�N�g
    GameObject Muzzle;  //���𐶐�������W�I�u�W�F�N�g
    public GameObject Effect;   //���G�t�F�N�g
    GameObject Effect_Instance; //�����������G�t�F�N�g
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    int rotation_speed = 1; //���I�u�W�F�N�g�̑��x
    bool leftrotation_flag = true;  //����]���邩�̃t���O
    bool effect_flag = false;   //���G�t�F�N�g�𐶐��������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.Find("Head").gameObject;
        Muzzle = transform.Find("Head/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)
        {
            if (leftrotation_flag)  //Head�I�u�W�F�N�g������]���鏈��
            {
                if (Head.transform.localEulerAngles.y >= 30 && Head.transform.localEulerAngles.y < 90)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if (!leftrotation_flag) //Head�I�u�W�F�N�g���E��]���鏈��
            {
                if (Head.transform.localEulerAngles.y <= 330 && Head.transform.localEulerAngles.y > 270)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = true;
                }
            }
            Head.transform.Rotate(new Vector3(0, rotation_speed, 0));
            Effect_Instance.transform.position = Muzzle.transform.position;
            Effect_Instance.transform.rotation = Head.transform.rotation;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I�������ɂ����ꍇ
        {
            lockon_flag = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I�������ɂ��Ȃ��ꍇ
        {
            lockon_flag = false;
        }
    }

    public void OnDestroy()
    {
        Destroy(Effect_Instance);
    }
}
