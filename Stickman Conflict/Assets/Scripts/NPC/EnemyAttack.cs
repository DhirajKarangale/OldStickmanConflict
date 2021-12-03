using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playerBloodEffect;
    [SerializeField] float damage,impactForce;
    [SerializeField] bool isBullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyHealth != null) && enemyHealth.currState == EnemyHealth.State.Dead) return;
        
        if (collision.gameObject.layer == 7)
        {
            playerMovement.rigidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
            playerMovement.playerHealth.TakeDamage(damage);
            Destroy(Instantiate(playerBloodEffect,collision.transform.position,Quaternion.identity),2);
            if(enemyHealth == null) Destroy(this.gameObject);
        }
        return;
    }
}
