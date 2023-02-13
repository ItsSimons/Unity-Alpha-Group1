using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HudUpDate : MonoBehaviour
{
    public float year = 0;
    [SerializeField]public float yearLength = 0.1f;
    
    public float creditBalance = 0;
    [SerializeField] bool creditPos;
    public float numberOfSouls = 1;

    [SerializeField] TMP_Text yearText;
    [SerializeField] TMP_Text creditText;
    [SerializeField] TMP_Text soulText;

    [SerializeField] public GameData gameData;
    [SerializeField] public int currency;

    // Update is called once per frame
    void Update()
    {
        updateYearHud();
        creditUpdateHud();
        updateSoulsHud();

        currency = (int)gameData.currency;
        if (currency >= 0)
        {
            creditPos = true;
        }
        else
        {
            creditPos = false;
        }
    }

    void updateYearHud()
    {
        yearText.text = "Year " + year;
        year += yearLength * Time.deltaTime;
    }

    void updateSoulsHud()
    {
        soulText.text = "Souls " + gameData.souls_total;
        numberOfSouls += yearLength * Time.deltaTime;
    }

    void creditUpdateHud()
    {
        if (Mathf.Abs(currency) >= 10000000)
        {
            creditText.text = "Credits " + currency / 1000000 + "M";
        }
        else if (Mathf.Abs(currency) >= 10000)
        {
            creditText.text = "Credits " + currency / 1000 + "K";
        }
        else
        {
            creditText.text = "Credits " + currency;
        }
        if (!creditPos)
        {
            creditText.color = new Color(1, 0, 0, 1);
            creditBalance -= numberOfSouls * Time.deltaTime;
        }
        else
        {
            creditText.color = Color.green;
            creditBalance += numberOfSouls * Time.deltaTime;
        }
    }
}
