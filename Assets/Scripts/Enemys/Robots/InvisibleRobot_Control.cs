using UnityEngine;

public class InvisibleRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //弾を生成する座標オブジェクト
    public GameObject bullet;   //生成する弾
    public GameObject cannonstreet_effect;  //弾の発射後の煙エフェクト
    GameObject Player;  //プレイヤーオブジェクト
    float bullet_serialspeed = 0f;  //攻撃するまでの遅延時間
    float bullet_stoptime = 0f; //弾の連射速度
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ

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
        if (lockon_flag)    //プレイヤーをロックオンした場合
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
        // 現オブジェクトの全ての子オブジェクトのアクティブを切り替える
        RecursiveSetActive(_gameObject, transparency, shader_name);
    }

    private void RecursiveSetActive(GameObject a_CheckObject, float transparency, string shader_name)
    {
        // 対象オブジェクトの子オブジェクトをチェックする
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
            // 子オブジェクトのアクティブを切り替える
            GameObject childObject = child.gameObject;
            SetActive(childObject, transparency, shader_name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいた場合
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
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいない場合
        {
            lockon_flag = false;
        }
    }
}
