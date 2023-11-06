using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f; // Hastighet av asteroidens r�relse
    private Transform targetStar; // Stj�rna som asteroiden r�r sig mot
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindRandomStar(); // Hitta en slumpm�ssig stj�rna att r�ra sig mot n�r asteroiden startar
    }

    void Update()
    {
        MoveTowardsStar();
    }

    void MoveTowardsStar()
    {
        if (targetStar != null)
        {
            Vector2 targetDirection = (targetStar.position - transform.position).normalized;
            rb.velocity = targetDirection * speed;

            // Om asteroiden har n�tt stj�rnan, hitta en ny slumpm�ssig stj�rna att r�ra sig mot
            if (Vector2.Distance(transform.position, targetStar.position) < 0.1f)
            {
                FindRandomStar();
            }
        }
    }

    void FindRandomStar()
    {
        // Hitta en slumpm�ssig stj�rna i scenen och s�tt den som m�l f�r asteroiden
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        if (stars.Length > 0)
        {
            int randomIndex = Random.Range(0, stars.Length);
            targetStar = stars[randomIndex].transform;
        }
    }
}
