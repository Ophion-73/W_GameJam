using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Musica : MonoBehaviour
{
    public AudioClip clipToPlay;
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clipToPlay;
        audioSource.loop = true;    // make it loop
        audioSource.playOnAwake = true; // start immediately
        audioSource.volume = volume;
    }

    void Start()
    {
        if (clipToPlay != null)
            audioSource.Play();
        else
            Debug.LogWarning("No audio clip assigned to LoopingAudio.");
    }
}
