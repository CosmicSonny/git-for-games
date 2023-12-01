using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Manger;

public class KeyPickup : MonoBehaviour
{
    public Manger.DoorKeyColours keyColour;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch (keyColour)
        {
            case Manger.DoorKeyColours.Red:
                sr.color = Color.red;
                break;
            case Manger.DoorKeyColours.Blue:
                sr.color = Color.blue;
                break;
            case Manger.DoorKeyColours.Yellow:
                sr.color = Color.yellow;
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manger.KeyPickup(keyColour);
            Destroy(gameObject);
            print("picked up " + keyColour);
            print("yellow = " + yellowKey + "Red = " + redKey + "blue = " + blueKey);
        }                                     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
