using UnityEngine;

public class AxeRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //斬撃エフェクトを生成する座標オブジェクト
    public GameObject Effect;   //斬撃エフェクト
    GameObject Effect_Instance; //生成した斬撃エフェクト
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool move_flag = false; //自オブジェクトが移動中かのフラグ
    float atack_time = 0;   //攻撃するまでの遅延時間

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
            atack_time += Time.deltaTime;
            if (atack_time >= 1)    //攻撃処理
            {
                Quaternion muzzle_quaternion = transform.rotation;
                Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
                Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
                rotation.y -= 90;
                Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
                atack_time = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいた場合
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-3);
                move_flag = true;
            }
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

    public void OnDestroy()
    {
        Destroy(Effect_Instance);
    }
}
