using UnityEngine;

public class HammerRobot_Control : MonoBehaviour
{
    GameObject HammerR_grip;    //右手の座標オブジェクト
    GameObject HammerL_grip;    //左手の座標オブジェクト
    GameObject Hammer_right;    //右手用の生成するハンマー
    GameObject Hammer_left; //左手用の生成するハンマー
    public GameObject Hammer;   //生成するハンマー
    GameObject HammerR_Instance;    //生成した右手用のハンマー
    GameObject HammerL_Instance;    //生成した左手用のハンマー
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool move_flag = false; //プレイヤーが移動中かのフラグ
    float speed = -150.0f;  //自オブジェクトの速度
    bool leftrotation_flag = true;  //左回転するフラグ

    // Start is called before the first frame update
    void Start()
    {
        HammerR_grip = transform.Find("Arm_right/Hammer").gameObject;
        HammerL_grip = transform.Find("Arm_left/Hammer").gameObject;
        Hammer_right = transform.Find("Arm_right/Hammer/Muzzle").gameObject;
        Hammer_left = transform.Find("Arm_left/Hammer/Muzzle").gameObject;

        Quaternion muzzle_quaternion = transform.rotation;
        HammerR_Instance = Instantiate(Hammer, Hammer_right.transform.position, muzzle_quaternion);
        Vector3 rotation_right = HammerR_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        HammerR_Instance.transform.localRotation = Quaternion.Euler(rotation_right);

        HammerL_Instance = Instantiate(Hammer, Hammer_left.transform.position, muzzle_quaternion);
        Vector3 rotation_left = HammerL_Instance.transform.localRotation.eulerAngles;
        rotation_left.z -= 90;
        HammerL_Instance.transform.localRotation = Quaternion.Euler(rotation_left);
    }

    // Update is called once per frame
    void Update()
    {
        HammerR_Instance.transform.position = Hammer_right.transform.position;
        HammerR_Instance.transform.rotation = Hammer_right.transform.rotation;
        Vector3 rotation_right = HammerR_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        HammerR_Instance.transform.localRotation = Quaternion.Euler(rotation_right);
        HammerL_Instance.transform.position = Hammer_left.transform.position;
        HammerL_Instance.transform.rotation = Hammer_left.transform.rotation;
        Vector3 rotation_left = HammerL_Instance.transform.localRotation.eulerAngles;
        rotation_left.z -= 90;
        HammerL_Instance.transform.localRotation = Quaternion.Euler(rotation_left);
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            if (leftrotation_flag)  //左回転する
            {
                if (HammerR_grip.transform.localEulerAngles.z > 250 && HammerR_grip.transform.localEulerAngles.z <= 280)
                {
                    speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if (!leftrotation_flag) //右回転する
            {
                if (HammerR_grip.transform.localEulerAngles.z >= 350 && HammerR_grip.transform.localEulerAngles.z < 360)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
                else if (HammerR_grip.transform.localEulerAngles.z >= 0 && HammerR_grip.transform.localEulerAngles.z < 20)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
            }
            HammerR_grip.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
            HammerL_grip.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいた場合
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-1);
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
        Destroy(HammerR_Instance);
        Destroy(HammerL_Instance);
    }
}
