using UnityEngine;
using EasyJoystick;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Joystick moveJoystick;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] CapsuleCollider2D legCollider;
    [SerializeField] SpriteRenderer headRenderer;
    [SerializeField] ParticleSystem dustEffectLeft, dustEffectRight;

    [Header("Attributes")]
    [SerializeField] float jumpForce;
    [SerializeField] float downForce;
    [SerializeField] float moveSpeed;
    public static Vector3 weaponRotation = new Vector3(0, 0, -90);
   // public static Vector3 weaponThrowPosition = new Vector3(1, 0.25f, 0);

    private void Update()
    {
        if ((moveJoystick.Horizontal() == 0) && (moveJoystick.Vertical() == 0))
        {
            animator.Play("Idel");
        }
        else
        {
            Vector2 moveVelocity = rigidBody.velocity;

            // Move
            if (moveJoystick.Horizontal() > 0.4F) // Move Front
            {
                animator.Play("Walk");
                DustEffect();
                //rigidBody.AddForce(Vector2.right * playerSpeed * moveJoystick.Horizontal());
                moveVelocity = new Vector2(moveSpeed * moveJoystick.Horizontal(), rigidBody.velocity.y);
                weaponRotation = new Vector3(0, 0, -90);
              //  weaponThrowPosition = new Vector3(1, 0.25f, 0);
                headRenderer.flipX = false;
            }
            else if (moveJoystick.Horizontal() < -0.4F) // Move Back
            {
                animator.Play("Walk Back");
                DustEffect();
                //rigidBody.AddForce(Vector2.right * playerSpeed * moveJoystick.Horizontal());
                moveVelocity = new Vector2(moveSpeed * moveJoystick.Horizontal(), rigidBody.velocity.y);
                weaponRotation = new Vector3(0, 0, 90);
               // weaponThrowPosition = new Vector3(-1, 0.25f, 0);
                headRenderer.flipX = true;
            }



            // Jump & Crounch
            if ((moveJoystick.Vertical() > 0.6f) && (Physics2D.IsTouchingLayers(legCollider, 64))) // Jump 
            {                                                                             // 64 is Layer Mask Of Ground
                DustEffect();
                //rigidBody.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                moveVelocity = new Vector2(rigidBody.velocity.x, moveJoystick.Vertical() * jumpForce);
            }
            else if (moveJoystick.Vertical() < -0.95f) // Crounch
            {
                rigidBody.AddForce(Vector2.down * downForce * Time.deltaTime);
            }

            rigidBody.velocity = moveVelocity;
        }
    }

    private void DustEffect()
    {
        dustEffectLeft.Play();
        dustEffectRight.Play();
    }
}