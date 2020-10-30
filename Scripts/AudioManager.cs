using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _laserAudio;

    [SerializeField]
    private AudioSource _explosionAudio;

    [SerializeField]
    private AudioSource _powerupAudio;

    public void LaserShot()
    {
        _laserAudio.Play();
    }

    public void ExplosionSound()
    {
        _explosionAudio.Play();
    }

    public void PowerupSound()
    {
        _powerupAudio.Play();
    }
}
