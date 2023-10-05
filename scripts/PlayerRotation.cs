using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation speed of the player.")]
    public float rotationSpeed = 5f;

    [Tooltip("Limited rotation range in degrees.")]
    [Range(0f, 360f)] // Define the allowable range for rotationRange
    public float rotationRange = 60f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    private Transform targetEnemy;

    void Update()
    {
        FindNearestEnemy();

        if (targetEnemy != null)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;

            // Calculate the angle between the player's forward direction and the direction to the enemy
            float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

            if (angleToEnemy <= rotationRange * 0.5f)
            {
                // Rotate towards the enemy
                Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Rotate the shooting point along with the player
                shootingPoint.rotation = targetRotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance && distance <= rotationRange)
            {
                nearestDistance = distance;
                targetEnemy = enemy.transform;
            }
        }
    }

    void Shoot()
    {
        // Instantiate a bullet from the shooting point
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
