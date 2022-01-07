using UnityEngine;
using EasyJoystick;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refrence")]
    public Joystick moveJoystick;
    public PlayerHealth playerHealth;
    public Rigidbody2D rigidBody;
    [SerializeField] Animator animator;
    [SerializeField] CapsuleCollider2D legCollider;
    [SerializeField] ParticleSystem walkEffect, fallEffect;

    [Header("Attributes")]
    [SerializeField] float jumpForce;
    [SerializeField] float downForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float fallDamage;
    private float velocity = 0;
    public bool isLegUp;

    private void Start()
    {
        if (SaveManager.instance.isDataLoaded)
        {
            transform.position = new Vector3(SaveManager.instance.saveData.playerSpwanPos[0] + 5, SaveManager.instance.saveData.playerSpwanPos[1], 0);
        }
    }

    private void Update()
    {
        if (PlayerHealth.isPlayerDye) return;
        if (transform.position.y < -100) playerHealth.Died();

        if ((moveJoystick.Horizontal() == 0) && (moveJoystick.Vertical() == 0))
        {
            animator.Play("Idel");
        }
        else
        {
            rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -15, 12), rigidBody.velocity.y);

            // Move
            if (moveJoystick.Horizontal() > 0.4F) // Move Front
            {
                animator.Play("Walk");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                transform.localScale = new Vector3(1, 1, 1);
                WalkEffect();
            }
            else if (moveJoystick.Horizontal() < -0.4F) // Move Back
            {
                animator.Play("Walk Back");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                transform.localScale = new Vector3(-1, 1, 1);
                WalkEffect();
            }


            // Jump & Crounch
            if ((moveJoystick.Vertical() > 0.6f) && (Physics2D.IsTouchingLayers(legCollider, 64))) // Jump 
            {
                walkEffect.Play();
                animator.speed = 0.3f;
                AudioManager.instance.Play("Jump");
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -15, 12));
                rigidBody.AddForce(jumpForce * Vector2.up * moveJoystick.Vertical(), ForceMode2D.Impulse);
            }
            else if (moveJoystick.Vertical() < -0.95f) // Crounch
            {
                rigidBody.AddForce(Vector2.down * downForce * Time.deltaTime);
            }

            if (moveJoystick.Vertical() < 0.3f) animator.speed = 1;
        }

        if (Physics2D.IsTouchingLayers(legCollider, 64))
        {
            if (velocity < -30)
            {
                fallEffect.Play();
                AudioManager.instance.Play("Fall");
                CamManager.Instance.Shake(6, 0.25f);
                playerHealth.TakeDamage(fallDamage * -velocity);
                velocity = 0;
                return;
            }
        }
        else
        {
            velocity = rigidBody.velocity.y;
        }
    }

    private void WalkEffect()
    {
        walkEffect.Play();
        if (!Physics2D.IsTouchingLayers(legCollider, 64))
        {
            isLegUp = true;
        }
        else if (isLegUp)
        {
            isLegUp = false;
            AudioManager.instance.Play("Walk");
        }
    }
}