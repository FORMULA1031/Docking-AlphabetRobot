using UnityEngine;

public class TextScaleDownOrUp : MonoBehaviour
{
    float time = 0.0f;  //�L����(�k�߂�)�v������
    float changeSpeed = 0.0f;   //�L����(�k�߂�)�傫��
    public bool enlarge;    //�L���邩�̃t���O

    void Start()
    {
        enlarge = true;
    }

    void Update()
    {
        changeSpeed = Time.deltaTime * 0.1f;

        if (time < 0)
        {
            enlarge = true;
        }
        if (time > 0.7f)
        {
            enlarge = false;
        }

        if (enlarge == true)    //�e�L�X�g���L����
        {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {   //�e�L�X�g���k�߂�
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
    }
}