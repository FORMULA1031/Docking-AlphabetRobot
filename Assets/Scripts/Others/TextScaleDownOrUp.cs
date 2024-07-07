using UnityEngine;

public class TextScaleDownOrUp : MonoBehaviour
{
    float time = 0.0f;  //広げる(縮める)計測時間
    float changeSpeed = 0.0f;   //広げる(縮める)大きさ
    public bool enlarge;    //広げるかのフラグ

    void Start()
    {
        enlarge = true;
    }

    void Update()
    {
        changeSpeed = Time.deltaTime * 0.1f;

        if (time < 0)
        {
            enlarge = true;
        }
        if (time > 0.7f)
        {
            enlarge = false;
        }

        if (enlarge == true)    //テキストを広げる
        {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {   //テキストを縮める
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
    }
}