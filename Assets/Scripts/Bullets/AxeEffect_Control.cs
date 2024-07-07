using UnityEngine;

public class AxeEffect_Control : MonoBehaviour
{
    private Rigidbody rb;   //自オブジェクト用のRigidbody
    float time = 0; //自オブジェクトの存在している時間
    int power = 20; //自オブジェクトの攻撃力

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
        rotation.y += 70;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()   //0.5秒以上経過すると自オブジェクトを削除
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()  //自オブジェクトの移動
    {
        rb.velocity = new Vector3(10, rb.velocity.y, -3);
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")    //ヒット判定
        {
            if (other.gameObject.GetComponent<Status_Control>() != null)
            {
                other.gameObject.GetComponent<Status_Control>().Damage(power);
            }
            Destroy(gameObject);
        }
    }
}
