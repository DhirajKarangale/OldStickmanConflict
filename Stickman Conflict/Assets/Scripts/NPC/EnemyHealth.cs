using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public enum State { Move, Hurt, Dead }
    public State currState;

    [Header("Refrence")]
    [SerializeField] MoveNPC moveNPC;
    [SerializeField] TMP_Text damageText;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject destroyEffect;
    private float currHealth;

    private void Start()
    {
        currHealth = health;
        healthSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (currState)
        {
            case State.Move:
                if (moveNPC) moveNPC.Move();
                break;
            case State.Hurt:
                Hurt();
                break;
            case State.Dead:
                Dead();
                break;
        }

        if (transform.position.y < -100)
        {
            Dead();
        }
    }

    public void TakeDamage(float damage)
    {
        if (currHealth <= 0) Dead();
        else
        {
            currHealth -= damage;
            Hurt();

            damageText.color = spriteRenderer.color;
            damageText.text = damage.ToString();
            Destroy(Instantiate(damageText.gameObject, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y + 1, 0), transform.rotation), 1);
        }
    }

    private void Hurt()
    {
        currState = State.Hurt;
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = currHealth / health;
        if (moveNPC) moveNPC.animator.Play("Hurt");
        if (currHealth <= 0) Dead();
        Invoke("ExitHurt", 0.5f);
    }

    private void ExitHurt()
    {
        if (currHealth <= 0) currState = State.Dead;
        else currState = State.Move;
    }

    public void Dead()
    {
        if (currState == State.Dead) return;
        healthSlider.gameObject.SetActive(false);
        CamShake.Instance.Shake(8, 0.3f);
        if (moveNPC)
        {
            moveNPC.rigidBody.velocity = Vector2.zero;
            moveNPC.animator.Play("Dead");
            Destroy(this.gameObject, 20);
            //moveNPC.rigidBody.isKinematic = true;
        }
        else
        {
            AudioManager.instance.Play("Destroy");
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        currState = State.Dead;
    }
}