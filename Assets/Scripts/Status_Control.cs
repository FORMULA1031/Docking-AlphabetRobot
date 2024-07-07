using UnityEngine;
using UnityEngine.SceneManagement;

public class Status_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    public int stamina; //耐久値
    public int stamina_max; //耐久値の最大値
    public GameObject Explosion;    //爆発エフェクト
    public GameObject DropItem; //自オブジェクトが落とすアイテム
    FixedJoystick joystick; //プレイヤーの横移動を制御するジョイスティック
    int random_number;  //アイテムを落とすランダムな数
    public int speed;   //現在の速度
    public int original_speed;  //通常時の速度
    int rotation_speed = 0; //旋回速度
    int add_rotationspeed = 0;  //追加する旋回速度
    public int add_power = 0;   //追加する攻撃力
    public int original_addpower = 0;   //通常時の攻撃力
    GameFinish GameFinish;  //EventSystemがコンポーネントしているGameFinishスクリプト
    float invincible_time = 0;  //無敵時間
    bool invincible_flag = false;   //無敵状態かのフラグ
    AudioSource AudioSource;    //EventSystemがコンポーネントしているAudioSource
    public AudioClip damage_se; //ダメージを受けたSE
    public bool speedup_flag = false;   //速度が上昇してるかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        random_number = Random.Range(1, 3);
        stamina_max = stamina;
        original_speed = speed;
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //ゲーム開始用の設定
        {
            AudioSource = GameObject.Find("EventSystem").GetComponent<AudioSource>();
            joystick = GameObject.Find("Canvas/FixedJoystick").GetComponent<FixedJoystick>();
            GameFinish = GameObject.Find("EventSystem").GetComponent<GameFinish>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(stamina > stamina_max)   //耐久値をカンストさせる
        {
            stamina = stamina_max;
        }
        if(stamina <= 0)    //耐久値が無くなった場合
        {
            if(DropItem != null && random_number == 1)  //アイテムをドロップさせる
            {
                Instantiate(DropItem, transform.position, Quaternion.identity);
            }
            if(gameObject.tag == "Player")  //プレイヤーの耐久値が無くなった場合
            {
                GameFinish.GameOver(false);
            }
            else if(gameObject.tag == "Enemy")  //敵の耐久値が無くなった場合
            {
                GameFinish.Defeated();
            }
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (invincible_flag)    //無敵状態処理
        {
            invincible_time += Time.deltaTime;
            if(invincible_time >= 0.01f)
            {
                invincible_flag = false;
                invincible_time = 0;
            }
        }
        Key_Outputs();  //プレイヤー操作処理
    }

    private void FixedUpdate()  //自オブジェクトの移動処理
    {
        if (gameObject.tag == "Player") //自オブジェクトがプレイヤーオブジェクトだった場合
            rb.velocity = new Vector3(rotation_speed, rb.velocity.y, speed);
        else
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }

    void Key_Outputs()  //プレイヤー操作処理
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //ゲーム開始中だった場合
        {
            if (gameObject.tag == "Player") //自オブジェクトがプレイヤーオブジェクトだった場合
            {
                if (joystick.Horizontal < 0 && transform.position.x > -2.5f)    //左入力
                {
                    rotation_speed = -5 - add_rotationspeed;
                }
                else if (joystick.Horizontal > 0 && transform.position.x < 2.5f)    //右入力
                {
                    rotation_speed = 5 + add_rotationspeed;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -2.5f)   //左入力
                {
                    rotation_speed = -5 - add_rotationspeed;
                }
                else if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 2.5f)   //右入力
                {
                    rotation_speed = 5 + add_rotationspeed;
                }
                else
                {
                    rotation_speed = 0;
                }
            }
        }
    }

    public void Stamina_Repair(int repair_amount)   //自オブジェクトの耐久値回復処理
    {
        stamina += repair_amount;
    }

    public void Damage(int damage_amount)   //自オブジェクトがダメージを受けた処理
    {
        if (!invincible_flag)   //無敵状態ではない場合
        {
            stamina -= damage_amount;
            invincible_flag = true;
            AudioSource.PlayOneShot(damage_se);
            if (gameObject.tag == "Player") //自オブジェクトがプレイヤーオブジェクトだった場合
            {
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<MainCamera_Control>().Shake(0.3f, 0.4f));    //画面を揺らす
            }
        }
    }

    public void Invincible(bool flag)   //無敵状態にする処理
    {
        if (flag)
        {
            invincible_flag = true;
        }
    }

    public void Add_Speed(int _speed)   //速度の上昇処理
    {
        speed += _speed;
        add_rotationspeed = _speed;
        speedup_flag = true;
    }

    public void Return_Speed()  //速度を通常時の速度に戻す
    {
        speed = original_speed;
        add_rotationspeed = 0;
        speedup_flag = false;
    }

    public void Add_Power(int _power)   //攻撃力を強化する処理
    {
        add_power += _power;
    }

    public void Return_Power()  //攻撃力を通常時に戻す
    {
        add_power = original_addpower;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!invincible_flag)   //無敵状態ではない場合
        {
            if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")   //プレイヤーオブジェクトの接触処理
            {
                stamina -= 10;
                invincible_flag = true;
                AudioSource.PlayOneShot(damage_se);
                StartCoroutine(GameObject.Find("Main Camera").GetComponent<MainCamera_Control>().Shake(0.3f, 0.4f));    //カメラを揺らす
            }
            if (other.gameObject.tag == "Player")   //敵の接触処理
            {
                stamina = 0;
                invincible_flag = true;
            }
        }
    }
}
