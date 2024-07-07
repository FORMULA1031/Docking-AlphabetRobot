using UnityEngine;

public class Missile_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    public GameObject BombBlast;    //爆発エフェクト
    int power = 80; //自オブジェクトの攻撃力

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * power, ForceMode.Impulse);
    }

    public void Change_Power(int _power)    //攻撃力の変更処理
    {
        power = _power;
    }

    private void OnCollisionEnter(Collision collision)  //自オブジェクトが他のオブジェクトと接触した場合
    {
        Instantiate(BombBlast, transform.position, Quaternion.identity);    //爆発エフェクトの生成
        Destroy(gameObject);
    }
}
