using UnityEngine;

public class SceneDJ : MonoBehaviour
{
    [SerializeField] AudioClip[] UIAudio;
    [SerializeField] AudioClip[] Flamethrower;
    [SerializeField] AudioClip[] Fireball;
    AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }


    public void PlayUISound()
    {
       // audiosource.PlayOneShot(UIAudio[0]);
        AudioSource.PlayClipAtPoint(UIAudio[0],transform.position);
        Debug.Log("Someone Played UI Sound i don't know if you heard it or not");

    }

    public void PlayFlameThrowerAudio()
    {
        audiosource.PlayOneShot(Flamethrower[UnityEngine.Random.Range(0, Flamethrower.Length)]);
        Debug.Log("Someone Played Flamethrower Sound i don't know if you heard it or not");


    }

    public void PlayFireballAudio()
    {
        audiosource.PlayOneShot(Fireball[UnityEngine.Random.Range(0, Fireball.Length)]);

    }



}
