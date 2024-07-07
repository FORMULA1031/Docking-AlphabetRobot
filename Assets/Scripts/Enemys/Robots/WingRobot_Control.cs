using UnityEngine;

public class WingRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    GameObject Muzzle;  //弾を生成する座標オブジェクト
    public GameObject bullet;   //生成する弾
    GameObject Wing_right;  //右翼
    GameObject Wing_left;   //左翼
    float bullet_serialspeed = 2.0f;    //攻撃するまでの遅延時間
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool firing_flag = true;    //飛んでよいかのフラグ
    bool leftrotation_flag = true;  //左回転するかのフラグ
    float speed = 0.8f; //自オブジェクトの速度

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        Wing_right = transform.Find("Backpack/Wing/Wing_right").gameObject;
        Wing_left = transform.Find("Backpack/Wing/Wing_left").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            bullet_serialspeed += Time.deltaTime;
            if (bullet_serialspeed >= 2.0f) //弾の生成
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(false);
                bullet_serialspeed = 0;
                firing_flag = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!leftrotation_flag) //翼の処理
        {
            if (Wing_right.transform.localEulerAngles.y < 60 && Wing_right.transform.localEulerAngles.y >= 50)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        if (leftrotation_flag)
        {
            if (Wing_right.transform.localEulerAngles.y > 330 && Wing_right.transform.localEulerAngles.y <= 340)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        Wing_right.transform.Rotate(new Vector3(0, 0, speed));
        Wing_left.transform.Rotate(new Vector3(0, 0, -speed));
        if (!firing_flag)   //飛ぶ処理
        {
            rb.AddForce(transform.up * 8000, ForceMode.Impulse);
            firing_flag = true;
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
