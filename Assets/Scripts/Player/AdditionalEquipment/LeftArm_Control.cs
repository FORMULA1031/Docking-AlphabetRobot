using UnityEngine;

public class LeftArm_Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = gameObject.transform.parent; //不要なパーツを削除
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
