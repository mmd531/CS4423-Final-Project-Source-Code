using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;
    public float volume = 0.5f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = musicClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (musicClip != null)
        {
            audioSource.Play();
        }
    }
}