using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;

    private int musicIndex = 0;

    public AudioMixerGroup SoundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    private void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = SoundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
