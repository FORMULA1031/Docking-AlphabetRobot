using UnityEngine;

public class DrillEffect_Control : MonoBehaviour
{
    int power = 20; //���I�u�W�F�N�g�̍U����
    public bool hit_flag = false;   //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG�������̃t���O
    bool enhancement_flag = false;  //���I�u�W�F�N�g�����������̃t���O

    public void Enhancement(int _add_power) //���I�u�W�F�N�g�̋�������
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    public void Destroy_Flag()
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
