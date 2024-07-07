using UnityEngine;

public class AxeRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�a���G�t�F�N�g�𐶐�������W�I�u�W�F�N�g
    public GameObject Effect;   //�a���G�t�F�N�g
    GameObject Effect_Instance; //���������a���G�t�F�N�g
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    bool move_flag = false; //���I�u�W�F�N�g���ړ������̃t���O
    float atack_time = 0;   //�U������܂ł̒x������

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            atack_time += Time.deltaTime;
            if (atack_time >= 1)    //�U������
            {
                Quaternion muzzle_quaternion = transform.rotation;
                Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
                Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
                rotation.y -= 90;
                Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
                atack_time = 0;
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
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I���������痣�ꂽ�ꍇ
        {
            lockon_flag = false;
        }
    }

    public void OnDestroy()
    {
        Destroy(Effect_Instance);
    }
}
