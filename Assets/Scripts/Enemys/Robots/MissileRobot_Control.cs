using UnityEngine;

public class MissileRobot_Control : MonoBehaviour
{
    GameObject Muzzle_left; //�����ɐ�������~�T�C���̍��W�I�u�W�F�N�g
    GameObject Muzzle_right;    //�E���ɐ�������~�T�C���̍��W�����W�I�u�W�F�N�g
    public GameObject bullet;   //��������~�T�C��
    public GameObject cannonstreet_effect;  //�~�T�C���̔��ˌ�̉��G�t�F�N�g
    float bullet_serialspeed = 2f;  //�U������܂ł̒x������
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        Muzzle_left = transform.Find("Backpack/Missile/Muzzle_left").gameObject;
        Muzzle_right = transform.Find("Backpack/Missile/Muzzle_right").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 3.0f) //�~�T�C�����˂̏���
            {
                Quaternion muzzle_quaternion = Muzzle_left.transform.rotation;
                muzzle_quaternion.y += 90;
                muzzle_quaternion.z -= 30;
                Instantiate(bullet, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(bullet, Muzzle_right.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_right.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
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
