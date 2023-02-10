using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdvisorOptionBox : MonoBehaviour
{

    public GameObject hell_ind;
    public GameObject heaven_ind;
    public TextMeshProUGUI title_text;

    public void SetIndicators(AdvisorManager.HeavenOrHell ind, string str)
    {
        if (ind == AdvisorManager.HeavenOrHell.Heaven)
        {
            heaven_ind.SetActive(true);
            hell_ind.SetActive(false);
        }
        else if (ind == AdvisorManager.HeavenOrHell.Hell)
        {
            heaven_ind.SetActive(false);
            hell_ind.SetActive(true);
        }
        else if (ind == AdvisorManager.HeavenOrHell.Both)
        {
            heaven_ind.SetActive(true);
            hell_ind.SetActive(true);
        }
        else if (ind == AdvisorManager.HeavenOrHell.Neither)
        {
            heaven_ind.SetActive(false);
            hell_ind.SetActive(false);
        }
        title_text.text = str;
    }
}
