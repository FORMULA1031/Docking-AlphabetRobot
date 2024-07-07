using UnityEngine;
using UnityEngine.UI;

public class PlayerTail_Control : MonoBehaviour
{
    public GameObject Tail; //生成するテール
    Tail_Control Tail_Control;  //生成したテールがコンポーネントしているTail_Controlスクリプト
    GameObject Tail_Instance;   //生成したテール
    Slider slider;  //耐久値用のバー
    int stamina = 100;  //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間

    // Start is called before the first frame update
    void Start()    //テールパーツの追加処理
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
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Tail_Instance = Instantiate(Tail, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation);
        Tail_Control = Tail_Instance.GetComponent<Tail_Control>();
        Tail_Control.Set_Action(true);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        serial_time += Time.deltaTime;
        if (serial_time >= 0.3f)    //耐久値の減少
        {
            stamina--;
            serial_time = 0;
        }

        if (stamina <= 0)   //耐久値が無くなった場合
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //耐久値用のバーの更新
    }

    private void FixedUpdate()  //生成したテールの座標処理
    {
        Tail_Instance.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 1);
    }

    private void OnDestroy()
    {
        Destroy(Tail_Instance);
    }
}
