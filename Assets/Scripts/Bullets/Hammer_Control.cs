using UnityEngine;

public class Hammer_Control : MonoBehaviour
{
    int power = 25; //���I�u�W�F�N�g�̍U����
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

    private void OnCollisionEnter(Collision other)  //�q�b�g����
    {
        if (other.gameObject.tag == "Player" && gameObject.name != "Hammer(Clone)")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
        if (other.gameObject.tag == "Enemy" && gameObject.name != "Hammer_Enemy(Clone)")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
    }
}
