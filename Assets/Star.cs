using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Renderer[] starRenderers; // Array to hold the renderers of all stars

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with the tag "Star" and get their renderers
        GameObject[] starObjects = GameObject.FindGameObjectsWithTag("Star");
        starRenderers = new Renderer[starObjects.Length];

        for (int i = 0; i < starObjects.Length; i++)
        {
            starRenderers[i] = starObjects[i].GetComponent<Renderer>();
        }
    }

    // OnTriggerEnter2D is called when another collider enters this object's collider
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
        if (other.CompareTag("Spaceship")) // Assuming the spaceship has the tag "Spaceship"
        {
            Debug.Log("Spaceship entered star trigger zone");
            // Iterate through all star renderers and make each star invisible by disabling its renderer component
            foreach (Renderer starRenderer in starRenderers)
            {
                starRenderer.enabled = false;
            }

            // You can add other actions when the spaceship collides with the stars here
        }
    }
}
