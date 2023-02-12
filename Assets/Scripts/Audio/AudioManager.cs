using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // All sounds by title
    public enum SoundName {Theme1, Theme2, Theme3, Theme4, Theme5, Theme6, Theme7, Click, Click2, Error, GateHeaven, GateHell, Demolish1, Demolish2, Demolish3, Limbo, Station, Port, InstituteHeaven, InstituteHell, Bank, TopiaHeaven, TopiaHell, SpecialHeaven, SpecialHell};
    public AudioClip[] audios;
    private List<GameObject> generated_sounds = new List<GameObject>();
    public GameObject aud_prefab;

    void Start()
    {
        generateSound(SoundName.Theme2);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown("v"))
        {
            generateSound(SoundName.Click);
        }
        */
    }

    public void generateSound(SoundName aud_index)
    {
        if (generated_sounds.Count != 0)
        {
            foreach (GameObject sound in generated_sounds)
            {
                if (sound.GetComponent<AudioSource>().clip == audios[(int)aud_index])
                {
                    sound.GetComponent<AudioSource>().Play();
                    return;
                }
            }
        }
        GameObject aud = Instantiate(aud_prefab, transform.position, transform.rotation);
        aud.GetComponent<AudioSource>().clip = audios[(int)aud_index];
        aud.GetComponent<AudioSource>().Play();
        aud.transform.parent = this.transform;
        generated_sounds.Add(aud);
        if ((int)aud_index < 7)
        {
            aud.GetComponent<AudioSource>().loop = true;
        }
    }
}
