using UnityEngine;
using UnityEngine.UI;

public class PlayerInvisible_Control : MonoBehaviour
{
    int stamina = 40;   //耐久値
    int stamina_max;    //耐久値の最大値
    float serial_time = 0;  //耐久値の減少の遅延時間
    Slider slider;  //耐久値のバー
    GameObject Pressure;    //プレイヤーをロックオンする範囲
    bool stamina_flag = true;   //耐久値が残っているかのフラグ
    Transform player_transform; //プレイヤーのTransform
    MeshRenderer mesh;  //プレイヤー用のMeshRenderer

    // Start is called before the first frame update
    void Start()    //インビジブルパーツの追加処理
    {
        Transform parent = gameObject.transform.parent; //古いパーツの削除
        if (parent != null)
        {
            for (int i = 0; parent.childCount > i; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject)
                    Destroy(parent.GetChild(i).gameObject);
            }
        }
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y -= 90;
        transform.localRotation = Quaternion.Euler(rotation);
        stamina_max = stamina;
        slider = GameObject.Find("Canvas/BackpackWeaponMask/BackpackWeaponGauge").GetComponent<Slider>();
        Pressure = transform.root.gameObject.transform.Find("Pressure").gameObject;
        player_transform = gameObject.transform.root;   //プレイヤー透明化の処理
        mesh = player_transform.gameObject.GetComponent<MeshRenderer>();
        mesh.material.SetFloat("_Mode", 3);
        mesh.material.SetOverrideTag("RenderType", "Transparent");
        mesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mesh.material.SetInt("_ZWrite", 0);
        mesh.material.DisableKeyword("_ALPHATEST_ON");
        mesh.material.DisableKeyword("_ALPHABLEND_ON");
        mesh.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mesh.material.renderQueue = 3000;
        mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0.5f);
        Invisible_ChildObject(player_transform);
    }

    // Update is called once per frame
    void Update()
    {
        serial_time += Time.deltaTime;
        if (serial_time >= 0.3f)    //耐久値の減少
        {
            stamina--;
            serial_time = 0;
            Invisible_ChildObject(player_transform);    //プレイヤーの透明化処理
            Pressure.GetComponent<BoxCollider>().enabled = false;
        }

        if (stamina <= 0)   //耐久値が無くなった場合
        {
            stamina_flag = false;
            Pressure.GetComponent<BoxCollider>().enabled = true;
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //耐久値バーの更新
    }

    private void Invisible_ChildObject(Transform child) //プレイヤーの透明化処理
    {
        if (child.childCount == 0)
        {
            return;
        }
        for (int i = 0; child.childCount > i; i++)
        {
            if (child.GetChild(i).gameObject.GetComponent<MeshRenderer>() != null)
            {
                mesh = child.GetChild(i).gameObject.GetComponent<MeshRenderer>();
                mesh.material.SetFloat("_Mode", 3);
                mesh.material.SetOverrideTag("RenderType", "Transparent");
                mesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mesh.material.SetInt("_ZWrite", 0);
                mesh.material.DisableKeyword("_ALPHATEST_ON");
                mesh.material.DisableKeyword("_ALPHABLEND_ON");
                mesh.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                mesh.material.renderQueue = 3000;
                mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 0.5f);
            }
            Invisible_ChildObject(child.GetChild(i));
        }
    }

    private void NotInvisible_ChildObject(Transform child)  //プレイヤーの不透明処理
    {
        if (child.childCount == 0)
        {
            return;
        }
        for (int i = 0; child.childCount > i; i++)
        {
            if (child.GetChild(i).gameObject.GetComponent<MeshRenderer>() != null)
            {
                mesh = child.GetChild(i).gameObject.GetComponent<MeshRenderer>();
                mesh.material.SetFloat("_Mode", 0);
                mesh.material.SetOverrideTag("RenderType", "");
                mesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mesh.material.SetInt("_ZWrite", 1);
                mesh.material.DisableKeyword("_ALPHATEST_ON");
                mesh.material.DisableKeyword("_ALPHABLEND_ON");
                mesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mesh.material.renderQueue = -1;
                mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1.0f);
            }
            NotInvisible_ChildObject(child.GetChild(i));
        }
    }

    private void OnDestroy()
    {
        if (stamina_flag == false)  //耐久値が無くなった場合
        {
            mesh = player_transform.gameObject.GetComponent<MeshRenderer>();
            mesh.material.SetFloat("_Mode", 0);
            mesh.material.SetOverrideTag("RenderType", "");
            mesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mesh.material.SetInt("_ZWrite", 1);
            mesh.material.DisableKeyword("_ALPHATEST_ON");
            mesh.material.DisableKeyword("_ALPHABLEND_ON");
            mesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mesh.material.renderQueue = -1;
            mesh.material.color = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1.0f);
            NotInvisible_ChildObject(player_transform);
        }
    }
}
