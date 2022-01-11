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
    [SerializeField] Animator cageAnimator;

    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject destroyEffect;
    private float currHealth;
    private string[] headShotText = { "Head", "Shot", "HeadShot", "Pattse", "Op" };
    private Color[] headShotTextColor = { Color.black, Color.blue, Color.red, };

    private void Start()
    {
        if (cageAnimator) cageAnimator.Play("Close");
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

    public void TakeDamage(float damage, int headShot)
    {
        if (currHealth <= 0) Dead();
        else
        {
            currHealth -= damage;
            Hurt();

            if (headShot != -1)
            {
                damageText.color = headShotTextColor[headShot];
                damageText.text = headShotText[Random.Range(0, headShotText.Length)];

                GameObject currentDamageText = Instantiate(damageText.gameObject,
                new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), transform.position.y + 4, 0), transform.rotation);
                currentDamageText.GetComponent<TMP_Text>().rectTransform.localScale *= 3;

                Destroy(currentDamageText, 1.5f);
                CamManager.Instance.Shake(8, 0.3f);
            }
            else
            {
                damageText.color = spriteRenderer.color;
                damageText.text = damage.ToString();
                Destroy(Instantiate(damageText.gameObject,
                new Vector3(Random.Range(transform.position.x - 3, transform.position.x + 3), transform.position.y + 2, 0), transform.rotation), 0.9f);
            }
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

    private void Dead()
    {
        if (currState == State.Dead) return;
        healthSlider.gameObject.SetActive(false);
        CamManager.Instance.Shake(9, 0.4f);
        if (moveNPC)
        {
            moveNPC.rigidBody.velocity = Vector2.zero;
            moveNPC.animator.Play("Dead");
            Destroy(this.gameObject, 20);
        }
        else
        {
            AudioManager.instance.Play("Destroy");
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (cageAnimator) cageAnimator.Play("Open");
        currState = State.Dead;
    }
}