using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlatformController>().ladderd = true;
        }
  }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlatformController>().ladderd = false;
        }
        
    }
}
