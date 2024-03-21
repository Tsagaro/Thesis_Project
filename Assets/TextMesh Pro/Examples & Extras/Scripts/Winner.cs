using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    public GameObject winAnime;
    public GameObject deathOverlay;


    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            deathOverlay.GetComponent<Animator>().SetBool("Death", true);
            winAnime.GetComponent<Animator>().SetBool("Win", true);
            StartCoroutine(ExitGameCoroutine());

        }

        IEnumerator ExitGameCoroutine()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Scenes/Menu");
        }


    }

}
