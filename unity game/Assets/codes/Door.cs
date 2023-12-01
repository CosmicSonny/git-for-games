using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Manger;

public class Door : MonoBehaviour
{
    public Manger.DoorKeyColours keyColour;
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.Find("door").gameObject;
        SpriteRenderer sr = door.GetComponent<SpriteRenderer>();
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
        if(collision.tag == "Player" && door!= null)
        {
            switch (keyColour)
            {
                case Manger.DoorKeyColours.Red:
                    if (Manger.redKey) Destroy(door);
                    break;
                case Manger.DoorKeyColours.Blue:
                    if (Manger.blueKey) Destroy(door);
                    break;
                case Manger.DoorKeyColours.Yellow:
                    if (Manger.yellowKey) Destroy(door);
                    break;
            }
        }
    }
}
