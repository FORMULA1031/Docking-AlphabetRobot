using UnityEngine;

public class InvisibleRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //�e�𐶐�������W�I�u�W�F�N�g
    public GameObject bullet;   //��������e
    public GameObject cannonstreet_effect;  //�e�̔��ˌ�̉��G�t�F�N�g
    GameObject Player;  //�v���C���[�I�u�W�F�N�g
    float bullet_serialspeed = 0f;  //�U������܂ł̒x������
    float bullet_stoptime = 0f; //�e�̘A�ˑ��x
    bool lockon_flag = false;   //�v���C���[�����b�N�I���������̃t���O

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Player = GameObject.Find("ZeroRobot");
        SetActive(gameObject, 0.1f, "Legacy Shaders/Transparent/Diffuse");
        gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        Color color = gameObject.GetComponent<Renderer>().material.color;
        color.a = 0.1f;
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //�v���C���[�����b�N�I�������ꍇ
        {
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.1f && bullet_stoptime <= 1)
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(false);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
            else if (bullet_stoptime >= 2)
            {
                bullet_stoptime = 0;
            }
        }
    }

    private void SetActive(GameObject _gameObject, float transparency, string shader_name)
    {
        // ���I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g�̃A�N�e�B�u��؂�ւ���
        RecursiveSetActive(_gameObject, transparency, shader_name);
    }

    private void RecursiveSetActive(GameObject a_CheckObject, float transparency, string shader_name)
    {
        // �ΏۃI�u�W�F�N�g�̎q�I�u�W�F�N�g���`�F�b�N����
        foreach (Transform child in a_CheckObject.transform)
        {
            if (child.GetComponent<Renderer>() != null)
            {
                child.GetComponent<Renderer>().material.shader = Shader.Find(shader_name);
                Color color = child.GetComponent<Renderer>().material.color;
                color.a = transparency;
                child.GetComponent<Renderer>().material.color = color;
            }
            if (child.GetComponent<ParticleSystem>() != null && shader_name == "Standard")
            {
                child.GetComponent<Renderer>().material.shader = Shader.Find("Particles/Standard Unlit");
            }
            // �q�I�u�W�F�N�g�̃A�N�e�B�u��؂�ւ���
            GameObject childObject = child.gameObject;
            SetActive(childObject, transparency, shader_name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I�������ɂ����ꍇ
        {
            SetActive(gameObject, 1f, "Standard");
            SetActive(Player, 1f, "Standard");
            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            Color color = Player.GetComponent<Renderer>().material.color;
            color.a = 1f;
            lockon_flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")   //�v���C���[�����b�N�I�������ɂ��Ȃ��ꍇ
        {
            lockon_flag = false;
        }
    }
}
