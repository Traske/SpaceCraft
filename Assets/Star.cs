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
}
