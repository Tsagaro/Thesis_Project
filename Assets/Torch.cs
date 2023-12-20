using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Torch : MonoBehaviour
{
    public TextMeshProUGUI text;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    private Animator torchAnimator;
    private Collider2D torchCollider;
    
    //private bool hasCollided = false;

    void Start()
    {
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
            TorchCounter.instance.AddTorch(); //adds to the total torches

            //hasCollided = true; // 

            //ChangeSprite(); // no need to change the sprite

            torchAnimator.SetBool("PlayAnimation", true); //sets the bool to true so the torch can light up
            torchCollider.enabled = false;
            Debug.Log("Collision detected"); // Debugging log


        }
    }
}