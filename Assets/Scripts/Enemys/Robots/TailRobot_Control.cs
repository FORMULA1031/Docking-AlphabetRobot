using UnityEngine;

public class TailRobot_Control : MonoBehaviour
{
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O
    GameObject Tail;    //�K���I�u�W�F�N�g
    Tail_Control Tail_Control;  //�K���I�u�W�F�N�g���R���|�[�l���g���Ă���Tail_Control�X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        Tail = gameObject.transform.Find("Backpack/Tail/Sphere").gameObject;
        Tail_Control = Tail.GetComponent<Tail_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        Tail_Control.Set_Action(lockon_flag);   //�K���I�u�W�F�N�g�̍U�����[�V������ݒ�
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
}
