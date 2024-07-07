using UnityEngine;

public class FireEffect_Control : MonoBehaviour
{
    int power = 10; //���I�u�W�F�N�g�̍U����
    public bool hit_flag = false;   //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG���Ă��邩�̃t���O
    bool enhancement_flag = false;  //���I�u�W�F�N�g�����������̃t���O

    public void Enhancement(int _add_power) //���I�u�W�F�N�g�̋�������
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    public void Destroy_Flag()  //���I�u�W�F�N�g�̍폜
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //�q�b�g����
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            hit_flag = true;
        }
    }
}
