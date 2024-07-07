using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    Slider slider;  //表示する耐久値ゲージ
    GameObject Player;  //プレイヤーオブジェクト
    Status_Control Status_Control;  //プレイヤーがコンポーネントしているStatus_Controlスクリプト
    public Slider slider_red;   //表示する耐久値ゲージの遅延ゲージ
    float damage_amount = 0;    //遅延するダメージ量
    float time = 0.0f;  //ダメージ減少の遅延時間
    bool downgauge_flag = true; //遅延ゲージを減少してよいかのフラグ
    bool startdowngauge_flag = true;    //ダメージを減少したかのフラグ
    float criterion = 0.0f; //遅延ゲージの減少させる設定の更新をさせるかの基準

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        Player = GameObject.Find("ZeroRobot");
        Status_Control = Player.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)Status_Control.stamina / (float)Status_Control.stamina_max;   //耐久値の更新

        if (slider_red.value > slider.value && downgauge_flag)  //遅延した耐久値が耐久値よりも多い場合
        {
            if (startdowngauge_flag || (slider_red.value - slider.value) / 5 > criterion)   //遅延ゲージの減少させる設定の更新
            {
                damage_amount = (slider_red.value - slider.value) / 5;
                criterion = damage_amount;
                startdowngauge_flag = false;
            }
            slider_red.value -= damage_amount;  //遅延ゲージの減少
            downgauge_flag = false;
        }
        else if (!startdowngauge_flag)  //遅延ゲージ処理のリセット
        {
            startdowngauge_flag = true;
        }

        if (!downgauge_flag)    //ゲージ減少の遅延時間
        {
            time += Time.deltaTime;
            if(time >= 0.1f)
            {
                downgauge_flag = true;
                time = 0;
            }
        }
    }
}
