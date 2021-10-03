using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController musCtr;
    public static MusicController instance { get { return musCtr; } }
    void OnEnable() { musCtr = this; }
    void OnDisable() { musCtr = null; }


    public AudioSource crashMusic;

    public List<AudioSource> songs = new List<AudioSource>();


    System.Random rand;
    AudioSource prevSong;
    // Start is called before the first frame update
    void Start()
    {
        rand = MarketController.instance.rand;

        prevSong = songs[0];
        prevSong.Play();
    }

    public void PlayCrashMusic() {
        prevSong.Stop();
        prevSong = crashMusic;
        prevSong.Play();
    }

    private void Update() {
        if(prevSong.time / prevSong.clip.length > 0.99f) { // Time to play a new song
            if (CrashController.instance.crashHappening) {
                prevSong.Play();
                return;
            }

            AudioSource newPlay = prevSong;
            while (newPlay == prevSong) {
                newPlay = songs[rand.Next(0, songs.Count)];
            }
            prevSong = newPlay;
            prevSong.Play();
        }
    }
}
