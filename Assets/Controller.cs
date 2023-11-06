using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    Vector2 velocity;
    public Transform[] asteroidTransforms;
    public Transform[] starTransforms;
    public float warningDistance = 1.5f;
    public float maxLandingSpeed = 3f; // Maximum landing speed allowed
    public TextMeshProUGUI warningText;
    private bool hasCollided = false; // Flag to track if the spaceship has already collided with the ground

    void Start()
    {
        velocity = Vector2.zero;
    }

    void Update()
    {
        if (!hasCollided) // Check if the spaceship has not collided with the ground
        {
            MoveSpaceship();

            Transform nearestObject = CheckNearbyObjects(asteroidTransforms, starTransforms);

            if (nearestObject != null)
            {
                if (nearestObject.CompareTag("Asteroid"))
                {
                    ShowWarningText("ASTEROID WARNING!");
                    RestartGame();
                }
                else if (nearestObject.CompareTag("Star"))
                {
                    CollectStar(nearestObject);
                }
            }
            else
            {
                HideWarningText();
            }
        }
    }

    Transform CheckNearbyObjects(Transform[] asteroids, Transform[] stars)
    {
        Transform nearestObject = null;
        float nearestDistance = warningDistance;

        foreach (Transform asteroidTransform in asteroids)
        {
            float distanceToObject = Vector2.Distance(transform.position, asteroidTransform.position);
            if (distanceToObject < nearestDistance)
            {
                nearestObject = asteroidTransform;
                nearestDistance = distanceToObject;
            }
        }

        foreach (Transform starTransform in stars)
        {
            float distanceToObject = Vector2.Distance(transform.position, starTransform.position);
            if (distanceToObject < nearestDistance)
            {
                nearestObject = starTransform;
                nearestDistance = distanceToObject;
            }
        }

        return nearestObject;
    }

    void MoveSpaceship()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set velocity based on input
        velocity = new Vector2(horizontalInput, verticalInput).normalized * 3;

        // Apply gravity 
        velocity.y -= 40f * Time.deltaTime;

        // Move the spaceship using Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        // Check if the spaceship is landing with excessive speed
        if (velocity.y < -maxLandingSpeed)
        {
            hasCollided = true; // Set the collision flag to true
            ShowWarningText("HARD LANDING!");
            RestartGame();
        }
    }

    void ShowWarningText(string message)
    {
        warningText.text = message;
    }

    void HideWarningText()
    {
        warningText.text = "";
    }

    void CollectStar(Transform starTransform)
    {
        starTransform.GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            hasCollided = true; // Set the collision flag to true
            ShowWarningText("HARD LANDING!");
            RestartGame();
        }
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
