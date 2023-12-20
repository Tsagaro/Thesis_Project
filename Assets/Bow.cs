using UnityEngine;

public class Bow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.gameObject.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                playerShooting.EnableShooting(); // Call a method to enable shooting
            }

            // Optionally, you can disable the bow object or perform other actions.
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
