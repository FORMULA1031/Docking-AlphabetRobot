using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera_Control : MonoBehaviour
{
    GameObject Player;  //プレイヤーオブジェクト
    Vector3 offset; //プレイヤーとカメラの座標の差

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //プレイヤーとカメラの座標の差を設定
        {
            Player = GameObject.Find("ZeroRobot");
            offset = transform.position - Player.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "StartMenuScene") //カメラの位置の更新
        {
            if (Player != null)
            {
                transform.position = Player.transform.position + offset;
            }
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
        else
        {   
            //スタート画面用のカメラ移動処理
            transform.Translate(0, 0, 0.2f, Space.World);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)   //画面を揺らす処理
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)  //duration秒揺らす
        {
            transform.position = originalPosition + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
