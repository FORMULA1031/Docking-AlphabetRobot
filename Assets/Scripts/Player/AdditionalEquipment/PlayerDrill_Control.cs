using UnityEngine;
using UnityEngine.UI;

public class PlayerDrill_Control : MonoBehaviour
{
    public GameObject Effect;   //ドリル用のエフェクト
    GameObject Effect_Instance; //生成したドリルエフェクト
    GameObject Muzzle;  //生成するドリルエフェクトの座標オブジェクト
    int bullets_number = 20;    //制限数
    float revolution_time = 0f; //制限数の減少の遅延時間
    Text WeaponNumber_text; //表示する制限数テキスト
    GameObject Player;  //プレイヤーオブジェクト
    Status_Control Status_Control;  //プレイヤーオブジェクトがコンポーネントしているStatus_Controlスクリプト
    int add_power = 0;  //強化する攻撃力の値

    // Start is called before the first frame update
    void Start()    //ドリルパーツの追加処理
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
        Muzzle = transform.Find("Muzzle").gameObject;
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        Player = GameObject.Find("ZeroRobot");
        WeaponNumber_text = GameObject.Find("Canvas/WeaponPanel(Head)/WeaponNumber").GetComponent<Text>();
        Quaternion muzzle_quaternion = transform.rotation;
        Effect_Instance = Instantiate(Effect, Muzzle.transform.position, muzzle_quaternion);
        Vector3 drilleffect_rotation = Effect_Instance.transform.localRotation.eulerAngles;
        drilleffect_rotation.y -= 90;
        Effect_Instance.transform.localRotation = Quaternion.Euler(drilleffect_rotation);
        GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = false;
        Status_Control = transform.root.gameObject.GetComponent<Status_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        add_power = Status_Control.add_power;   //強化する攻撃力の値の更新
        if (add_power != 0)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Enhancement(add_power);
        }
        revolution_time += Time.deltaTime;
        Effect_Instance.transform.position = Muzzle.transform.position;
        if (revolution_time >= 1f)  //制限数の減少
        {
            revolution_time = 0;
            bullets_number--;
        }

        if (bullets_number <= 0)    //制限数がなくなった場合
        {
            Player.GetComponent<Core_Control>().CastOf("head");
            GameObject.Find("Canvas/HeadButton").GetComponent<Button>().interactable = true;
            Destroy(Effect_Instance);
            Destroy(gameObject);
        }
        Display_BulletsNumber();    //表示する残り制限数の更新
    }

    void Display_BulletsNumber()    //表示する残り制限数の更新
    {
        WeaponNumber_text.text = "" + bullets_number;
    }

    private void OnDestroy()
    {
        if (Effect_Instance != null)
        {
            Effect_Instance.GetComponent<DrillEffect_Control>().Destroy_Flag();
        }
    }
}
