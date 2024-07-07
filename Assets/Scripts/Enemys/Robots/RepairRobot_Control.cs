using UnityEngine;

public class RepairRobot_Control : MonoBehaviour
{
    bool lockon_flag = false;   //プレイヤーをロックオンしたかのフラグ
    bool move_flag = false; //自オブジェクトが移動中かのフラグ

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーがロックオン距離にいた場合
        {
            if (!lockon_flag && !move_flag)
            {
                gameObject.GetComponent<Status_Control>().Add_Speed(-3);
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
}
