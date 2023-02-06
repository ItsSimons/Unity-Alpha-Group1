using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HudUpDate : MonoBehaviour
{
    public float year = 0;
    public float yearLength = 0.1f;

    public float creditBalance = 0;
    [SerializeField] bool creditPos;
    public float numberOfSouls = 1;
    
    [SerializeField] TMP_Text yearText;
    [SerializeField] TMP_Text creditText;
    
    
    // Update is called once per frame
    void Update()
    {
        updateYearHud();
        creditUpdateHud();
    }

    void updateYearHud()
    {
        yearText.text = "Year " + year;
        year += yearLength * Time.deltaTime;
    }

    void creditUpdateHud()
    {
        if (creditPos)
        {
            creditText.color = new Color(1, 0, 0, 1);
            creditText.text = "Credits " + creditBalance;
            creditBalance -= numberOfSouls * Time.deltaTime;
        }
        else
        {
            creditText.color = Color.green;
            creditText.text = "Credits " + creditBalance;
            creditBalance += numberOfSouls * Time.deltaTime;
        }

        
        
    }
}
