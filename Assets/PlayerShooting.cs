using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    public Transform shootPoint;
    private GameObject projectile;
    public float projectileSpeed = 5f;
    private Vector3 offset = Vector3.zero;
    private Vector2 lastMovementDirection = Vector2.right; // Default direction is +x axis

    private bool canShoot = false; // stops player from shooting when idle
    private bool freezeShot = false;
    private bool hasBow = false;
     
    void Update()
    {
        // Check for shooting input and if shooting is allowed
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && !freezeShot)
        {
            // Call the method to shoot a projectile with the specified direction
            ShootProjectile();
            
        }
    }

    public void SetShootingDirection(Vector2 direction)
    {

        // Rotate the shootPoint based on the shooting direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Store the last known movement direction
        lastMovementDirection = direction;

        // Update the offset only when the player is moving
        if (direction != Vector2.zero)
        {
            if (hasBow)
            {
                canShoot = true;
            }
                
            // Adjust the offset based on the player's movement
            if (direction.x > 0) // Moving right
            {
                offset = new Vector3(0.38f, 0f, 0f);
            }
            else if (direction.x < 0) // Moving left
            {
                offset = new Vector3(-0.33f, -0.14f, 0f);
            }
            else if (direction.y > 0) // Moving up
            {
                offset = new Vector3(-0.01f, 0.66f, 0f);
            }
            else if (direction.y < 0) // Moving down
            {
                offset = new Vector3(0.01f, -0.64f, 0f);
            }
        }
        else
        {
            if (hasBow)
            {
                canShoot = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (projectile != null && other.tag != "projectile")
        {
            Destroy(projectile);
        }
        
    }

    void ShootProjectile()
    {

        // Check if the projectilePrefab and shootPoint are assigned
        if (projectilePrefab != null && shootPoint != null)
        {
            // Instantiate a new projectile at the shootPoint position with the current rotation
            projectile = Instantiate(projectilePrefab, shootPoint.position + offset, shootPoint.rotation);

            // Get the Rigidbody2D component of the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Check if the Rigidbody2D component is not null
            if (rb != null)
            {
                // Apply force to the projectile in the direction the player is facing
                rb.velocity = lastMovementDirection * projectileSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody2D component not found on the projectilePrefab.");

            }
            freezeShot = true;
            StartCoroutine(DestroyCoroutine());
        }
        else
        {

            Debug.LogError("Projectile prefab or shoot point not assigned in the inspector.");
        }

        
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(projectile);
        projectile = null;
        freezeShot = false;
    }

    //called when bow is picked up
    public void EnableShooting()
    {
        hasBow = true; // Set a flag to enable shooting
    }
}
