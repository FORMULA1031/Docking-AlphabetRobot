using UnityEngine;

public class KnifeEffect_Control : MonoBehaviour
{
    int power = 20; //���I�u�W�F�N�g�̍U����
    public bool hit_flag = false;   //���I�u�W�F�N�g�����̃I�u�W�F�N�g�ƐڐG�������̃t���O

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�ƐڐG�����ꍇ
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            hit_flag = true;
        }

        if(other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")    //�G�ƐڐG�����ꍇ
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
    }
}
