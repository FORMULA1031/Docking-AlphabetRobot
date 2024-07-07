using UnityEngine;

public class DrillRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //ドリルエフェクトを生成する座標オブジェクト
    public GameObject Effect;   //ドリルエフェクト
    GameObject Effect_Instance; //生成したドリルエフェクト
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool effect_flag = false;   //ドリルエフェクトを生成したフラグ
    bool move_flag = false; //自オブジェクトが移動中かのフラグ

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Head/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //ドリルエフェクトを生成
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y -= 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
        if (lockon_flag)    //生成したドリルエフェクトの制御
        {
            Effect_Instance.transform.position = Muzzle.transform.position;
            if (Effect_Instance.GetComponent<DrillEffect_Control>().hit_flag)
            {
                gameObject.GetComponent<Status_Control>().Damage(100);
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
