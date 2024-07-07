using UnityEngine;

public class HeadItem_Control : MonoBehaviour
{
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    public GameObject weapon;   //��������w�b�h�p�[�c

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
            Player.GetComponent<Core_Control>().AdditionalEquipment("head", weapon, null);  //�w�b�h�p�[�c�̑�������
            Destroy(gameObject);
        }
    }
}
