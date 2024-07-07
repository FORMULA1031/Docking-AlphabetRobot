using UnityEngine;

public class VariationRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    int rotation_speed = 0; //���I�u�W�F�N�g�𑬓x
    float atack_time = 0;   //�U���܂ł̒x������
    float jump_time = 0;    //�W�����v����܂ł̒x������
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    bool direction_left = true; //���ֈړ����邩�̃t���O
    GameObject Muzzle;  //�e�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������e
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    GameFinish GameFinish;  //EventSystem���R���|�[�l���g���Ă���GameFinish�X�N���v�g
    bool invisible_flag = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        GameFinish = GameObject.Find("EventSystem").GetComponent<GameFinish>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) //�v���C���[�����b�N�I�����鏈��
        {
            if (Player.transform.position.z >= 255)
            {
                lockon_flag = true;
            }
        }
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            atack_time += Time.deltaTime;
            jump_time += Time.deltaTime;

            if (atack_time >= 2 && !invisible_flag) //�e�̐���
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(true);
                atack_time = 0;
            }

            if(jump_time >= 3)  //�W�����v����
            {
                rb.AddForce(transform.up * 5000, ForceMode.Impulse);
                jump_time = 0;
            }

            if (direction_left && transform.position.x > -2.5f) //�ړ�����
            {
                rotation_speed = -5;
            }
            else if (direction_left)
            {
                direction_left = false;
            }
            if (!direction_left && transform.position.x < 2.5f)
            {
                rotation_speed = 5;
            }
            else if (!direction_left)
            {
                direction_left = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            rb.velocity = new Vector3(rotation_speed, rb.velocity.y, rotation_speed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            invisible_flag = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            invisible_flag = true;
        }
    }

    public void OnDestroy()
    {
        GameFinish.GameOver(true);
    }
}
