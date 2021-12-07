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
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject destroyEffect;
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
                if (moveNPC) moveNPC.Move();
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
        currState = State.Hurt;
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = currHealth / health;
        if (moveNPC) moveNPC.animator.Play("Hurt");
        Invoke("ExitHurt", 0.5f);
    }

    private void ExitHurt()
    {
        if (currHealth <= 0) currState = State.Dead;
        else currState = State.Move;
    }

    private void Dead()
    {
        currState = State.Dead;
        healthSlider.gameObject.SetActive(false);
        if (moveNPC)
        {
            moveNPC.rigidBody.velocity = Vector2.zero;
            moveNPC.animator.Play("Dead");
            moveNPC.rigidBody.isKinematic = true;
            Destroy(this.gameObject, 20);
        }
        else
        {
            AudioManager.instance.Play("Destroy");
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
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