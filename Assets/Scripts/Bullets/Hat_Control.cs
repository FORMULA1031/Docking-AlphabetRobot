using UnityEngine;

public class Hat_Control : MonoBehaviour
{
    int power = 5;  //���I�u�W�F�N�g�̍U����
    public bool hit_flag = false;   //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG�������̃t���O
    bool enhancement_flag = false;  //���I�u�W�F�N�g�����������̃t���O

    public void Hit_Reset() //�q�b�g�����̏�����
    {
        hit_flag = false;
    }

    public void Enhancement(int _add_power) //���I�u�W�F�N�g���������̏���
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //��������
        {
            if (!hit_flag)
            {
                if (other.gameObject.GetComponent<Status_Control>() != null)
                {
                    other.gameObject.GetComponent<Status_Control>().Damage(power);
                    hit_flag = true;
                }
            }
        }
        else
        {
            hit_flag = true;
        }
    }
}
