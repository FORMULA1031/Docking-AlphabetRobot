using UnityEngine;

public class Tail_Control : MonoBehaviour
{
    int power = 20; //���I�u�W�F�N�g�̍U����
    int speed = 5;  //���I�u�W�F�N�g�̑��x
    bool action_flag = false;   //���I�u�W�F�N�g���s�����Ƃ��Ă悢���̃t���O
    bool leftrotation_flag = true;  //���I�u�W�F�N�g������]���邩�̃t���O

    private void FixedUpdate()  //���I�u�W�F�N�g�̈ړ�
    {
        if (action_flag)
        {
            if (leftrotation_flag)  //����]
            {
                if (transform.localEulerAngles.x < 100 && transform.localEulerAngles.x >= 80)
                {
                    speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if(!leftrotation_flag)  //�E��]
            {
                if (transform.localEulerAngles.x > 260 && transform.localEulerAngles.x <= 280)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
            }
            transform.Rotate(new Vector3(speed, 0, 0));
        }
    }

    public void Set_Action(bool _actionflag)    //���I�u�W�F�N�g���s�����邩�����肷��
    {
        action_flag = _actionflag;
    }

    private void OnCollisionEnter(Collision other)  //�q�b�g����
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
        if (other.gameObject.tag == "Player" && gameObject.tag != "Untagged")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                Destroy(gameObject.transform.root.gameObject);
            }
        }

        if(gameObject.tag == "Untagged")
        {
            if (leftrotation_flag)
            {
                leftrotation_flag = false;
            }
            else
            {
                leftrotation_flag = true;
            }
            speed *= -1;
        }
    }
}
