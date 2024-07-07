using UnityEngine;

public class CannonRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�e�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������e
    public GameObject cannonstreet_effect;  //�e�̔��ˌ�̉��G�t�F�N�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    float bullet_serialspeed = 0.5f;    //�U������܂ł̒x������
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    Quaternion original_angle;  //�v���C���[�����b�N�I�����Ă��Ȃ��ꍇ�̐�����

    // Start is called before the first frame update
    void Start()
    {
        original_angle = transform.rotation;
        Muzzle = transform.Find("Head/Muzzle").gameObject;
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
            if (bullet_serialspeed >= 1.5 && transform.position.z > Player.transform.position.z + 3f)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(true);
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
        if(other.gameObject.tag == "Player")    //�v���C���[�����b�N�I�������ɂ����ꍇ
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
