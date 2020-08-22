using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip[] footsteps;

    public void FootstepSound()
    {
        AudioClip randomSound = footsteps[Random.Range(0, footsteps.Length)];
        playerAudio.PlayOneShot(randomSound);
    }

    public void FootstepSoundLeft()
    {

    }

    public void FootstepSoundRight()
    {

    }
}
