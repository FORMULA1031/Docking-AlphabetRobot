using UnityEngine;

public class PlayerAxeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    GameObject Player;  //プレイヤーオブジェクト
    float time = 0; //存在している時間
    int power = 100;    //攻撃力
    int speed = 0;  //速度
    bool enhancement_flag = false;  //強化中かのフラグ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.Find("ZeroRobot");
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Player.GetComponent<Status_Control>().Invincible(true); //自オブジェクトが存在している限りプレイヤーを無敵状態にする
        time += Time.deltaTime;
        if (time >= 0.5f)   //時間経過で自オブジェクトを削除
        {
            Player.GetComponent<Status_Control>().Invincible(false);
            Destroy(gameObject);
        }
        speed = Player.GetComponent<Status_Control>().speed;    //追加する速度の更新
    }

    private void FixedUpdate()  //自オブジェクトの移動処理
    {
        rb.velocity = new Vector3(-10, rb.velocity.y, speed);
    }

    public void Enhancement(int _add_power) //強化する場合
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Enemy")    //敵に直撃した場合
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Player.GetComponent<Status_Control>().Invincible(false);
            Destroy(gameObject);
        }
    }
}
