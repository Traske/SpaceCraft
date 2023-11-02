using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Controller : MonoBehaviour
{
    Vector2 velocity;
    GameObject finishLine;


    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0, 0);
        finishLine = GameObject.Find("FinishLine");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        KeyboardControl();

        Debug.Log(Vector2.Distance(transform.position, finishLine.transform.position));
    }

    void KeyboardControl()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            velocity.y = -3;
        }

        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            velocity.y = 3;
        }

        else
        {
            velocity.y = 0;
        }


        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            velocity.x = -3;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            velocity.x = 3;
        }

        else
        {
            velocity.x = 0;
        }
    }
}
