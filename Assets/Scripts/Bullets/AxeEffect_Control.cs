using UnityEngine;

public class AxeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    float time = 0; //���I�u�W�F�N�g�̑��݂��Ă��鎞��
    int power = 20; //���I�u�W�F�N�g�̍U����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()   //0.5�b�ȏ�o�߂���Ǝ��I�u�W�F�N�g���폜
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�
    {
        rb.velocity = new Vector3(10, rb.velocity.y, -3);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //�q�b�g����
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Destroy(gameObject);
        }
    }
}
