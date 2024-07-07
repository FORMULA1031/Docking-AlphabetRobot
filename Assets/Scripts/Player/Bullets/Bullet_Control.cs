using UnityEngine;

public class Bullet_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    public int speed;   //移動速度
    public int power;   //攻撃
    float launch_time = 0;  //存在している時間
    bool induction_flag = false;    //誘導するかのフラグ
    GameObject Player;  //プレイヤーオブジェクト
    bool hit_flag = false;  //他のオブジェクトと接触したかのフラグ
    bool enhancement_flag = false;  //自オブジェクトを強化するかのフラグ
    bool player_flag = false;   //プレイヤーによって生成されたかのフラグ
    int speed_add = 0;  //追加する機動力値
    public float endurance_value;   //耐久値
    bool invincible_flag = false;   //無敵かのフラグ
    float invincible_time = 0.0f;   //無敵時間

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        if (!player_flag && Player != null && induction_flag)   //生成時にプレイヤーの方へ向く
        {
            transform.LookAt(Player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jet_flag(); //ジェットパーツを装備しているときの処理
        launch_time += Time.deltaTime;
        if (induction_flag && Player != null)   //誘導処理
        {
            if (transform.position.z > Player.transform.position.z + 1.5f)
            {
                Vector3 relativePos = Player.transform.position - this.transform.position;
                // 方向を、回転情報に変換
                Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, relativePos.y - 1f, relativePos.z));
                // 現在の回転情報と、ターゲット方向の回転情報を補完する
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.003f);
            }
            if (transform.position.z < Player.transform.position.z - 1f)    //プレイヤーの後ろについた場合
            {
                Destroy(gameObject);
            }
        }
        if (launch_time >= 4)   //時間経過で自オブジェクトの削除
        {
            Destroy(gameObject);
        }

        if (invincible_flag)    //無敵時間処理
        {
            invincible_time += Time.deltaTime;
            if(invincible_time >= 0.1f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
    }

    private void FixedUpdate()  //自オブジェクトの移動処理
    {
        rb.velocity = transform.forward * (speed + speed_add);
    }

    public void Player_flag(bool flag)  //プレイヤーによって生成された場合
    {
        player_flag = true;
    }

    public void Induction(bool flag)    //誘導するかを決める
    {
        induction_flag = flag;
    }

    private void Jet_flag() //ジェットパーツを装備している場合
    {
        if (Player != null) //機動力上昇
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

    public void Enhancement(int _add_power) //攻撃力上昇
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //直撃判定
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
        else if(other.gameObject.tag == "Bullet" && !invincible_flag)   //実弾を接触した場合
        {
            if (other.gameObject.GetComponent<Bullet_Control>() != null)    //耐久値の減少
            {
                endurance_value -= other.gameObject.GetComponent<Bullet_Control>().power;
            }
            if(endurance_value <= 0)    //耐久値が無くなった場合
            {
                Destroy(gameObject);
            }
            invincible_flag = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")  //バリア判定のあるオブジェクトと接触した場合
        {
            Destroy(gameObject);
        }
    }
}
