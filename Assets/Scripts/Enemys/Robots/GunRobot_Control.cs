using UnityEngine;

public class GunRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //���肩��̒e�𐶐�������W�I�u�W�F�N�g
    GameObject Muzzle2; //�E�肩��̒e�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������e
    public GameObject cannonstreet_effect;  //�e�̔��ˌ�̉��G�t�F�N�g
    float bullet_serialspeed = 0f;  //�U������܂ł̒x������
    float bullet_stoptime = 0f; //�e�̘A�ˑ��x
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_left/Muzzle").gameObject;
        Muzzle2 = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.4f && bullet_stoptime <= 1) //�e�̐����̏���
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);

                GameObject bullet_Instance2 = Instantiate(bullet, Muzzle2.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle2.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
            else if (bullet_stoptime >= 2)
            {
                bullet_stoptime = 0;
            }
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
