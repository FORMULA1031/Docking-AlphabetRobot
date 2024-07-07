using UnityEngine;

public class Hammer_Control : MonoBehaviour
{
    int power = 25; //自オブジェクトの攻撃力
    bool enhancement_flag = false;  //自オブジェクトが強化中かのフラグ

    public void Enhancement(int _add_power) //自オブジェクトの強化処理
    {
        if (!enhancement_flag)
        {
            power += _add_power;
            enhancement_flag = true;
        }
    }

    public void Destroy_Flag()  //自オブジェクトの削除
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)  //ヒット判定
    {
        if (other.gameObject.tag == "Player" && gameObject.name != "Hammer(Clone)")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
        if (other.gameObject.tag == "Enemy" && gameObject.name != "Hammer_Enemy(Clone)")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
    }
}
