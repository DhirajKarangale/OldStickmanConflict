using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EmenyHealth emenyHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] float damage,impactForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(emenyHealth.currState == EmenyHealth.State.Dead) return;
        
        if (collision.gameObject.layer == 7)
        {
            playerMovement.rigidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
            playerMovement.playerHealth.TakeDamage(damage);
            Destroy(Instantiate(playerBloodEffect,collision.transform.position,Quaternion.identity),2);
        }
        return;
    }
}
