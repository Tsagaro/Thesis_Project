using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] characterSprites;
    private const int CharacterSpriteUpIndex = 0;
    private const int CharacterSpriteRightIndex = 1;
    private const int CharacterSpriteDownIndex = 2;
    private const int CharacterSpriteLeftIndex = 3;
    int currentSpriteIndex = CharacterSpriteDownIndex; //used to keep latest sprite index
    public PlayerShooting playerShooting;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Initialize the reference to the shooting script
        playerShooting = GetComponent<PlayerShooting>();
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveCharacterSprite();
        ChangeCharacterSprite();
        // Pass the input direction to the shooting script
        playerShooting.SetShootingDirection(rb.velocity.normalized);
    }

    private void MoveCharacterSprite()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        float verticalSpeed = Input.GetAxis("Vertical") * moveSpeed;

        // Set the velocity of the Rigidbody2D
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    private void ChangeCharacterSprite()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) { currentSpriteIndex = CharacterSpriteLeftIndex; }
        else if (Input.GetKey(KeyCode.RightArrow)) { currentSpriteIndex = CharacterSpriteRightIndex; }
        else if (Input.GetKey(KeyCode.UpArrow)) { currentSpriteIndex = CharacterSpriteUpIndex; }
        else if (Input.GetKey(KeyCode.DownArrow)) { currentSpriteIndex = CharacterSpriteDownIndex; }

        spriteRenderer.sprite = characterSprites[currentSpriteIndex];
    }
}
