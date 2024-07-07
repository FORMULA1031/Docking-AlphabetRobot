using UnityEngine;

public class KnifeRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    GameObject Muzzle;  //�a���G�t�F�N�g�𐶐�������W�I�u�W�F�N�g
    public GameObject Effect;   //�a���G�t�F�N�g
    GameObject Effect_Instance; //���������a���G�t�F�N�g
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    bool effect_flag = false;   //�a���G�t�F�N�g�𐶐��������̃t���O
    float jump_time = 0;    //�W�����v����܂ł̒x������

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //�a���G�t�F�N�g�𐶐�
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Effect_Instance.transform.parent = Muzzle.transform;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            jump_time += Time.deltaTime;
            if (jump_time >= 2f)
            {
                rb.AddForce(transform.up * 8000, ForceMode.Impulse);
                jump_time = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            transform.Rotate(new Vector3(0, -5f, 0));
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
        Destroy(Effect_Instance);
    }
}
