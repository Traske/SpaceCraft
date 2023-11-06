using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f; // Hastighet av asteroidens rörelse
    private Transform targetStar; // Stjärna som asteroiden rör sig mot
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindRandomStar(); // Hitta en slumpmässig stjärna att röra sig mot när asteroiden startar
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

            // Om asteroiden har nått stjärnan, hitta en ny slumpmässig stjärna att röra sig mot
            if (Vector2.Distance(transform.position, targetStar.position) < 0.1f)
            {
                FindRandomStar();
            }
        }
    }

    void FindRandomStar()
    {
        // Hitta en slumpmässig stjärna i scenen och sätt den som mål för asteroiden
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        if (stars.Length > 0)
        {
            int randomIndex = Random.Range(0, stars.Length);
            targetStar = stars[randomIndex].transform;
        }
    }
}
