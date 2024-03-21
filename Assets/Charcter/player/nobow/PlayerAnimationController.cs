using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    // Boolean parameter names in the Animator Controller
    private string moveUpParameter = "MovingUp";
    private string moveDownParameter = "MovingDown";
    private string moveLeftParameter = "MovingLeft";
    private string moveRightParameter = "MovingRight";
    private string bowParameter = "HasBow";
    private string attackParameter = "AttackBow";

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check arrow key inputs
        bool moveUp = Input.GetKey(KeyCode.UpArrow);
        bool moveDown = Input.GetKey(KeyCode.DownArrow);
        bool moveLeft = Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.RightArrow);
        bool attack = Input.GetKey(KeyCode.Space);

        // Update the Animator parameters based on arrow key inputs
        UpdateMovementAnimationParameters(moveUp, moveDown, moveLeft, moveRight, attack);
    }

    void UpdateMovementAnimationParameters(bool moveUp, bool moveDown, bool moveLeft, bool moveRight, bool attack)
    {
        // Update the Animator parameters based on arrow key inputs
        animator.SetBool(moveUpParameter, moveUp);
        animator.SetBool(moveDownParameter, moveDown);
        animator.SetBool(moveLeftParameter, moveLeft);
        animator.SetBool(moveRightParameter, moveRight);
        animator.SetBool(attackParameter, attack);
        StartCoroutine(ResetAttackCoroutine());
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bow"))
        {
            animator.SetBool(bowParameter, true);
        }
    }
    IEnumerator ResetAttackCoroutine()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool(attackParameter, false);
    }
}
