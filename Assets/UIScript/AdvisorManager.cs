using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdvisorManager : MonoBehaviour
{
    [SerializeField] public string[] dialogue_arr;
    [SerializeField] private bool dialogue_finished = true;
    [SerializeField] private List<int> dialogue_list = new List<int>();
    [SerializeField] public TextMeshProUGUI dialogue_box;
    [SerializeField] public float time_between_dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue_box.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("["))
        //{
        //    // --USE THIS SECTION TO CALL FROM ANY SCRIPT--
        //    GenerateAdvise(0);
        //    // ---GenerateAdvise(NUMBER INDEX OF CHOICE)---
        //    // --------------------------------------------
        //}
        //else if (Input.GetKeyDown("]"))
        //{
        //    // --USE THIS SECTION TO CALL FROM ANY SCRIPT--
        //    GenerateAdvise(5);
        //    // ---GenerateAdvise(NUMBER INDEX OF CHOICE)---
        //    // --------------------------------------------
        //}
        //HandleAdvisors();
    }

    public void GenerateAdvise(int advise_index)
    {
        // Public function for other scripts to call to
        dialogue_list.Add(advise_index);
    }

    public void HandleAdvisors()
    {
        // Check if dialogue needs to be displayed
        if (dialogue_finished && dialogue_list.Count > 0)
        {
            StartCoroutine(Advise());
        }
    }

    // Please dont use coroutine in update function, it will cause the game entire game to wait for the coroutine to finish before the next tick
    private IEnumerator Advise()
    {
        // Set talking to true so no repeated talking
        dialogue_finished = false;
        // Change dialogue text
        int next_dialogue_index = CheckSpecialDialogue();
        //Start talking animation
        yield return new WaitForSeconds(time_between_dialogue);
        // If there is continuous dialogue
        if (next_dialogue_index >= 0)
        {
            // Then produce dialogue again
            dialogue_list[0] = next_dialogue_index;
            dialogue_finished = true;
        }
        else
        {
            // Otherwise remove dialogue after timer
            dialogue_list.RemoveAt(0);
            dialogue_box.text = "";
            yield return new WaitForSeconds(1);
            dialogue_finished = true;
        }
    }

    private int CheckSpecialDialogue()
    {
        // This finds any sequencial dialogue and removes the numbers and slashes from the string
        string t_string = dialogue_arr[dialogue_list[0]];
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
                    dialogue_box.color = new Color32(255, 0, 0, 255);
                }
                else if (char_check == 'y')
                {
                    dialogue_box.color = new Color32(255, 255, 0, 255);
                }
            }
            if (t_index_string != "")
            {
                t_index = int.Parse(t_index_string);
            }
        }

        dialogue_box.text = t_string;
        return t_index;
    }

    // Please use char.IsNumber(char) instead
    // Also you forgot to put break for switch cases
    private bool IsNumber(char character_check)
    {
        switch (character_check)
        {
            case '1':
                return true;
            case '2':
                return true;
            case '3':
                return true;
            case '4':
                return true;
            case '5':
                return true;
            case '6':
                return true;
            case '7':
                return true;
            case '8':
                return true;
            case '9':
                return true;
            case '0':
                return true;
        }
        return false;
    }
}
