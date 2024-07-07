using UnityEngine;

public class ShieldRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //弾を生成する座標オブジェクト
    GameObject Muzzle_Shield;   //シールドを生成する座標オブジェクト
    public GameObject bullet;   //生成する弾
    public GameObject cannonstreet_effect;  //弾の発射後の煙エフェクト
    public GameObject ShieldBarrier;    //バリア判定オブジェクト
    GameObject Shield_Instance; //生成したシールド
    float bullet_serialspeed = 0f;  //攻撃するまでの遅延時間
    float bullet_stoptime = 0f; //弾の連射速度
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Muzzle_Shield = transform.Find("Backpack/Shield/Muzzle").gameObject;
        Shield_Instance = Instantiate(ShieldBarrier, Muzzle_Shield.transform.position, transform.rotation);
        Vector3 rotation = Shield_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Shield_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            bullet_serialspeed += Time.deltaTime;
            bullet_stoptime += Time.deltaTime;
            if (bullet_serialspeed >= 0.2f && bullet_stoptime <= 1) //弾の生成
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
        Shield_Instance.transform.position = Muzzle_Shield.transform.position;
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
