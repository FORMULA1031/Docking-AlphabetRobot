using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    GameObject EventSystem; //EventSystem�p�I�u�W�F�N�g
    AudioSource bgm;    //EventSystem�p��AudioSource
    public static float volume = 0.1f;  //����
    Slider volume_slider;   //���ʃo�[
    bool setting = false;   //�ݒ��ʂ��J���Ă��邩�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        load(); //���ʃf�[�^�̎擾
        EventSystem = this.gameObject;
        if(EventSystem.GetComponent<AudioSource>() != null)
        {
            bgm = EventSystem.GetComponent<AudioSource>();
        }
        if(GameObject.Find("Canvas/SettingPanel/BackgroundPanel/Slider") != null)
        {
            setting = true;
            volume_slider = GameObject.Find("Canvas/SettingPanel/BackgroundPanel/Slider").GetComponent<Slider>();
            volume_slider.value = volume;
        }
    }

    // Update is called once per frame
    void Update()   //���ʂ̍X�V
    {
        bgm.volume = volume;
        Setting_volume();
    }

    private void Setting_volume()   //���ʂ̕ύX
    {
        if (setting)
        {
            volume = volume_slider.value;
            save();
        }
    }

    public static void save()   //���݂̉��ʂ̃Z�[�u
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public static void load()   //���ʃf�[�^�̃��[�h
    {
        volume = PlayerPrefs.GetFloat("volume", 0.1f);
    }
}
