using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manger : MonoBehaviour
{
    public static Manger instance;
    private static int coins;
    public enum DoorKeyColours { Red, Blue, Yellow};
    public static bool redKey, blueKey, yellowKey;
    public static Vector3 LastCheckPoint;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AddCoins(int coinValue)
    {
        coins += coinValue;
        print("coins: " + coins);
    }

    public static void KeyPickup(DoorKeyColours keyColour)
    {
        switch (keyColour)
        {
            case DoorKeyColours.Red:
                redKey = true;
                break;
            case DoorKeyColours.Blue:
                blueKey = true;
                break;
            case DoorKeyColours.Yellow:
                yellowKey = true;
                break;
        }
    }

    public static void UpdateCheckPoints(GameObject flag)
    {
        LastCheckPoint = flag.transform.position;
        checkpoint[] allCheckPoints = FindObjectsOfType<checkpoint>();
        foreach (checkpoint cp in allCheckPoints)
        {
            if(cp != flag.GetComponent<checkpoint>())
            {
                cp.LowerFlag();
            }
        }
    }       
}
