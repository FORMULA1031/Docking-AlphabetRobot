using UnityEngine;

public class BazookaRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //弾を生成する座標オブジェクト
    public GameObject bullet;   //生成する弾
    float bullet_serialspeed = 5.0f;    //攻撃するまでの遅延時間
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 5.0f) //攻撃処理
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
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
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離から離れた場合
        {
            lockon_flag = false;
        }
    }
}
