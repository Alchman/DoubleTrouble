using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : GenericSingletonClass<UIAudio>
{
    public AudioClip clickSound;
    public AudioClip closeSound;
    public AudioClip gameOverSound;
    public AudioClip vitorySound;
    public AudioClip startSound;
    
    
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.PlayOneShot(startSound);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(Instance.clickSound);
    }
    
    public void CloseSound()
    {
        audioSource.PlayOneShot(Instance.closeSound);
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
    public void GameOverSound()
    {
        audioSource.PlayOneShot(Instance.gameOverSound);
    }
    public void VictorySound()
    {
        audioSource.PlayOneShot(Instance.vitorySound);
    }
}
