using UnityEngine;
using UnityEngine.UI;

public class PlayerShield_Control : MonoBehaviour
{
    public GameObject Shield;   //生成するシールド
    GameObject Shield_Instance; //生成したシールド
    GameObject Muzzle;  //シールドを生成する座標オブジェクト
    Slider slider;  //耐久値用のバー
    int stamina = 80;   //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間
    Vector3 rotation_shield;    //シールドの向き

    // Start is called before the first frame update
    void Start()    //シールドパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
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
        Muzzle = transform.Find("Muzzle").gameObject;
        Shield_Instance = Instantiate(Shield, new Vector3(Muzzle.transform.position.x, Muzzle.transform.position.y, Muzzle.transform.position.z), transform.rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        rotation_shield = Shield_Instance.transform.localRotation.eulerAngles;
        rotation_shield.y += 90;
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

    private void FixedUpdate()  //生成したシールドの動作処理
    {
        Shield_Instance.transform.position = new Vector3(Muzzle.transform.position.x, Muzzle.transform.position.y, Muzzle.transform.position.z);
        Shield_Instance.transform.localRotation = Quaternion.Euler(rotation_shield);
    }

    private void OnDestroy()
    {
        Destroy(Shield_Instance);
    }
}
