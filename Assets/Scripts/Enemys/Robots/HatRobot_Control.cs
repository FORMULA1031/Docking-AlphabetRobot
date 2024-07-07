using UnityEngine;

public class HatRobot_Control : MonoBehaviour
{
    GameObject Muzzle;  //ハットの正位置
    public GameObject Hat;  //ハット
    GameObject Hat_Instance;    //生成したハット
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    float serial_time = 0;  //攻撃するまでの遅延時間

    // Start is called before the first frame update
    void Start()
    {
        Muzzle = transform.Find("Head/Muzzle").gameObject;
        Quaternion muzzle_quaternion = transform.rotation;
        Hat_Instance = Instantiate(Hat, Muzzle.transform.position, muzzle_quaternion);
        Vector3 rotation = Hat_Instance.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        Hat_Instance.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockon_flag)    //プレイヤーをロックオンした場合
        {
            serial_time += Time.deltaTime;
        }
        if (Hat_Instance.GetComponent<Hat_Control>().hit_flag)  //ハットの正位置処理
        {
            Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Hat_Instance.transform.position = Muzzle.transform.position;
            serial_time = 0;
            Hat_Instance.GetComponent<Hat_Control>().Hit_Reset();
        }
    }

    private void FixedUpdate()
    {
        if (!lockon_flag)
        {
            Hat_Instance.transform.position = Muzzle.transform.position;
        }

        if (lockon_flag)    //攻撃中のハットの処理
        {
            if (serial_time >= 2 && serial_time < 3.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * 8f * Time.deltaTime;
            }
            else if (serial_time >= 3.0f && serial_time < 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = transform.right * -8f * Time.deltaTime;
            }
            else if(serial_time >= 4.0f)
            {
                Hat_Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Hat_Instance.transform.position = Muzzle.transform.position;
                serial_time = 0;
            }
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
        Destroy(Hat_Instance);
    }
}
