using UnityEngine;
using UnityEngine.UI;

public class PlayerJet_Control : MonoBehaviour
{
    int stamina = 100;  //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 0;  //�ϋv�l�̌����̒x������
    bool castof_flag = false;   //���̃p�[�c���p�[�W�������̃t���O
    Slider slider;  //�ϋv�l�p�̃o�[

    // Start is called before the first frame update
    void Start()    //�W�F�b�g�p�[�c�̒ǉ�����
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
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̋@���͂��グ��
        if (transform.root.gameObject.GetComponent<Status_Control>().speed == transform.root.gameObject.GetComponent<Status_Control>().original_speed)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Speed(3);
        }
        serial_time += Time.deltaTime;
        if(serial_time >= 0.3f) //�ϋv�l�̌�������
        {
            stamina--;
            serial_time = 0;
        }

        if(stamina <= 0)    //�ϋv�l�������Ȃ����ꍇ
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //�ϋv�l�p�̃o�[�̍X�V
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //���̃p�[�c���p�[�W����Ă��Ȃ��ꍇ
        {
            transform.root.gameObject.GetComponent<Status_Control>().Return_Speed();
            castof_flag = true;
        }
    }
}
