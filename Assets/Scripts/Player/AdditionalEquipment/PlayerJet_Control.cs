using UnityEngine;
using UnityEngine.UI;

public class PlayerJet_Control : MonoBehaviour
{
    int stamina = 100;  //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間
    bool castof_flag = false;   //このパーツをパージしたかのフラグ
    Slider slider;  //耐久値用のバー

    // Start is called before the first frame update
    void Start()    //ジェットパーツの追加処理
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
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの機動力を上げる
        if (transform.root.gameObject.GetComponent<Status_Control>().speed == transform.root.gameObject.GetComponent<Status_Control>().original_speed)
        {
            transform.root.gameObject.GetComponent<Status_Control>().Add_Speed(3);
        }
        serial_time += Time.deltaTime;
        if(serial_time >= 0.3f) //耐久値の減少処理
        {
            stamina--;
            serial_time = 0;
        }

        if(stamina <= 0)    //耐久値が無くなった場合
        {
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //耐久値用のバーの更新
    }

    private void OnDestroy()
    {
        if (!castof_flag)   //このパーツがパージされていない場合
        {
            transform.root.gameObject.GetComponent<Status_Control>().Return_Speed();
            castof_flag = true;
        }
    }
}
