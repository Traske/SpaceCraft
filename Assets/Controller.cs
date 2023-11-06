using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Controller : MonoBehaviour
{
    Vector2 velocity;
    public Transform asteroidTransform;
    public float warningDistance = 5f;
    public TextMeshProUGUI warningText;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero; // Set initial velocity to zero
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpaceship();

        float distanceToAsteroid = Vector2.Distance(transform.position, asteroidTransform.position);

        if (distanceToAsteroid < warningDistance)
        {
            ShowWarningText();
        }
        else
        {
            HideWarningText();
        }
    }

    void MoveSpaceship()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set velocity based on input
        velocity = new Vector2(horizontalInput, verticalInput).normalized * 3;

        // Apply gravity (example: a downward force with magnitude 2 units per second squared)
        velocity.y -= 40f * Time.deltaTime;


        // Move the spaceship using Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }


    void ShowWarningText()
    {
        warningText.text = "ASTEROID WARNING!"; // Set the text of the UI Text element
    }

    void HideWarningText()
    {
        warningText.text = ""; // Clear the text to hide the warning
    }
}
