using UnityEngine;

public class FireRobot_Control : MonoBehaviour
{
    GameObject Head;    //子オブジェクトのHeadオブジェクト
    GameObject Muzzle;  //炎を生成する座標オブジェクト
    public GameObject Effect;   //炎エフェクト
    GameObject Effect_Instance; //生成した炎エフェクト
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    int rotation_speed = 1; //自オブジェクトの速度
    bool leftrotation_flag = true;  //左回転するかのフラグ
    bool effect_flag = false;   //炎エフェクトを生成したかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        Head = transform.Find("Head").gameObject;
        Muzzle = transform.Find("Head/Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag && !effect_flag)    //プレイヤーをロックオンした場合
        {
            Quaternion muzzle_quaternion = transform.rotation;
            Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
            effect_flag = true;
        }
    }

    private void FixedUpdate()
    {
        if (lockon_flag)
        {
            if (leftrotation_flag)  //Headオブジェクトを左回転する処理
            {
                if (Head.transform.localEulerAngles.y >= 30 && Head.transform.localEulerAngles.y < 90)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if (!leftrotation_flag) //Headオブジェクトを右回転する処理
            {
                if (Head.transform.localEulerAngles.y <= 330 && Head.transform.localEulerAngles.y > 270)
                {
                    rotation_speed *= -1;
                    leftrotation_flag = true;
                }
            }
            Head.transform.Rotate(new Vector3(0, rotation_speed, 0));
            Effect_Instance.transform.position = Muzzle.transform.position;
            Effect_Instance.transform.rotation = Head.transform.rotation;
            Vector3 rotation = Effect_Instance.transform.localRotation.eulerAngles;
            rotation.y += 90;
            Effect_Instance.transform.localRotation = Quaternion.Euler(rotation);
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
