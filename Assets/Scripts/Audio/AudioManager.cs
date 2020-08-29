using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : GenericSingletonClass<AudioManager>
{
    [SerializeField] AudioSource effects;

    public static void PlaySound(AudioClip sound)
    {
        Instance.effects.PlayOneShot(sound);
    }
}
