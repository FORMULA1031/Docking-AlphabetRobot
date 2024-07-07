using UnityEngine;
using UnityEngine.UI;

public class PlayerTail_Control : MonoBehaviour
{
    public GameObject Tail; //��������e�[��
    Tail_Control Tail_Control;  //���������e�[�����R���|�[�l���g���Ă���Tail_Control�X�N���v�g
    GameObject Tail_Instance;   //���������e�[��
    Slider slider;  //�ϋv�l�p�̃o�[
    int stamina = 100;  //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 0;  //�ϋv�l�̌����̒x������

    // Start is called before the first frame update
    void Start()    //�e�[���p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
        Transform[] brotrans = new Transform[parent.childCount];
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
        Tail_Instance = Instantiate(Tail, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation);
        Tail_Control = Tail_Instance.GetComponent<Tail_Control>();
        Tail_Control.Set_Action(true);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
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
        slider.value = (float)stamina / (float)stamina_max; //�ϋv�l�p�̃o�[�̍X�V
    }

    private void FixedUpdate()  //���������e�[���̍��W����
    {
        Tail_Instance.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1);
    }

    private void OnDestroy()
    {
        Destroy(Tail_Instance);
    }
}
