using UnityEngine;
using UnityEngine.UI;

public class PlayerEnhancement_Control : MonoBehaviour
{
    int stamina = 80;   //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 0;  //�ϋv�l�̌����̒x������
    bool castof_flag = false;   //���̃p�[�c���p�[�W����Ă��邩�̃t���O
    Slider slider;  //�ϋv�Q�[�W
    Status_Control status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control�X�N���v�g

    // Start is called before the first frame update
    void Start()    //�G���n���X�����g�p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status_Control.add_power == status_Control.original_addpower)   //�v���C���[�̍U���̏㏸
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Power(5);
        }
        serial_time += Time.deltaTime;
        if (serial_time >= 0.3f)    //�ϋv�l�̌���
        {
            stamina--;
            serial_time = 0;
        }

        if (stamina <= 0)   //�ϋv�l�������Ȃ����ꍇ
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //�\�����Ă���ϋv�l�̃o�[�̍X�V
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //���̃p�[�c���p�[�W����Ă��Ȃ��ꍇ
        {
            if (status_Control != null)
            {
                status_Control.Return_Power();
                castof_flag = true;
            }
        }
    }
}
