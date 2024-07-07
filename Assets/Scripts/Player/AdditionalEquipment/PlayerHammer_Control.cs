using UnityEngine;
using UnityEngine.UI;

public class PlayerHammer_Control : MonoBehaviour
{
    public GameObject Hammer;   //生成するハンマー
    GameObject Muzzle;  //生成するハンマーの座標オブジェクト
    GameObject Hammer_Instance; //生成したハンマー
    GameObject Hammer_grip; //ハンマーの握り部分
    float revolution_time = 0.0f;   //耐久値の減少の遅延時間
    int bullets_number = 20;    //耐久値
    Text WeaponNumber_text; //表示する耐久値テキスト
    GameObject Player;  //プレイヤーオブジェクト
    Status_Control Status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値
    float speed = -150.0f;  //動作速度
    bool leftrotation_flag = true;  //左回転するかのフラグ

    // Start is called before the first frame update
    void Start()    //ハンマーパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
        Transform[] brotrans = new Transform[parent.childCount];
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Muzzle = transform.Find("Hammer/Muzzle").gameObject;
        Hammer_grip = transform.Find("Hammer").gameObject;
        if (gameObject.name == "Hammer_Player(Clone)")
        {
            Vector3 rotation = this.transform.localRotation.eulerAngles;
            rotation.x -= 90;
            rotation.y -= 90;
            rotation.z -= 90;
            transform.localRotation = Quaternion.Euler(rotation);
        }
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Arm)/WeaponNumber").GetComponent<Text>();
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
        GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = false;
        Instance_Bullets();
    }

    // Update is called once per frame
    void Update()
    {
        Hammer_Instance.transform.position = Muzzle.transform.position; //生成したハンマーの位置角度の更新
        Hammer_Instance.transform.rotation = Muzzle.transform.rotation;
        Vector3 rotation_right = Hammer_Instance.transform.localRotation.eulerAngles;
        rotation_right.z -= 90;
        Hammer_Instance.transform.localRotation = Quaternion.Euler(rotation_right);

        if (leftrotation_flag)  //左回転する
        {
            if (Hammer_grip.transform.localEulerAngles.z > 250 && Hammer_grip.transform.localEulerAngles.z <= 280)
            {
                speed *= -1;
                leftrotation_flag = false;
            }
        }
        if (!leftrotation_flag) //右回転する
        {
            if (Hammer_grip.transform.localEulerAngles.z >= 350 && Hammer_grip.transform.localEulerAngles.z < 360)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
            else if (Hammer_grip.transform.localEulerAngles.z >= 0 && Hammer_grip.transform.localEulerAngles.z < 20)
            {
                speed *= -1;
                leftrotation_flag = true;
            }
        }
        Hammer_grip.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        if (add_power != 0)
        {
            Hammer_Instance.GetComponent<Hammer_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Hammer_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)  //耐久値の減少
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)    //耐久値がなくなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("arm");
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する残り耐久値の更新
    }

    void Instance_Bullets() //ハンマー生成処理
    {
        Quaternion muzzle_quaternion = transform.rotation;
        muzzle_quaternion.x = 0.00f;
        muzzle_quaternion.y = 0;
        muzzle_quaternion.z = 0;
        Hammer_Instance = Instantiate(Hammer, Muzzle.transform.position, muzzle_quaternion);
    }

    void Display_BulletsNumber()    //表示する残り耐久値の更新
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (GameObject.Find("Canvas/ArmButton") != null)
        {
            GameObject.Find("Canvas/ArmButton").GetComponent<Button>().interactable = true;
        }
        Destroy(Hammer_Instance);
    }
}
