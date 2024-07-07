using UnityEngine;

public class WingRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    GameObject Muzzle;  //�e�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������e
    GameObject Wing_right;  //�E��
    GameObject Wing_left;   //����
    float bullet_serialspeed = 2.0f;    //�U������܂ł̒x������
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    bool firing_flag = true;    //���ł悢���̃t���O
    bool leftrotation_flag = true;  //����]���邩�̃t���O
    float speed = 0.8f; //���I�u�W�F�N�g�̑��x

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Wing_right = transform.Find("Backpack/Wing/Wing_right").gameObject;
        Wing_left = transform.Find("Backpack/Wing/Wing_left").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 2.0f) //�e�̐���
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_serialspeed = 0;
                firing_flag = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!leftrotation_flag) //���̏���
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
        if (!firing_flag)   //��ԏ���
        {
            rb.AddForce(transform.up * 8000, ForceMode.Impulse);
            firing_flag = true;
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
}
