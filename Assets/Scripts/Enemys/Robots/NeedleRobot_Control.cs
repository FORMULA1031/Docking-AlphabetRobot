using UnityEngine;

public class NeedleRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�j�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������j
    public GameObject cannonstreet_effect;  //�j�̔��ˌ�̉��G�t�F�N�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    float bullet_serialspeed = 0.5f;    //�U������܂ł̒x������
    float bullet_stoptime = 0f; //�j�̘A�ˑ��x
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    Quaternion original_angle;  //���I�u�W�F�N�g�̐�����

    // Start is called before the first frame update
    void Start()
    {
        original_angle = transform.rotation;
        Muzzle = transform.Find("Arm_left/Muzzle").gameObject;
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && Player != null)  //�v���C���[�����b�N�I�������ꍇ
        {
            this.transform.LookAt(Player.transform);
            Vector3 rotation = this.transform.localRotation.eulerAngles;
            rotation.x = 0;
            rotation.y -= 90;
            rotation.x -= 5;
            transform.localRotation = Quaternion.Euler(rotation);
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.5f && bullet_stoptime <= 1) //�j�̐���
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(true);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
        }
        else
        {
            transform.rotation = original_angle;
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
