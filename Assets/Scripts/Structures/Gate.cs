using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameData gameData;
    private int soulsPerSec;

    private void Start()
    {
        InvokeRepeating("addSouls", 0, 1);
    }

    public void initGate(GameData _gameData, int _soulsPerSec)
    {
        gameData = _gameData;
        soulsPerSec = _soulsPerSec;
    }

    private void addSouls()
    {
        ZoneManager.ZoneType zone = (ZoneManager.ZoneType)Random.Range(0, 7);

        switch (zone)
        {
            case ZoneManager.ZoneType.Green:
                gameData.souls_heaven_Green += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Yellow:
                gameData.souls_heaven_Yellow += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Orange:
                gameData.souls_heaven_Orange += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Brown:
                gameData.souls_heaven_Brown += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Purple:
                gameData.souls_heaven_Purple += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Red:
                gameData.souls_heaven_Red += soulsPerSec;
                break;

            case ZoneManager.ZoneType.Blue:
                gameData.souls_heaven_Blue += soulsPerSec;
                break;
        }
    }
}
