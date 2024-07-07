using UnityEngine;

public class KnifeRobot_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    GameObject Muzzle;  //斬撃エフェクトを生成する座標オブジェクト
    public GameObject Effect;   //斬撃エフェクト
    GameObject Effect_Instance; //生成した斬撃エフェクト
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool effect_flag = false;   //斬撃エフェクトを生成したかのフラグ
    float jump_time = 0;    //ジャンプするまでの遅延時間

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Muzzle = transform.Find("Arm_right/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //斬撃エフェクトを生成
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Effect_Instance.transform.parent = Muzzle.transform;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            jump_time += Time.deltaTime;
            if (jump_time >= 2f)
            {
                rb.AddForce(transform.up * 8000, ForceMode.Impulse);
                jump_time = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            transform.Rotate(new Vector3(0, -5f, 0));
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

    public void OnDestroy()
    {
        Destroy(Effect_Instance);
    }
}
