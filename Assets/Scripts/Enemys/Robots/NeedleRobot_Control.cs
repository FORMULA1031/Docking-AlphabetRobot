using UnityEngine;

public class NeedleRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //針を生成する座標オブジェクト
    public GameObject bullet;   //生成する針
    public GameObject cannonstreet_effect;  //針の発射後の煙エフェクト
    GameObject Player;  //プレイヤーオブジェクト
    float bullet_serialspeed = 0.5f;    //攻撃するまでの遅延時間
    float bullet_stoptime = 0f; //針の連射速度
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    Quaternion original_angle;  //自オブジェクトの正向き

    // Start is called before the first frame update
    void Start()
    {
        original_angle = transform.rotation;
        Muzzle = transform.Find("Arm_left/Muzzle").gameObject;
        Player = GameObject.Find("ZeroRobot");
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && Player != null)  //プレイヤーをロックオンした場合
        {
            this.transform.LookAt(Player.transform);
            Vector3 rotation = this.transform.localRotation.eulerAngles;
            rotation.x = 0;
            rotation.y -= 90;
            rotation.x -= 5;
            transform.localRotation = Quaternion.Euler(rotation);
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.5f && bullet_stoptime <= 1) //針の生成
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<Bullet_Control>().Induction(true);
                Instantiate(cannonstreet_effect, Muzzle.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
        }
        else
        {
            transform.rotation = original_angle;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいた場合
        {
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
