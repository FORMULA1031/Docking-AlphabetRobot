using UnityEngine;

public class HeadItem_Control : MonoBehaviour
{
    GameObject Player;  //プレイヤーオブジェクト
    public GameObject weapon;   //装備するヘッドパーツ

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("ZeroRobot");
    }

    private void FixedUpdate()  //自オブジェクトの回転処理
    {
        transform.Rotate(new Vector3(0, 3, 0));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")   //プレイヤーと接触した場合
        {
            Player.GetComponent<Core_Control>().AdditionalEquipment("head", weapon, null);  //ヘッドパーツの装備処理
            Destroy(gameObject);
        }
    }
}
