using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicClips = new List<AudioClip>();

    private AudioSource musicSource;

    AudioClip currentTrack;
    private float lenght;

    private static MusicPlayer instance;
    private Coroutine musicLoop;
    private MusicQueue musicQueue;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        musicQueue = new MusicQueue(musicClips);

        musicSource = GetComponent<AudioSource>();

        StartMusic();

    }


    public void PlayMusicClip(AudioClip music)
    {
        //Changing the clip
        musicSource.Stop();

        musicSource.clip = music;

        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicLoop != null)
        {
            StopCoroutine(musicLoop);
        }
    }

    public void StartMusic()
    {
        musicLoop = StartCoroutine(musicQueue.LoopMusic(this, 0f, PlayMusicClip));
    }
}

public class MusicQueue
{
    private List<AudioClip> clips;

    public MusicQueue(List<AudioClip> clips)
    {
        this.clips = clips;
    }

    public IEnumerator LoopMusic(MonoBehaviour player, float delay, System.Action<AudioClip> playFunction)
    {
        while (true)
        {
            yield return player.StartCoroutine(Run(RandomizeList(clips), delay, playFunction));
        }
    }
    public IEnumerator Run(List<AudioClip> tracks, float delay, System.Action<AudioClip> playFunction)
    {
        foreach (AudioClip clip in tracks)
        {
            playFunction(clip);

            yield return new WaitForSeconds(clip.length + delay);
        }
    }

    public List<AudioClip> RandomizeList(List<AudioClip> list)
    {
        List<AudioClip> copy = new List<AudioClip>(list);

        int n = copy.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);

            AudioClip value = copy[k];

            copy[k] = copy[n];
            copy[n] = value;
        }

        return copy;
    }
}
