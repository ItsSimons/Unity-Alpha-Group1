using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadThingsEvents : MonoBehaviour
{
    [SerializeField] public GameData data;
    [SerializeField] public CurrencyManager economy;
    
    [SerializeField] public UIManager hud;
    [SerializeField] public GameObject window;
    
    public void BirdsOfParadise(bool start)
    {
        if (start)
        {
            hud.setEventText("Oh no! The Birds of paradise are invading heaven!");
            hud.setEventbutton("Crap!");
            window.SetActive(true);

            //Decrease efficency by 75%
            economy.passiveIncome = (economy.passiveIncome / 100) * 25;
            economy.passiveCost = economy.passiveCost * 4;
        }
        else
        {
            hud.setEventText("The birds of heaven flew away... there is poop everywhere now tho...");
            hud.setEventbutton("Cleanup time");
            window.SetActive(true);
            
            //Efficency back to full
            economy.passiveIncome = economy.passiveIncome * 4;
            economy.passiveCost = economy.passiveCost / 4;
        }
    }

    public void BatsOutOfHell(bool start)
    {
        if (start)
        {
            hud.setEventText("Hell is crawling with bats! Ew!");
            hud.setEventbutton("Shoot!");
            window.SetActive(true);

            //Decrease efficency by 75%
            economy.passiveIncome = (economy.passiveIncome / 100) * 25;
            economy.passiveCost = economy.passiveCost * 4;
        }
        else
        {
            hud.setEventText("The bats are gone, time to clean the mess up...");
            hud.setEventbutton("Unfortunate");
            window.SetActive(true);
            
            //Efficency back to full
            economy.passiveIncome = economy.passiveIncome * 4;
            economy.passiveCost = economy.passiveCost / 4;
        }
    }

    public void HeavenGetsTheBlues(bool start)
    {
        if (start)
        {
            hud.setEventText("Where are the roads? workers are lost!");
            hud.setEventbutton("OOF");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("Workers found their way again, that's positive, i guess?");
            hud.setEventbutton("ROAADS");
            window.SetActive(true);
        }
    }

    public void DiscoInferno(bool start)
    {
        if (start)
        {
            hud.setEventText("Hell is vibin! daaance to the muuusic! Wait, hold on... " +
                             "don't dance on the buildings!! stop destroying them!!");
            hud.setEventbutton("Boogie-woogie!");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("Dancing is no more, but at least no more buildings are being taken down...");
            hud.setEventbutton(":(");
            window.SetActive(true);
        }
    }

    public void ParadiseDices(bool start)
    {
        if (start)
        {
            hud.setEventText("Couple of hands are trowing dices over heaven! watch out!");
            hud.setEventbutton("Diceees!");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("Hands are gone, no more dices, time to rebuilt what has been destroyed...");
            hud.setEventbutton("Gotha");
            window.SetActive(true);
        }
    }

    public void HellFreezesOver(bool start)
    {
        if (start)
        {
            hud.setEventText("Its so cold... wait, did heaven just turned into antarctica?");
            hud.setEventbutton("Brrrrr!");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("Hell is back to normal temperature! No more magmatic ice!");
            hud.setEventbutton("Phew!");
            window.SetActive(true);
        }
    }

    public void HeavenNose(bool start)
    {
        if (start)
        {
            hud.setEventText("A Giant nose is roaming Hell! Oh my!");
            hud.setEventbutton("*Sniff Sniff*");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("The nose is gone! Phew!");
            hud.setEventbutton("Pog");
            window.SetActive(true);
        }
    }

    public void HellHandbasket(bool start)
    {
        if (start)
        {
            hud.setEventText("A handbasked is roaming heaven, why is it there!");
            hud.setEventbutton("Shoot!");
            window.SetActive(true);
        }
        else
        {
            hud.setEventText("Handbasked is gone, No more trouble!");
            hud.setEventbutton("Phew!");
            window.SetActive(true);
        }
    }
}
