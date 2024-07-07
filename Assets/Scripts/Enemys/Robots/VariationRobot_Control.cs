using UnityEngine;

public class VariationRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    int rotation_speed = 0; //自オブジェクトを速度
    float atack_time = 0;   //攻撃までの遅延時間
    float jump_time = 0;    //ジャンプするまでの遅延時間
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool direction_left = true; //左へ移動するかのフラグ
    GameObject Muzzle;  //弾を生成する座標オブジェクト
    public GameObject bullet;   //生成する弾
    GameObject Player;  //プレイヤーオブジェクト
    GameFinish GameFinish;  //EventSystemがコンポーネントしているGameFinishスクリプト
    bool invisible_flag = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
        GameFinish = GameObject.Find("EventSystem").GetComponent<GameFinish>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) //プレイヤーをロックオンする処理
        {
            if (Player.transform.position.z >= 255)
            {
                lockon_flag = true;
            }
        }
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            atack_time += Time.deltaTime;
            jump_time += Time.deltaTime;

            if (atack_time >= 2 && !invisible_flag) //弾の生成
            {
                Quaternion muzzle_quaternion = transform.rotation;
                muzzle_quaternion.y += 90;
                GameObject bullet_Instance = Instantiate(bullet, Muzzle.transform.position, muzzle_quaternion);
                bullet_Instance.GetComponent<CannonBullet_Control>().Induction(true);
                atack_time = 0;
            }

            if(jump_time >= 3)  //ジャンプする
            {
                rb.AddForce(transform.up * 5000, ForceMode.Impulse);
                jump_time = 0;
            }

            if (direction_left && transform.position.x > -2.5f) //移動処理
            {
                rotation_speed = -5;
            }
            else if (direction_left)
            {
                direction_left = false;
            }
            if (!direction_left && transform.position.x < 2.5f)
            {
                rotation_speed = 5;
            }
            else if (!direction_left)
            {
                direction_left = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            rb.velocity = new Vector3(rotation_speed, rb.velocity.y, rotation_speed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            invisible_flag = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            invisible_flag = true;
        }
    }

    public void OnDestroy()
    {
        GameFinish.GameOver(true);
    }
}
