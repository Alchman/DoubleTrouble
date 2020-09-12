using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : GenericSingletonClass<UIAudio>
{
    public AudioClip clickSound;
    public AudioClip closeSound;
    
    
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(Instance.clickSound);
    }
    
    public void CloseSound()
    {
        audioSource.PlayOneShot(Instance.closeSound);
    }
    
}
