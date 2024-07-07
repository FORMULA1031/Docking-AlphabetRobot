using UnityEngine;

public class BackItem_Control : MonoBehaviour
{
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    public GameObject weapon;   //��������o�b�N�p�b�N

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
            Player.GetComponent<Core_Control>().AdditionalEquipment("backpack", weapon, null);  //�o�b�N�p�b�N�p�[�c�̑�������
            Destroy(gameObject);
        }
    }
}
