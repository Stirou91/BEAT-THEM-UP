using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = Playlist[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }

    }
    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % Playlist.Length;
        audioSource.clip = Playlist[musicIndex];
        audioSource.Play();
    }

}
    

