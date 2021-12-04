using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject playerBloodEffect, destroyEffect;
    [SerializeField] float damage,impactForce;

    private void Start()
    {
        if(playerMovement == null)
        {
            playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyHealth != null) && enemyHealth.currState == EnemyHealth.State.Dead) return;
        
        if (collision.gameObject.layer == 7)
        {
            playerMovement.rigidBody.AddForce(new Vector2(transform.localScale.x, 0.5f) * impactForce, ForceMode2D.Force);
            playerMovement.playerHealth.TakeDamage(damage);
            Destroy(Instantiate(playerBloodEffect,collision.transform.position,Quaternion.identity),1);
            if(enemyHealth == null) Destroy(this.gameObject);
        }
        else
        {
            if(enemyHealth == null) 
            {
                Destroy(Instantiate(destroyEffect,transform.position,Quaternion.identity),1);
                Destroy(this.gameObject);
            }
        }
        return;
    }
}
