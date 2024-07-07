using UnityEngine;

public class KnifeEffect_Control : MonoBehaviour
{
    int power = 20; //自オブジェクトの攻撃力
    public bool hit_flag = false;   //自オブジェクトが他のオブジェクトと接触したかのフラグ

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーと接触した場合
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            hit_flag = true;
        }

        if(other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")    //敵と接触した場合
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
    }
}
