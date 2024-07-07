using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera_Control : MonoBehaviour
{
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    Vector3 offset; //�v���C���[�ƃJ�����̍��W�̍�

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�v���C���[�ƃJ�����̍��W�̍���ݒ�
        {
            Player = GameObject.Find("ZeroRobot");
            offset = transform.position - Player.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //�J�����̈ʒu�̍X�V
        {
            if (Player != null)
            {
                transform.position = Player.transform.position + offset;
            }
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
        else
        {   
            //�X�^�[�g��ʗp�̃J�����ړ�����
            transform.Translate(0, 0, 0.2f, Space.World);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)   //��ʂ�h�炷����
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)  //duration�b�h�炷
        {
            transform.position = originalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
