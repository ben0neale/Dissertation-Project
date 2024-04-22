using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    [SerializeField] private AudioSource CointCollect;
    [SerializeField] private AudioSource PowerupCollect;
    [SerializeField] private AudioSource CrashCollect;
    [SerializeField] private AudioSource Skid;

    public void PlayCoin()
    {
        CointCollect.Play();
    }

    public void PlayPowerup()
    {
        PowerupCollect.Play();
    }

    public void PlayCrash()
    {
        CrashCollect.Play();
    }

    public void PlaySkid()
    {
        Skid.Play();
    }
}
