using UnityEngine;

public class Explosion_Control : MonoBehaviour
{
    AudioSource AudioSource;    //���I�u�W�F�N�g�p��AudioSource
    public AudioClip explosion_se;  //�����p��SE

    // Start is called before the first frame update
    void Start()    //�����p��SE��炷
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(explosion_se);
    }
}
