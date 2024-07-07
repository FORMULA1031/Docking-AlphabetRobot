using UnityEngine;

public class Tail_Control : MonoBehaviour
{
    int power = 20; //自オブジェクトの攻撃力
    int speed = 5;  //自オブジェクトの速度
    bool action_flag = false;   //自オブジェクトが行動をとってよいかのフラグ
    bool leftrotation_flag = true;  //自オブジェクトが左回転するかのフラグ

    private void FixedUpdate()  //自オブジェクトの移動
    {
        if (action_flag)
        {
            if (leftrotation_flag)  //左回転
            {
                if (transform.localEulerAngles.x < 100 && transform.localEulerAngles.x >= 80)
                {
                    speed *= -1;
                    leftrotation_flag = false;
                }
            }
            if(!leftrotation_flag)  //右回転
            {
                if (transform.localEulerAngles.x > 260 && transform.localEulerAngles.x <= 280)
                {
                    speed *= -1;
                    leftrotation_flag = true;
                }
            }
            transform.Rotate(new Vector3(speed, 0, 0));
        }
    }

    public void Set_Action(bool _actionflag)    //自オブジェクトが行動するかを決定する
    {
        action_flag = _actionflag;
    }

    private void OnCollisionEnter(Collision other)  //ヒット判定
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
        }
        if (other.gameObject.tag == "Player" && gameObject.tag != "Untagged")
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                Destroy(gameObject.transform.root.gameObject);
            }
        }

        if(gameObject.tag == "Untagged")
        {
            if (leftrotation_flag)
            {
                leftrotation_flag = false;
            }
            else
            {
                leftrotation_flag = true;
            }
            speed *= -1;
        }
    }
}
