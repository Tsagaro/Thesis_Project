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
    public string restartScenePath = "Scenes/Final_Stage";
    //private bool hasCollided = false;

    void Start()
    {
        DoorPrefab =  GameObject.Find("Door");
        
        torchAnimator = GetComponent<Animator>(); // Get the Animator component
        torchAnimator.SetBool("PlayAnimation", false);
        torchCollider = GetComponent<Collider2D>();
    }


    //void ChangeSprite()
    //{
    // spriteRenderer.sprite = newSprite; 
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int total = TorchCounter.instance.AddTorch(); //adds to the total torches

            //hasCollided = true; // 

            //ChangeSprite(); // no need to change the sprite

            torchAnimator.SetBool("PlayAnimation", true); //sets the bool to true so the torch can light up
            torchCollider.enabled = false;
            Debug.Log("Collision detected "+ total); // Debugging log
            if (total >= 2)
            {
                Destroy(DoorPrefab);
            } 
            else if (total >= 4)
            {
                SceneManager.LoadScene(restartScenePath);
            }
        }

    }
    

}
        
        
    