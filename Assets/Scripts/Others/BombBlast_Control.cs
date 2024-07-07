using UnityEngine;

public class BombBlast_Control : MonoBehaviour
{
    int power = 3;  //自オブジェクトの攻撃力
    float serial_time = 0;  //自オブジェクトの存在している時間

    void Update()   //0.5秒後に自オブジェクトを削除する
    {
        serial_time += Time.deltaTime;
        if(serial_time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)  //ヒット判定
    {
        if (other.gameObject.tag == "Enemy" && gameObject.tag != "Enemy")   //敵と接触
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
        if (other.gameObject.tag == "Player")   //プレイヤーと接触
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
                gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
    }
}
