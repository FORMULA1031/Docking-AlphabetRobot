using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyWall_Control : MonoBehaviour
{
    GameObject Player;  //プレイヤーオブジェクト
    Vector3 offset; //自オブジェクトとプレイヤーの座標の差

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //ゲームが開始されている場合
        {
            Player = GameObject.Find("ZeroRobot");
            offset = transform.position - Player.transform.position;
        }
    }

    private void FixedUpdate()  //自オブジェクトの座標更新
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //ゲーム開始用処理
        {
            if (Player != null)
            {
                transform.position = Player.transform.position + offset;
            }
            transform.position = new Vector3(0, 6, transform.position.z);
        }
        else
        {
            transform.Translate(0, 0, 0.2f, Space.World);
        }
    }

    //自オブジェクトと接触したオブジェクトを削除する
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
