using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControls : MonoBehaviour
{
   
    Rigidbody2D rb;
    private Vector2 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("vertical"));
        print("The input vector is" + inputVector);
    }


}
