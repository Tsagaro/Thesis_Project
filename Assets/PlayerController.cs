using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float translateSpeed = 5f;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] characterSprites;
    private const int CharacterSpriteUpIndex = 0;
    private const int CharacterSpriteRightIndex = 1;
    private const int CharacterSpriteDownIndex = 2;
    private const int CharacterSpriteLeftIndex = 3;
    int currentSpriteIndex = CharacterSpriteDownIndex; //used to keep latest sprite index
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();     
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacterSprite();
        changeCharacterSprite();
    }

    


    private void moveCharacterSprite(){
        float verticalSpeed = Input.GetAxis("Vertical") * Time.deltaTime * translateSpeed;
        float horizontalSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * translateSpeed;
        transform.Translate(horizontalSpeed, verticalSpeed, 0);
    }
    private void changeCharacterSprite(){
        if(Input.GetKey(KeyCode.LeftArrow)){ currentSpriteIndex = CharacterSpriteLeftIndex; }
        else if(Input.GetKey(KeyCode.RightArrow)){ currentSpriteIndex = CharacterSpriteRightIndex; } 
        else if(Input.GetKey(KeyCode.UpArrow)){ currentSpriteIndex = CharacterSpriteUpIndex; }
        else if(Input.GetKey(KeyCode.DownArrow)){ currentSpriteIndex = CharacterSpriteDownIndex; }
        spriteRenderer.sprite = characterSprites[currentSpriteIndex];
    }
}
