using UnityEngine;

public class CannonBullet_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    public int speed;   //���I�u�W�F�N�g�̑��x
    public int power;   //���I�u�W�F�N�g�̍U����
    float launch_time = 0;  //���I�u�W�F�N�g�����݂��Ă��鎞��
    bool induction_flag = false;    //���I�u�W�F�N�g���U�����邩�̃t���O
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    bool hit_flag = false;  //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG�������̃t���O
    bool enhancement_flag = false;  //���I�u�W�F�N�g���������邩�̃t���O
    bool player_flag = false;   //���I�u�W�F�N�g���v���C���[�ɂ���Đ������ꂽ���̃t���O
    int speed_add = 0;  //���������X�s�[�h��
    public float endurance_value;   //���I�u�W�F�N�g�̑ϋv�l
    bool invincible_flag = false;   //���I�u�W�F�N�g�����G���̃t���O
    float invincible_time = 0.0f;   //���I�u�W�F�N�g�̖��G����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        if (!player_flag && Player != null && induction_flag)   //�U������G�̒e�������ꍇ
        {
            transform.LookAt(Player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jet_flag(); //�W�F�b�g�𑕔����̏���
        launch_time += Time.deltaTime;
        if (induction_flag && Player != null)   //�^�[�Q�b�g�ւ̗U���̏���
        {
            if (transform.position.z > Player.transform.position.z + 1.5f)
            {
                Vector3 relativePos = Player.transform.position - this.transform.position;
                // �������A��]���ɕϊ�
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, relativePos.y - 1f, relativePos.z));
                // ���݂̉�]���ƁA�^�[�Q�b�g�����̉�]����⊮����
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.003f);
            }
            if(transform.position.z < Player.transform.position.z - 1f)
            {
                Destroy(gameObject);
            }
        }
        if (launch_time >= 4)   //4�b�ȏ�o�߂���ƍ폜
        {
            Destroy(gameObject);
        }

        if (invincible_flag)    //���G��Ԃ̏���
        {
            invincible_time += Time.deltaTime;
            if (invincible_time >= 0.1f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�
    {
        rb.velocity = transform.forward * (speed + speed_add);
    }

    public void Player_flag(bool flag)  //�v���C���[�ɂ���Ă̐��䂾�����ꍇ
    {
        player_flag = true;
    }

    public void Induction(bool flag)    //�U�����邩�̏���
    {
        induction_flag = flag;
    }

    public void Jet_flag()  //�W�F�b�g�𑕔����̏���
    {
        if (Player != null)
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

    public void Enhancement(int _add_power) //���I�u�W�F�N�g�̋�������
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //�����q�b�g����
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
        else if (other.gameObject.tag == "Beam" && !invincible_flag)    //�r�[���Ƃ̐ڐG����
        {
            if (other.gameObject.GetComponent<CannonBullet_Control>() != null)
            {
                endurance_value -= other.gameObject.GetComponent<CannonBullet_Control>().power;
            }
            if (endurance_value <= 0)
            {
                Destroy(gameObject);
            }
            invincible_flag = true;
        }
        else
        {
            if (other.gameObject.name != "Hammer(Clone)")   //�n���}�[�ƐڐG����
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Barrier")   //��O�ɓ������ꍇ
        {
            Destroy(gameObject);
        }
    }
}
