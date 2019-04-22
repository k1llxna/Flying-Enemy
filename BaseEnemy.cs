using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    private bool movingRight = true;

    public int hp;
    public int pointValue;
    public int atkDmg;
    public int speed;
    public int noSpeed;

    public Transform groundDetection;
    public GameObject deathEffect;

	void Start () {
        if (hp <= 0)
        {
            hp = 1;
        }

		if (atkDmg <= 0)
        {
            atkDmg = 1;
        }

        if (speed < 0)
        {
            speed = 1;
        }

    }

    void Update () {
        Patrol();
	}

    public void Patrol()
    {
        // wall flip checks
        transform.Translate(-Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 10f);

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    // enemy contact flip checks
    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (c.gameObject.CompareTag("Projectile"))
        {
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
