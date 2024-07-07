using UnityEngine;

public class DrillEffect_Control : MonoBehaviour
{
    int power = 20; //自オブジェクトの攻撃力
    public bool hit_flag = false;   //自オブジェクトが他のオブジェクトと接触したかのフラグ
    bool enhancement_flag = false;  //自オブジェクトが強化中かのフラグ

    public void Enhancement(int _add_power) //自オブジェクトの強化処理
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    public void Destroy_Flag()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //ヒット判定
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            hit_flag = true;
        }
    }
}
