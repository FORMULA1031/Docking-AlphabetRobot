using UnityEngine;
using UnityEngine.SceneManagement;

public class Status_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    public int stamina; //�ϋv�l
    public int stamina_max; //�ϋv�l�̍ő�l
    public GameObject Explosion;    //�����G�t�F�N�g
    public GameObject DropItem; //���I�u�W�F�N�g�����Ƃ��A�C�e��
    FixedJoystick joystick; //�v���C���[�̉��ړ��𐧌䂷��W���C�X�e�B�b�N
    int random_number;  //�A�C�e���𗎂Ƃ������_���Ȑ�
    public int speed;   //���݂̑��x
    public int original_speed;  //�ʏ펞�̑��x
    int rotation_speed = 0; //���񑬓x
    int add_rotationspeed = 0;  //�ǉ�������񑬓x
    public int add_power = 0;   //�ǉ�����U����
    public int original_addpower = 0;   //�ʏ펞�̍U����
    GameFinish GameFinish;  //EventSystem���R���|�[�l���g���Ă���GameFinish�X�N���v�g
    float invincible_time = 0;  //���G����
    bool invincible_flag = false;   //���G��Ԃ��̃t���O
    AudioSource AudioSource;    //EventSystem���R���|�[�l���g���Ă���AudioSource
    public AudioClip damage_se; //�_���[�W���󂯂�SE
    public bool speedup_flag = false;   //���x���㏸���Ă邩�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        random_number = Random.Range(1, 3);
        stamina_max = stamina;
        original_speed = speed;
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�Q�[���J�n�p�̐ݒ�
        {
            AudioSource = GameObject.Find("EventSystem").GetComponent<AudioSource>();
            joystick = GameObject.Find("Canvas/FixedJoystick").GetComponent<FixedJoystick>();
            GameFinish = GameObject.Find("EventSystem").GetComponent<GameFinish>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(stamina > stamina_max)   //�ϋv�l���J���X�g������
        {
            stamina = stamina_max;
        }
        if(stamina <= 0)    //�ϋv�l�������Ȃ����ꍇ
        {
            if(DropItem != null && random_number == 1)  //�A�C�e�����h���b�v������
            {
                Instantiate(DropItem, transform.position, Quaternion.identity);
            }
            if(gameObject.tag == "Player")  //�v���C���[�̑ϋv�l�������Ȃ����ꍇ
            {
                GameFinish.GameOver(false);
            }
            else if(gameObject.tag == "Enemy")  //�G�̑ϋv�l�������Ȃ����ꍇ
            {
                GameFinish.Defeated();
            }
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (invincible_flag)    //���G��ԏ���
        {
            invincible_time += Time.deltaTime;
            if(invincible_time >= 0.01f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
        Key_Outputs();  //�v���C���[���쏈��
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�����
    {
        if (gameObject.tag == "Player") //���I�u�W�F�N�g���v���C���[�I�u�W�F�N�g�������ꍇ
            rb.velocity = new Vector3(rotation_speed, rb.velocity.y, speed);
        else
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }

    void Key_Outputs()  //�v���C���[���쏈��
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�Q�[���J�n���������ꍇ
        {
            if (gameObject.tag == "Player") //���I�u�W�F�N�g���v���C���[�I�u�W�F�N�g�������ꍇ
            {
                if (joystick.Horizontal < 0 && transform.position.x > -2.5f)    //������
                {
                    rotation_speed = -5 - add_rotationspeed;
                }
                else if (joystick.Horizontal > 0 && transform.position.x < 2.5f)    //�E����
                {
                    rotation_speed = 5 + add_rotationspeed;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -2.5f)   //������
                {
                    rotation_speed = -5 - add_rotationspeed;
                }
                else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 2.5f)   //�E����
                {
                    rotation_speed = 5 + add_rotationspeed;
                }
                else
                {
                    rotation_speed = 0;
                }
            }
        }
    }

    public void Stamina_Repair(int repair_amount)   //���I�u�W�F�N�g�̑ϋv�l�񕜏���
    {
        stamina += repair_amount;
    }

    public void Damage(int damage_amount)   //���I�u�W�F�N�g���_���[�W���󂯂�����
    {
        if (!invincible_flag)   //���G��Ԃł͂Ȃ��ꍇ
        {
            stamina -= damage_amount;
            invincible_flag = true;
            AudioSource.PlayOneShot(damage_se);
            if (gameObject.tag == "Player") //���I�u�W�F�N�g���v���C���[�I�u�W�F�N�g�������ꍇ
            {
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<MainCamera_Control>().Shake(0.3f, 0.4f));    //��ʂ�h�炷
            }
        }
    }

    public void Invincible(bool flag)   //���G��Ԃɂ��鏈��
    {
        if (flag)
        {
            invincible_flag = true;
        }
    }

    public void Add_Speed(int _speed)   //���x�̏㏸����
    {
        speed += _speed;
        add_rotationspeed = _speed;
        speedup_flag = true;
    }

    public void Return_Speed()  //���x��ʏ펞�̑��x�ɖ߂�
    {
        speed = original_speed;
        add_rotationspeed = 0;
        speedup_flag = false;
    }

    public void Add_Power(int _power)   //�U���͂��������鏈��
    {
        add_power += _power;
    }

    public void Return_Power()  //�U���͂�ʏ펞�ɖ߂�
    {
        add_power = original_addpower;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!invincible_flag)   //���G��Ԃł͂Ȃ��ꍇ
        {
            if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")   //�v���C���[�I�u�W�F�N�g�̐ڐG����
            {
                stamina -= 10;
                invincible_flag = true;
                AudioSource.PlayOneShot(damage_se);
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<MainCamera_Control>().Shake(0.3f, 0.4f));    //�J������h�炷
            }
            if (other.gameObject.tag == "Player")   //�G�̐ڐG����
            {
                stamina = 0;
                invincible_flag = true;
            }
        }
    }
}
