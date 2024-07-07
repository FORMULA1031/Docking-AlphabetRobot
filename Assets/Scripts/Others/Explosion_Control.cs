using UnityEngine;

public class Explosion_Control : MonoBehaviour
{
    AudioSource AudioSource;    //自オブジェクト用のAudioSource
    public AudioClip explosion_se;  //爆発用のSE

    // Start is called before the first frame update
    void Start()    //爆発用のSEを鳴らす
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(explosion_se);
    }
}
