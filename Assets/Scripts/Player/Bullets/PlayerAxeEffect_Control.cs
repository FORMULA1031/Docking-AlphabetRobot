using UnityEngine;

public class PlayerAxeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    float time = 0; //���݂��Ă��鎞��
    int power = 100;    //�U����
    int speed = 0;  //���x
    bool enhancement_flag = false;  //���������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Player.GetComponent<Status_Control>().Invincible(true); //���I�u�W�F�N�g�����݂��Ă������v���C���[�𖳓G��Ԃɂ���
        time += Time.deltaTime;
        if (time >= 0.5f)   //���Ԍo�߂Ŏ��I�u�W�F�N�g���폜
        {
            Player.GetComponent<Status_Control>().Invincible(false);
            Destroy(gameObject);
        }
        speed = Player.GetComponent<Status_Control>().speed;    //�ǉ����鑬�x�̍X�V
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�����
    {
        rb.velocity = new Vector3(-10, rb.velocity.y, speed);
    }

    public void Enhancement(int _add_power) //��������ꍇ
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Enemy")    //�G�ɒ��������ꍇ
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Player.GetComponent<Status_Control>().Invincible(false);
            Destroy(gameObject);
        }
    }
}
