using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] musicClips;
    private List<AudioClip> musicClipsToPlay = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        musicClipsToPlay = CopyAudioClips(musicClips, musicClipsToPlay);

        PlayNewRandomClip();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying && Time.frameCount >= 10)
        {
            PlayNewRandomClip();
        }
    }

    private AudioClip GetNewRandomAudioClip(List<AudioClip> audioClips)
    {
        int newTrackIndex = Random.Range(0, audioClips.Count);
        Debug.Log(newTrackIndex);
        AudioClip newTrack = audioClips[newTrackIndex];

        audioClips.RemoveAt(newTrackIndex);

        if(audioClips.Count == 0)
        {
            CopyAudioClips(musicClips, musicClipsToPlay);
        }

        return newTrack;
    }

    private List<AudioClip> CopyAudioClips(AudioClip[] musicClips, List<AudioClip> musicClipsCopied)
    {
        for(int i = 0; i < musicClips.Length; i++)
        {
            musicClipsCopied.Add(musicClips[i]);
        }

        return musicClipsCopied;
    }

    private void PlayNewRandomClip()
    {
        this.GetComponent<AudioSource>().clip = GetNewRandomAudioClip(musicClipsToPlay);
        this.GetComponent<AudioSource>().Play();
    }
}
