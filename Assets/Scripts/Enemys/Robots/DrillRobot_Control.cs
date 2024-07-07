using UnityEngine;

public class DrillRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�h�����G�t�F�N�g�𐶐�������W�I�u�W�F�N�g
    public GameObject Effect;   //�h�����G�t�F�N�g
    GameObject Effect_Instance; //���������h�����G�t�F�N�g
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    bool effect_flag = false;   //�h�����G�t�F�N�g�𐶐������t���O
    bool move_flag = false; //���I�u�W�F�N�g���ړ������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Head/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //�h�����G�t�F�N�g�𐶐�
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y -= 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)    //���������h�����G�t�F�N�g�̐���
        {
            Effect_Instance.transform.position = Muzzle.transform.position;
            if (Effect_Instance.GetComponent<DrillEffect_Control>().hit_flag)
            {
                gameObject.GetComponent<Status_Control>().Damage(100);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I�������ɂ����ꍇ
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-3);
                move_flag = true;
            }
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

    public void OnDestroy()
    {
        Destroy(Effect_Instance);
    }
}
