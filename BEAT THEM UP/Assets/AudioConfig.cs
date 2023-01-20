using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Game/Audio Config", order = 10)]
public class AudioConfig : ScriptableObject
{
    [Range(0, 1f)]
    public float Volume = 3f;
    public AudioListener[] GameSongs;
    public AudioClip[] PlayerSong;
    public AudioClip[] EnemiesSong;
    public AudioClip[] ReloadSong;

    public void PlayGameSong(AudioListener AudioSource, bool ReloadSongs = false)
    {
        if (true)
        {

        }
        else
        {

        }

    }

    public void PlayerGameSong(AudioListener AudioSource, bool PlayerSong = false)
    {
        if (true)
        {

        }
        else
        {

        }
    }

    public void EnemiGameSong(AudioListener AudioSource, bool EnemiesSong = false)
    {
        if (true)
        {

        }
        else
        {

        }
    }

    public void RealoadGameSong(AudioListener AudioSource, bool ReloadSong = false)
    {
        if (true)
        {

        }
        else
        {

        }
    }
}












