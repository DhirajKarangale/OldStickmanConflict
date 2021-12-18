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
    [SerializeField] ParticleSystem dustEffectLeft, dustEffectRight, fallEffect;

    [Header("Attributes")]
    [SerializeField] float jumpForce;
    [SerializeField] float downForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float fallDamage;
    public static int weaponRotation = 1;
    private float velocity = 0;
    public bool isLegUp;

    private void Start()
    {
        if (SaveManager.instance.isDataLoaded)
        {
            transform.position = new Vector3(SaveManager.instance.saveData.playerSpwanPos[0] + 5, SaveManager.instance.saveData.playerSpwanPos[1], SaveManager.instance.saveData.playerSpwanPos[2]);
        }
    }

    private void Update()
    {
        if ((playerHealth != null) && playerHealth.isPlayerDye) return;
        if (transform.position.y < -100) playerHealth.Died();

        if (Input.GetAxis("Horizontal") != 0)
        {
            DustEffect();
            animator.Play("Walk");
            rigidBody.AddForce(Vector2.right * moveSpeed * Input.GetAxis("Horizontal"));
            weaponRotation = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            DustEffect();
            animator.speed = 0.3f;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -15, 12));
            rigidBody.AddForce(jumpForce * Vector2.up * Input.GetAxis("Vertical"), ForceMode2D.Impulse);
        }

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
                DustEffect();
                animator.Play("Walk");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                weaponRotation = 1;
                transform.localScale = new Vector3(1, 1, 1);
                WalkSound();
            }
            else if (moveJoystick.Horizontal() < -0.4F) // Move Back
            {
                DustEffect();
                animator.Play("Walk Back");
                rigidBody.AddForce(Vector2.right * moveSpeed * moveJoystick.Horizontal());
                weaponRotation = -1;
                transform.localScale = new Vector3(-1, 1, 1);
                WalkSound();
            }


            // Jump & Crounch
            if ((moveJoystick.Vertical() > 0.6f) && (Physics2D.IsTouchingLayers(legCollider, 64))) // Jump 
            {
                DustEffect();
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
                playerHealth.TakeDamage(fallDamage);
                velocity = 0;
                CamShake.Instance.Shake(6, 0.25f);
                return;
            }
        }
        else
        {
            velocity = rigidBody.velocity.y;
        }
    }

    private void WalkSound()
    {
        if (!Physics2D.IsTouchingLayers(legCollider, 64))
        {
            isLegUp = true;
        }

        if (isLegUp && Physics2D.IsTouchingLayers(legCollider, 64))
        {
            isLegUp = false;
            AudioManager.instance.Play("Walk");
        }
    }

    private void DustEffect()
    {
        dustEffectLeft.Play();
        dustEffectRight.Play();
    }
}