using UnityEngine;

public class Bullet_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    public int speed;   //�ړ����x
    public int power;   //�U��
    float launch_time = 0;  //���݂��Ă��鎞��
    bool induction_flag = false;    //�U�����邩�̃t���O
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    bool hit_flag = false;  //���̃I�u�W�F�N�g�ƐڐG�������̃t���O
    bool enhancement_flag = false;  //���I�u�W�F�N�g���������邩�̃t���O
    bool player_flag = false;   //�v���C���[�ɂ���Đ������ꂽ���̃t���O
    int speed_add = 0;  //�ǉ�����@���͒l
    public float endurance_value;   //�ϋv�l
    bool invincible_flag = false;   //���G���̃t���O
    float invincible_time = 0.0f;   //���G����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        if (!player_flag && Player != null && induction_flag)   //�������Ƀv���C���[�̕��֌���
        {
            transform.LookAt(Player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jet_flag(); //�W�F�b�g�p�[�c�𑕔����Ă���Ƃ��̏���
        launch_time += Time.deltaTime;
        if (induction_flag && Player != null)   //�U������
        {
            if (transform.position.z > Player.transform.position.z + 1.5f)
            {
                Vector3 relativePos = Player.transform.position - this.transform.position;
                // �������A��]���ɕϊ�
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, relativePos.y - 1f, relativePos.z));
                // ���݂̉�]���ƁA�^�[�Q�b�g�����̉�]����⊮����
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.003f);
            }
            if (transform.position.z < Player.transform.position.z - 1f)    //�v���C���[�̌��ɂ����ꍇ
            {
                Destroy(gameObject);
            }
        }
        if (launch_time >= 4)   //���Ԍo�߂Ŏ��I�u�W�F�N�g�̍폜
        {
            Destroy(gameObject);
        }

        if (invincible_flag)    //���G���ԏ���
        {
            invincible_time += Time.deltaTime;
            if(invincible_time >= 0.1f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�����
    {
        rb.velocity = transform.forward * (speed + speed_add);
    }

    public void Player_flag(bool flag)  //�v���C���[�ɂ���Đ������ꂽ�ꍇ
    {
        player_flag = true;
    }

    public void Induction(bool flag)    //�U�����邩�����߂�
    {
        induction_flag = flag;
    }

    private void Jet_flag() //�W�F�b�g�p�[�c�𑕔����Ă���ꍇ
    {
        if (Player != null) //�@���͏㏸
        {
            if (Player.GetComponent<Status_Control>().speedup_flag)
            {
                speed_add = 3;
            }
            else
            {
                speed_add = 0;
            }
        }
    }

    public void Enhancement(int _add_power) //�U���͏㏸
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //��������
        {
            if (!hit_flag)
            {
                if (other.gameObject.GetComponent<Status_Control>() != null)
                {
                    other.gameObject.GetComponent<Status_Control>().Damage(power);
                    Destroy(gameObject);
                    hit_flag = true;
                }
            }
        }
        else if(other.gameObject.tag == "Bullet" && !invincible_flag)   //���e��ڐG�����ꍇ
        {
            if (other.gameObject.GetComponent<Bullet_Control>() != null)    //�ϋv�l�̌���
            {
                endurance_value -= other.gameObject.GetComponent<Bullet_Control>().power;
            }
            if(endurance_value <= 0)    //�ϋv�l�������Ȃ����ꍇ
            {
                Destroy(gameObject);
            }
            invincible_flag = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")  //�o���A����̂���I�u�W�F�N�g�ƐڐG�����ꍇ
        {
            Destroy(gameObject);
        }
    }
}
