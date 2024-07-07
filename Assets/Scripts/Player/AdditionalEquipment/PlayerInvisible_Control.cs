using UnityEngine;
using UnityEngine.UI;

public class PlayerInvisible_Control : MonoBehaviour
{
    int stamina = 40;   //�ϋv�l
    int stamina_max;    //�ϋv�l�̍ő�l
    float serial_time = 0;  //�ϋv�l�̌����̒x������
    Slider slider;  //�ϋv�l�̃o�[
    GameObject Pressure;    //�v���C���[�����b�N�I������͈�
    bool stamina_flag = true;   //�ϋv�l���c���Ă��邩�̃t���O
    Transform player_transform; //�v���C���[��Transform
    MeshRenderer mesh;  //�v���C���[�p��MeshRenderer

    // Start is called before the first frame update
    void Start()    //�C���r�W�u���p�[�c�̒ǉ�����
    {
        Transform parent = gameObject.transform.parent; //�Â��p�[�c�̍폜
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
        player_transform = gameObject.transform.root;   //�v���C���[�������̏���
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
        if (serial_time >= 0.3f)    //�ϋv�l�̌���
        {
            stamina--;
            serial_time = 0;
            Invisible_ChildObject(player_transform);    //�v���C���[�̓���������
            Pressure.GetComponent<BoxCollider>().enabled = false;
        }

        if (stamina <= 0)   //�ϋv�l�������Ȃ����ꍇ
        {
            stamina_flag = false;
            Pressure.GetComponent<BoxCollider>().enabled = true;
            Destroy(gameObject);
        }
        slider.value = (float)stamina / (float)stamina_max; //�ϋv�l�o�[�̍X�V
    }

    private void Invisible_ChildObject(Transform child) //�v���C���[�̓���������
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

    private void NotInvisible_ChildObject(Transform child)  //�v���C���[�̕s��������
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
        if (stamina_flag == false)  //�ϋv�l�������Ȃ����ꍇ
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
