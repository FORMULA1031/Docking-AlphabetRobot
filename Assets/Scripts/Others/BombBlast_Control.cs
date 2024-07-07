using UnityEngine;

public class BombBlast_Control : MonoBehaviour
{
    int power = 3;  //���I�u�W�F�N�g�̍U����
    float serial_time = 0;  //���I�u�W�F�N�g�̑��݂��Ă��鎞��

    void Update()   //0.5�b��Ɏ��I�u�W�F�N�g���폜����
    {
        serial_time += Time.deltaTime;
        if(serial_time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)  //�q�b�g����
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")   //�G�ƐڐG
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
        if (other.gameObject.tag == "Player")   //�v���C���[�ƐڐG
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
