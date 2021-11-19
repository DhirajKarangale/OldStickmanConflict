using UnityEngine;
using EasyJoystick;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrence")]
    public Joystick moveJoystick;
    [SerializeField] Animator animator;
    public Rigidbody2D rigidBody;
    [SerializeField] CapsuleCollider2D legCollider;
    [SerializeField] ParticleSystem dustEffectLeft, dustEffectRight;

    [Header("Attributes")]
    [SerializeField] float jumpForce;
    [SerializeField] float downForce;
    [SerializeField] float moveSpeed;
    public static int weaponRotation = 1;

    private void Update()
    {
        if ((moveJoystick.Horizontal() == 0) && (moveJoystick.Vertical() == 0))
        {
            animator.Play("Idel");
        }
        else
        {
            rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -15, 12), Mathf.Clamp(rigidBody.velocity.y, -10, 12));


            // Move
            if (moveJoystick.Horizontal() > 0.4F) // Move Front
            {
                DustEffect();
                animator.Play("Walk");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                weaponRotation = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveJoystick.Horizontal() < -0.4F) // Move Back
            {
                DustEffect();
                animator.Play("Walk Back");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                weaponRotation = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }


            // Jump & Crounch
            if ((moveJoystick.Vertical() > 0.6f) && (Physics2D.IsTouchingLayers(legCollider, 64))) // Jump 
            {                                                                             // 64 is Layer Mask Of Ground     
                animator.speed = 0.3f;
                DustEffect();
                rigidBody.AddForce(jumpForce * Vector2.up * moveJoystick.Vertical(), ForceMode2D.Impulse);
            }
            else if (moveJoystick.Vertical() < -0.95f) // Crounch
            {
                rigidBody.AddForce(Vector2.down * downForce * Time.deltaTime);
            }

            if (moveJoystick.Vertical() < 0.3f) animator.speed = 1;
        }
    }

    private void DustEffect()
    {
        dustEffectLeft.Play();
        dustEffectRight.Play();
    }
}