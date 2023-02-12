using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    // creates heaven and hell audio source
    public AudioSource heaven;
    public AudioSource hell;
    public AudioSource heaven1;
    public AudioSource hell1;

    private void Awake()
    {
        // Sets delayed play and loop for heaven
        heaven.PlayDelayed(20f);
        heaven.loop = false;
        heaven1.PlayDelayed(200f);
        heaven1.loop = false;
        // Sets delayed play and loop for hell
        hell.PlayDelayed(600f);
        hell.loop = true;
        hell1.PlayDelayed(1000f);
        hell1.loop = true;
    }
    // Where i got my audio files from
    // https://freesound.org/people/ElanHickler/sounds/108906/
    // https://freesound.org/people/Leady/sounds/26727/
    // https://freesound.org/people/X3nus/sounds/476782/
    // 
}
