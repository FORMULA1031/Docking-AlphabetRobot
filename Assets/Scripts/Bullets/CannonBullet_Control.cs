using UnityEngine;

public class CannonBullet_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    public int speed;   //自オブジェクトの速度
    public int power;   //自オブジェクトの攻撃力
    float launch_time = 0;  //自オブジェクトが存在している時間
    bool induction_flag = false;    //自オブジェクトが誘導するかのフラグ
    GameObject Player;  //プレイヤーオブジェクト
    bool hit_flag = false;  //自オブジェクトが他のオブジェクトと接触したかのフラグ
    bool enhancement_flag = false;  //自オブジェクトを強化するかのフラグ
    bool player_flag = false;   //自オブジェクトがプレイヤーによって生成されたかのフラグ
    int speed_add = 0;  //増幅されるスピード量
    public float endurance_value;   //自オブジェクトの耐久値
    bool invincible_flag = false;   //自オブジェクトが無敵かのフラグ
    float invincible_time = 0.0f;   //自オブジェクトの無敵時間

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        if (!player_flag && Player != null && induction_flag)   //誘導する敵の弾だった場合
        {
            transform.LookAt(Player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jet_flag(); //ジェットを装備中の処理
        launch_time += Time.deltaTime;
        if (induction_flag && Player != null)   //ターゲットへの誘導の処理
        {
            if (transform.position.z > Player.transform.position.z + 1.5f)
            {
                Vector3 relativePos = Player.transform.position - this.transform.position;
                // 方向を、回転情報に変換
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, relativePos.y - 1f, relativePos.z));
                // 現在の回転情報と、ターゲット方向の回転情報を補完する
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.003f);
            }
            if(transform.position.z < Player.transform.position.z - 1f)
            {
                Destroy(gameObject);
            }
        }
        if (launch_time >= 4)   //4秒以上経過すると削除
        {
            Destroy(gameObject);
        }

        if (invincible_flag)    //無敵状態の処理
        {
            invincible_time += Time.deltaTime;
            if (invincible_time >= 0.1f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
    }

    private void FixedUpdate()  //自オブジェクトの移動
    {
        rb.velocity = transform.forward * (speed + speed_add);
    }

    public void Player_flag(bool flag)  //プレイヤーによっての制御だった場合
    {
        player_flag = true;
    }

    public void Induction(bool flag)    //誘導するかの処理
    {
        induction_flag = flag;
    }

    public void Jet_flag()  //ジェットを装備中の処理
    {
        if (Player != null)
        {
            if (Player.GetComponent<Status_Control>().speedup_flag)
            {
                speed_add = 3;
            }
            else
            {
                speed_add = 0;
            }
        }
    }

    public void Enhancement(int _add_power) //自オブジェクトの強化処理
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //直撃ヒット判定
        {
            if (!hit_flag)
            {
                if (other.gameObject.GetComponent<Status_Control>() != null)
                {
                    other.gameObject.GetComponent<Status_Control>().Damage(power);
                    Destroy(gameObject);
                    hit_flag = true;
                }
            }
        }
        else if (other.gameObject.tag == "Beam" && !invincible_flag)    //ビームとの接触判定
        {
            if (other.gameObject.GetComponent<CannonBullet_Control>() != null)
            {
                endurance_value -= other.gameObject.GetComponent<CannonBullet_Control>().power;
            }
            if (endurance_value <= 0)
            {
                Destroy(gameObject);
            }
            invincible_flag = true;
        }
        else
        {
            if (other.gameObject.name != "Hammer(Clone)")   //ハンマーと接触判定
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Barrier")   //場外に入った場合
        {
            Destroy(gameObject);
        }
    }
}
