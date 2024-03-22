using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fire_trap : MonoBehaviour
{

    public GameObject deathAnime;
    public GameObject deathOverlay;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            deathOverlay.GetComponent<Animator>().SetBool("Death", true);
            deathAnime.GetComponent<Animator>().SetBool("Death", true);
            StartCoroutine(ExitGameCoroutine());

        }
        else 
        {
            return;
        }
    }
    IEnumerator ExitGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        deathOverlay.GetComponent<Animator>().SetBool("Death", false);
        deathAnime.GetComponent<Animator>().SetBool("Death", false);
        SceneManager.LoadScene("Scenes/Menu");
    }
}
