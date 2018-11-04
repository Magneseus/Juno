using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    private float boid_radius = 1;
    public float weight = 1;
    List<Boid> neiboids;

    Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        neiboids = new List<Boid>();
        rb = GetComponent<Rigidbody2D>();
        CircleCollider2D[] circles = GetComponents<CircleCollider2D>();
        foreach(CircleCollider2D c in circles)
        {
            if(c.isTrigger)
            {
                boid_radius = c.radius;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 crowdDirection = new Vector2(0, 0);
        Vector2 center = new Vector2(0, 0);
        Vector2 separation = new Vector2(0, 0);

        Rigidbody2D b_rb;
        float dist;
        Vector2 away;
        foreach (Boid b in neiboids)
        {
            b_rb = b.GetComponent<Rigidbody2D>();

            // converge
            center += b_rb.position;

            // separate
            dist = Vector2.Distance(b_rb.position, rb.position);
            if (dist < boid_radius / 2)
            {
                away = (b_rb.position - rb.position);
                separation -= away.normalized * ((boid_radius / 2) - dist);
                
            }

            // mimic velocity
            crowdDirection += b_rb.velocity / neiboids.Count * weight;
        }
        
        center = (center / neiboids.Count) - rb.position;
        Vector2 add = center / 100 + crowdDirection / 8 + separation / 25;

        if((rb.velocity + add).sqrMagnitude < 4)
        {
            rb.velocity += add;
        }
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag.Equals("Boid") && !c.isTrigger)
        {
            neiboids.Add(c.GetComponent<Boid>());
            c.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag.Equals("Boid") && !c.isTrigger)
        {
            neiboids.Remove(c.GetComponent<Boid>());
            c.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
