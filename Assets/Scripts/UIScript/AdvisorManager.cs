using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdvisorManager : MonoBehaviour
{
    public enum HeavenOrHell { Heaven, Hell, Both, Neither};
    public enum JasperOrArea { Jasper, Aria};

    [SerializeField] public string[] dialogue_arr;
    [SerializeField] private bool dialogue_finished = true;
    [SerializeField] private int current_dialogue;

    [SerializeField] private JasperOrArea current_speaking;
    [SerializeField] public GameObject dialogue_box;
    [SerializeField] public RawImage box_image;
    [SerializeField] public AdvisorOptionBox[] option_boxes;
    [SerializeField] public Texture[] box_sprites;

    [SerializeField] public float time_between_dialogue;
    [SerializeField] public float time_dialogue_tracker;
    [SerializeField] private int next_dialogue_index;

    [SerializeField] public List<int> dialogue_starts;
    [SerializeField] public List<string> dialogue_titles;
    [SerializeField] public List<HeavenOrHell> dialogue_standpoint;

    [SerializeField] public List<int> current_faults;
    [SerializeField] public bool update_check;

    //Debug Variables
    private bool conditionCheck0 = true;
    private bool conditionCheck1 = true;
    //---------------

    // Start is called before the first frame update
    void Start()
    {
        dialogue_box.GetComponent<TextMeshProUGUI>().text = "";
        UpdateOptions();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAdvise();
        CheckFaults();
    }

    public void UpdateAdvise()
    {
        if (!dialogue_finished)
        {
            if (time_dialogue_tracker >= time_between_dialogue)
            {
                box_image.texture = box_sprites[0];
                // If there is continuous dialogue
                if (next_dialogue_index >= 0)
                {
                    // Then produce dialogue again
                    time_dialogue_tracker = 0;
                    current_dialogue = next_dialogue_index;
                    next_dialogue_index = CheckSpecialDialogue();
                }
                else
                {
                    // Otherwise remove dialogue after timer
                    dialogue_finished = true;
                    dialogue_box.GetComponent<TextMeshProUGUI>().text = "";
                }
            }
            else
            {
                // Animation
                if ((int)(time_dialogue_tracker*4) % 2 == 0)
                {
                    if (current_speaking == JasperOrArea.Jasper)
                    {
                        box_image.texture = box_sprites[1];
                    }
                    else
                    {
                        box_image.texture = box_sprites[2];
                    }
                }
                else
                {
                    box_image.texture = box_sprites[0];
                }
                time_dialogue_tracker += Time.deltaTime;
            }
        }
    }

    public void CheckFaults()
    {
        update_check = false;
        // This is where all the condition based dialogue is checked for
        if (conditionCheck0)
        {
            if (!current_faults.Contains(0))
            {
                current_faults.Add(0);
                conditionCheck0 = false;
                update_check = true;
            }
        }
        if (conditionCheck1)
        {
            if (!current_faults.Contains(1))
            {
                current_faults.Add(1);
                conditionCheck1 = false;
                update_check = true;
            }
        }
        if (update_check)
        {
            UpdateOptions();
        }
    }

    public void UpdateOptions()
    {
        for (int i = 0; i < current_faults.Count; i ++)
        {
            option_boxes[i].GetComponent<AdvisorOptionBox>().SetIndicators(dialogue_standpoint[current_faults[i]], dialogue_titles[current_faults[i]]);
        }
        update_check = false;
    }

    public void GenerateAdvise(int advise_index)
    {
        // Check if dialogue needs to be displayed
        if (dialogue_finished)
        {
            time_dialogue_tracker = 0;
            current_dialogue = dialogue_starts[advise_index];
            next_dialogue_index = CheckSpecialDialogue();
            dialogue_finished = false;
            //StartCoroutine(Advise());
        }
    }

    /*
    // Please dont use coroutine in update function, it will cause the game entire game to wait for the coroutine to finish before the next tick
    private IEnumerator Advise()
    {
        // Set talking to true so no repeated talking
        dialogue_finished = false;
        // Change dialogue text
        while (!dialogue_finished)
        {
            int next_dialogue_index = CheckSpecialDialogue();
            // Start talking animation

            // -----FUTURE ANIMATION CODE-----

            yield return new WaitForSeconds(time_between_dialogue);
            // If there is continuous dialogue
            if (next_dialogue_index >= 0)
            {
                // Then produce dialogue again
                current_dialogue = next_dialogue_index;
            }
            else
            {
                // Otherwise remove dialogue after timer
                dialogue_box.text = "";
                yield return new WaitForSeconds(1);
                dialogue_finished = true;
            }
        }
    }
    */

    private int CheckSpecialDialogue()
    {
        // This finds any sequencial dialogue and removes the numbers and slashes from the string
        string t_string = dialogue_arr[current_dialogue];
        int t_index = -1;
        while (t_string.Contains("/"))
        {
            int special_index = t_string.IndexOf('/');
            string t_index_string = "";
            bool end_check = false;
            char char_check;
            t_string = t_string.Remove(special_index, 1);
            while (!end_check)
            {
                char_check = t_string[special_index];
                t_string = t_string.Remove(special_index, 1);
                if (char_check == '/')
                {
                    end_check = true;
                }
                else if (char.IsNumber(char_check))
                {
                    t_index_string = t_index_string + char_check.ToString();
                }
                else if (char_check == 'r')
                {
                    current_speaking = JasperOrArea.Jasper;
                    dialogue_box.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
                }
                else if (char_check == 'y')
                {
                    current_speaking = JasperOrArea.Aria;
                    dialogue_box.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 0, 255);
                }
            }
            if (t_index_string != "")
            {
                t_index = int.Parse(t_index_string);
            }
        }

        dialogue_box.GetComponent<TextMeshProUGUI>().text = t_string;
        return t_index;
    }

    public void OptionPressed(int opt)
    {
        GenerateAdvise(opt);
    }
}
