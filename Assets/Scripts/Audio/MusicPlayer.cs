using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] musicFiles;
    [SerializeField] private int songLength = 10;
    [SerializeField] private float fadeTime = 2f;


    private int currentFile;
    private float currentPlayed;
    private bool isPlaying;

    private AudioSource audioSource;

    private float maxVolume;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        
        currentFile = -1;
        
        audioSource.clip = musicFiles[0];
        currentPlayed = 0;
        audioSource.Play();
        isPlaying = true;
    }

    void NextFile()
    {
        isPlaying = false;
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(audioSource.DOFade(0, fadeTime));
        mySequence.AppendCallback(() =>
        {
            currentFile++;
            if (currentFile > musicFiles.Length)
            {
                currentFile = 0;
            }
            audioSource.clip = musicFiles[currentFile];
            audioSource.Play();

            isPlaying = true;
        });
        mySequence.Append(audioSource.DOFade(maxVolume, fadeTime));

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) { return;}

        currentPlayed += Time.deltaTime;
        if (currentPlayed >= songLength)
        {
            NextFile();
        }

    }
}
