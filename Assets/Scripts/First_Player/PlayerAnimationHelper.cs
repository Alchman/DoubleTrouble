using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimationHelper : MonoBehaviour
{
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] private Animator animator;
    
    [SerializeField]  FirstPlayer firstPlayer;

    private void Awake()
    {
        if (firstPlayer == null)
        {
            firstPlayer = GetComponentInParent<FirstPlayer>();
        }
    }

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

    public void HitObjects()
    {
        firstPlayer.HitObjects();
    }

    public void Sand() {
        animator.SetInteger("Sand", 1);
    }
}
