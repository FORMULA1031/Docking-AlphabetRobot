using UnityEngine;

public class HatRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�n�b�g�̐��ʒu
    public GameObject Hat;  //�n�b�g
    GameObject Hat_Instance;    //���������n�b�g
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    float serial_time = 0;  //�U������܂ł̒x������

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Head/Muzzle").gameObject;
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            serial_time += Time.deltaTime;
        }
        if (Hat_Instance.GetComponent<Hat_Control>().hit_flag)  //�n�b�g�̐��ʒu����
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
        }
    }

    private void FixedUpdate()
    {
        if (!lockon_flag)
        {
            Hat_Instance.transform.position = Muzzle.transform.position;
        }

        if (lockon_flag)    //�U�����̃n�b�g�̏���
        {
            if (serial_time >= 2 && serial_time < 3.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * 8f * Time.deltaTime;
            }
            else if (serial_time >= 3.0f && serial_time < 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * -8f * Time.deltaTime;
            }
            else if(serial_time >= 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Hat_Instance.transform.position = Muzzle.transform.position;
                serial_time = 0;
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

    public void OnDestroy()
    {
        Destroy(Hat_Instance);
    }
}
