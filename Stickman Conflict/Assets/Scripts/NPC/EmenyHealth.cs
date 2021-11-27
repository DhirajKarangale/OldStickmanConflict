using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmenyHealth : MonoBehaviour
{
    public enum State { Move, Hurt, Dead }
    public State currState;

    [Header("Refrence")]
    [SerializeField] MoveNPC moveNPC;
    [SerializeField] TMP_Text damageText;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject enemyBloodEffect;

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] float health;
    private float currHealth;

    private void Start()
    {
        currHealth = health;
        currState = State.Move;
        healthSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (currState)
        {
            case State.Move:
                moveNPC.Move();
                break;
            case State.Hurt:
                Hurt();
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    private void Hurt()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = currHealth / health;
        currState = State.Hurt;
        moveNPC.animator.Play("Hurt");
        Invoke("ExitHurt", 0.5f);
        Destroy(Instantiate(enemyBloodEffect, transform.position + new Vector3(0, 0.5f, 0), transform.rotation), 2);
    }

    private void ExitHurt()
    {
        if (currHealth <= 0) currState = State.Dead;
        else currState = State.Move;
    }

    private void Dead()
    {
        currState = State.Dead;
        moveNPC.rigidBody.velocity = Vector2.zero;
        healthSlider.gameObject.SetActive(false);
        moveNPC.animator.Play("Dead");
        moveNPC.rigidBody.isKinematic = true;
        Destroy(this.gameObject, 20);
    }

    public void TakeDamage(float damage)
    {
        if (currHealth <= 0) Dead();
        else
        {
            currHealth -= damage;
            Hurt();

            if (spriteRenderer.color == Color.white) damageText.color = Color.yellow;
            else damageText.color = spriteRenderer.color;
            damageText.text = damage.ToString();
            Destroy(Instantiate(damageText.gameObject, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y + 1, 0), transform.rotation), 1);
        }
    }
}