using UnityEngine;
using UnityEngine.UI;

public class PlayerEnhancement_Control : MonoBehaviour
{
    int stamina = 80;   //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間
    bool castof_flag = false;   //このパーツがパージされているかのフラグ
    Slider slider;  //耐久ゲージ
    Status_Control status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Controlスクリプト

    // Start is called before the first frame update
    void Start()    //エンハンスメントパーツの追加処理
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
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status_Control.add_power == status_Control.original_addpower)   //プレイヤーの攻撃の上昇
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Power(5);
        }
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
        slider.value = (float)stamina / (float)stamina_max; //表示している耐久値のバーの更新
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //このパーツがパージされていない場合
        {
            if (status_Control != null)
            {
                status_Control.Return_Power();
                castof_flag = true;
            }
        }
    }
}
