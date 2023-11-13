using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    Vector2 velocity;
    public Transform[] asteroidTransforms;
    public Transform[] starTransforms;
    public float warningDistance = 2f;
    public float maxLandingSpeed = 3f; 
    public TextMeshProUGUI warningTextAsteroid;
    public TextMeshProUGUI warningTextLanding;
    private bool hasCollided = false; // Flag to check if the spaceship has collided with the ground

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
                    ShowWarningTextAsteroid("ASTEROID WARNING!");
                }
                else if (nearestObject.CompareTag("Star"))
                {
                    CollectStar(nearestObject);
                }
            }
            else
            {
                HideWarningTextAsteroid();
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
            if (distanceToObject < 1.75f)
            {
                nearestObject = asteroidTransform;
                nearestDistance = distanceToObject;
                ShowWarningTextAsteroid("ASTEROID WARNING!");
            }
            else if (distanceToObject > 2f)
            {
                HideWarningTextAsteroid();
            }
            if (distanceToObject < 1f)
            {
                RestartGame();
            }
        }

        foreach (Transform starTransform in stars)
        {
            float distanceToObject = Vector2.Distance(transform.position, starTransform.position);
            if (distanceToObject < 1f)
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

    // velocity based on input
    velocity = new Vector2(horizontalInput, verticalInput).normalized * 3;

    // Gravity 
    velocity.y -= 40f * Time.deltaTime;

    // Move the spaceship using Rigidbody2D
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.velocity = velocity;

    // Get the main camera
    Camera mainCamera = Camera.main;

    // Restricts the spaceships position within the cameras bounds
    Vector3 clampedPosition = mainCamera.WorldToViewportPoint(transform.position);
    clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0.04f, 0.96f); 
    clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0.14f, 0.95f); 

    transform.position = mainCamera.ViewportToWorldPoint(clampedPosition);
}


    void ShowWarningTextAsteroid(string message)
    {
        warningTextAsteroid.text = message;
    }

    void HideWarningTextAsteroid()
    {
        warningTextAsteroid.text = "";
    }

    void ShowWarningTextLanding(string message)
    {
        warningTextLanding.text = message;
    }

    void HideWarningTextLanding()
    {
        warningTextLanding.text = "";
    }

    void CollectStar(Transform starTransform)
    {
        starTransform.GetComponent<Renderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            // Check the landing speed
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float landingSpeed = rb.velocity.magnitude;

            if (landingSpeed > maxLandingSpeed)
            {
                hasCollided = true; // Set the collision flag to true
                ShowWarningTextLanding("HARD LANDING!");
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
