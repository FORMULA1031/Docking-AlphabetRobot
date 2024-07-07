using UnityEngine;

public class ArmItem_Control : MonoBehaviour
{
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    public GameObject weapon;   //�������鍶�r
    public GameObject weapon2;  //��������E�r

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("ZeroRobot");
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̉�]����
    {
        transform.Rotate(new Vector3(0, 3, 0));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�ƐڐG�����ꍇ
        {
            Player.GetComponent<Core_Control>().AdditionalEquipment("arm", weapon, weapon2);    //�A�[���p�[�c�̑�������
            Destroy(gameObject);
        }
    }
}
