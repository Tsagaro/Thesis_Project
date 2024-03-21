using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Torch : MonoBehaviour
{
    public TextMeshProUGUI text;
    private GameObject DoorPrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    private Animator torchAnimator;
    private Collider2D torchCollider;

    void Start()
    {
        DoorPrefab =  GameObject.Find("Door");
        
        torchAnimator = GetComponent<Animator>(); // Get the Animator component
        torchAnimator.SetBool("PlayAnimation", false);
        torchCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int total = TorchCounter.instance.AddTorch(); //adds to the total torches

            torchAnimator.SetBool("PlayAnimation", true); //sets the bool to true so the torch can light up
            torchCollider.enabled = false;
            Debug.Log("Collision detected "+ total); // Debugging log
            if (total >= 1)
            {
                SceneManager.LoadScene("Scenes/Cutscene");
            }
            else if (total >= 2)
            {
                Destroy(DoorPrefab);
            }
        }

    }
    

}
        
        
    