using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioSource heaven;
    public AudioSource hell;
    public AudioSource heaven1;
    public AudioSource hell1;

    private void Awake()
    {
        heaven.PlayDelayed(20f);
        heaven.loop = false;
        heaven1.PlayDelayed(200f);
        heaven1.loop = false;
        hell.PlayDelayed(600f);
        hell.loop = true;
        hell1.PlayDelayed(1000f);
        hell1.loop = true;
    }
}
