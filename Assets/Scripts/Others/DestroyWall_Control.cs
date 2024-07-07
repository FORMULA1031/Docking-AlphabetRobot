using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyWall_Control : MonoBehaviour
{
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Vector3 offset; //���I�u�W�F�N�g�ƃv���C���[�̍��W�̍�

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�Q�[�����J�n����Ă���ꍇ
        {
            Player = GameObject.Find("ZeroRobot");
            offset = transform.position - Player.transform.position;
        }
    }

    private void FixedUpdate()  //���I�u�W�F�N�g�̍��W�X�V
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�Q�[���J�n�p����
        {
            if (Player != null)
            {
                transform.position = Player.transform.position + offset;
            }
            transform.position = new Vector3(0, 6, transform.position.z);
        }
        else
        {
            transform.Translate(0, 0, 0.2f, Space.World);
        }
    }

    //���I�u�W�F�N�g�ƐڐG�����I�u�W�F�N�g���폜����
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
