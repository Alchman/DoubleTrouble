using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    [SerializeField] AudioSource effects;

    public void PlaySound(AudioClip sound)
    {
        effects.PlayOneShot(sound);
    }
}
