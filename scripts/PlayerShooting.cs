using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab.
    public Transform shootPoint;    // The point from which bullets will be shot.
    public Color[] colors;
    public float shootInterval = 1.0f; // Time interval between shots in seconds.
    private int colorIndex = 0; // Index of the current color.
    private float nextShootTime;
    private Renderer playerRenderer;

    private void Start()
    {
        // Get the Renderer component of the player character.
        playerRenderer = GetComponent<Renderer>();
    }


    private void Update()
    {
        // Check if it's time to shoot again.
        if (Time.time >= nextShootTime)
        {
            if (Input.GetButtonDown("Fire1")) // Adjust the input button as needed.
            {
                Shoot(); // Call the Shoot method.
                nextShootTime = Time.time + shootInterval; // Update the next shoot time.
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeColor();
            }
        }
    }

    private void Shoot()
    {
        // Create an instance of the bullet prefab at the shoot point.
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Add force to the bullet to make it move.
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bullet.transform.forward * 2.0f; // Adjust the bullet speed as needed.

        playerRenderer.material.color = colors[colorIndex];
        Renderer bulletRenderer = bullet.GetComponent<Renderer>();
        bulletRenderer.material.color = colors[colorIndex];

        // Destroy the bullet after a certain time (e.g., to prevent memory leaks).
        Destroy(bullet, 5.0f); // Adjust the duration as needed.
    }
    private void ChangeColor()
    {
        // Increment the color index and loop back to the beginning if necessary.
        colorIndex = (colorIndex + 1) % colors.Length;
    }
}
