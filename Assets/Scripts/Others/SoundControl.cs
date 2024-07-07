using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    GameObject EventSystem; //EventSystem用オブジェクト
    AudioSource bgm;    //EventSystem用のAudioSource
    public static float volume = 0.1f;  //音量
    Slider volume_slider;   //音量バー
    bool setting = false;   //設定画面を開いているかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        load(); //音量データの取得
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
    void Update()   //音量の更新
    {
        bgm.volume = volume;
        Setting_volume();
    }

    private void Setting_volume()   //音量の変更
    {
        if (setting)
        {
            volume = volume_slider.value;
            save();
        }
    }

    public static void save()   //現在の音量のセーブ
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public static void load()   //音量データのロード
    {
        volume = PlayerPrefs.GetFloat("volume", 0.1f);
    }
}
