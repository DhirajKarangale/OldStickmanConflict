using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int health;

    public void TakeDamage(int damage)
    {
        if(health <= 0)
        {
            animator.Play("Dead");
            Destroy(this.gameObject,10);
        }
        else
        {
            animator.Play("Hurt");
            health -= damage;
        }
    }
}
