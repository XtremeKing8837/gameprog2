using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Enemy movement speed
    public TMP_Text gameOverText; // Reference to the "Game Over" Text component

    private Transform player; // Reference to the player's Transform
    private bool gameIsOver = false; // Flag to track game over state

    void Start()
    {
        // Find the player GameObject by tag ("Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!gameIsOver)
        {
            // Move the enemy towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // Handle collisions with the player and bullets
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Enemy has collided with the player or a bullet, trigger game over.
            
            // Destroy both the enemy and the bullet
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enemy has collided with the player, trigger game over.
            ShowGameOver();
        }
    }
    // Display "Game Over" text and handle game over logic
    void ShowGameOver()
    {
        gameIsOver = true; // Set the game over flag

        // Enable the "Game Over" text
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        // Add other game-over logic here (e.g., pausing the game, showing a restart button, etc.).
    }
}