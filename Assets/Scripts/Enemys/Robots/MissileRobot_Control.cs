using UnityEngine;

public class MissileRobot_Control : MonoBehaviour
{
    GameObject Muzzle_left; //左側に生成するミサイルの座標オブジェクト
    GameObject Muzzle_right;    //右側に生成するミサイルの座標を座標オブジェクト
    public GameObject bullet;   //生成するミサイル
    public GameObject cannonstreet_effect;  //ミサイルの発射後の煙エフェクト
    float bullet_serialspeed = 2f;  //攻撃するまでの遅延時間
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        Muzzle_left = transform.Find("Backpack/Missile/Muzzle_left").gameObject;
        Muzzle_right = transform.Find("Backpack/Missile/Muzzle_right").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 3.0f) //ミサイル発射の処理
            {
                Quaternion muzzle_quaternion = Muzzle_left.transform.rotation;
                muzzle_quaternion.y += 90;
                muzzle_quaternion.z -= 30;
                Instantiate(bullet, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_left.transform.position, muzzle_quaternion);
                Instantiate(bullet, Muzzle_right.transform.position, muzzle_quaternion);
                Instantiate(cannonstreet_effect, Muzzle_right.transform.position, muzzle_quaternion);
                bullet_serialspeed = 0;
            }
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
