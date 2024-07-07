using UnityEngine;

public class Hat_Control : MonoBehaviour
{
    int power = 5;  //自オブジェクトの攻撃力
    public bool hit_flag = false;   //自オブジェクトが他のオブジェクトと接触したかのフラグ
    bool enhancement_flag = false;  //自オブジェクトが強化中かのフラグ

    public void Hit_Reset() //ヒット判定後の初期化
    {
        hit_flag = false;
    }

    public void Enhancement(int _add_power) //自オブジェクトが強化中の処理
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //直撃判定
        {
            if (!hit_flag)
            {
                if (other.gameObject.GetComponent<Status_Control>() != null)
                {
                    other.gameObject.GetComponent<Status_Control>().Damage(power);
                    hit_flag = true;
                }
            }
        }
        else
        {
            hit_flag = true;
        }
    }
}
