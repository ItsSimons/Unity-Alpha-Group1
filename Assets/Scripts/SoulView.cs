using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulView : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI description_text;
    public string[] available_names;
    public string[] available_punishments;
    public SoulGenerator soul_gen;

    public void generateSoul()
    {
        name_text.text = available_names[Random.Range(0, available_names.Length)];
        string temp_str;
        int rand_pun_or_rew = Random.Range(0, available_punishments.Length);
        if (rand_pun_or_rew > 6)
        {
            temp_str = "Rewarded for " + available_punishments[rand_pun_or_rew] + "\n";
        }
        else
        {
            temp_str = "Punished for " + available_punishments[rand_pun_or_rew] + "\n";
        }
        temp_str = temp_str + "Years left at location: " + Random.Range(0, 1000) + "\n";
        temp_str = temp_str + "Believes in " + soul_gen.generateBeliefs() + "ism";
        description_text.text = temp_str;
    }
}
