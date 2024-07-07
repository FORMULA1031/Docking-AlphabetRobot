using UnityEngine;
using UnityEngine.UI;

public class PlayerDrill_Control : MonoBehaviour
{
    public GameObject Effect;   //�h�����p�̃G�t�F�N�g
    GameObject Effect_Instance; //���������h�����G�t�F�N�g
    GameObject Muzzle;  //��������h�����G�t�F�N�g�̍��W�I�u�W�F�N�g
    int bullets_number = 20;    //������
    float revolution_time = 0f; //�������̌����̒x������
    Text WeaponNumber_text; //�\�����鐧�����e�L�X�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Status_Control Status_Control;  //�v���C���[�I�u�W�F�N�g���R���|�[�l���g���Ă���Status_Control�X�N���v�g
    int add_power = 0;  //��������U���͂̒l

    // Start is called before the first frame update
    void Start()    //�h�����p�[�c�̒ǉ�����
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
        Muzzle = transform.Find("Muzzle").gameObject;
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        Quaternion muzzle_quaternion = transform.rotation;
        Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
        Vector3 drilleffect_rotation = Effect_Instance.transform.localRotation.eulerAngles;
        drilleffect_rotation.y -= 90;
        Effect_Instance.transform.localRotation = Quaternion.Euler(drilleffect_rotation);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = false;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //��������U���͂̒l�̍X�V
        if (add_power != 0)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Effect_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)  //�������̌���
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)    //���������Ȃ��Ȃ����ꍇ
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
            Destroy(Effect_Instance);
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //�\������c�萧�����̍X�V
    }

    void Display_BulletsNumber()    //�\������c�萧�����̍X�V
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (Effect_Instance != null)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Destroy_Flag();
        }
    }
}
