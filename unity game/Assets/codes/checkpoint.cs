using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = transform.Find("flag").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            flag.GetComponent<SpriteRenderer>().color = Color.red;
            Manger.UpdateCheckPoints(gameObject);

        }
    }

    public void LowerFlag()
    {
        flag.GetComponent<SpriteRenderer>().color = Color.white;
    }


}
