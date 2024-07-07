using UnityEngine;

public class TailRobot_Control : MonoBehaviour
{
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    GameObject Tail;    //尻尾オブジェクト
    Tail_Control Tail_Control;  //尻尾オブジェクトがコンポーネントしているTail_Controlスクリプト

    // Start is called before the first frame update
    void Start()
    {
        Tail = gameObject.transform.Find("Backpack/Tail/Sphere").gameObject;
        Tail_Control = Tail.GetComponent<Tail_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        Tail_Control.Set_Action(lockon_flag);   //尻尾オブジェクトの攻撃モーションを設定
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
}
