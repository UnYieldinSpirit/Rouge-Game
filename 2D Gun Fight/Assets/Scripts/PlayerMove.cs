using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigbod;
    public float moveSpeed = 1;

    public const string RIGHT = "right";
    public const string LEFT = "left";

    string buttonPressed;
    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            buttonPressed = RIGHT;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            buttonPressed = LEFT;
        }
        else
        {
            buttonPressed = null;
        }
    }

    private void FixedUpdate()
    {
        if(buttonPressed == RIGHT)
        {
            rigbod.velocity = new Vector2(moveSpeed, 0);
        }
        else if(buttonPressed == LEFT)
        {
            rigbod.velocity = new Vector2(-moveSpeed, 0); 
        }
        else
        {

        }
    }
}
