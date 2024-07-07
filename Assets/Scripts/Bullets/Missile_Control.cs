using UnityEngine;

public class Missile_Control : MonoBehaviour
{
    private Rigidbody rb;   //���I�u�W�F�N�g�p��Rigidbody
    public GameObject BombBlast;    //�����G�t�F�N�g
    int power = 80; //���I�u�W�F�N�g�̍U����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * power, ForceMode.Impulse);
    }

    public void Change_Power(int _power)    //�U���͂̕ύX����
    {
        power = _power;
    }

    private void OnCollisionEnter(Collision collision)  //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG�����ꍇ
    {
        Instantiate(BombBlast, transform.position, Quaternion.identity);    //�����G�t�F�N�g�̐���
        Destroy(gameObject);
    }
}
