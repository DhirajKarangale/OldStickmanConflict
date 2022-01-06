using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Rigidbody2D weapon;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Animator animator;

    private void Update()
    {
        if (enemyHealth.currState == EnemyHealth.State.Dead)
        {
            Destroy(this.gameObject.GetComponent<EnemyAttack>());
            return;
        }

        if (weapon)
        {
            weapon.transform.localPosition = new Vector3(0, -0.503f, 0);
            weapon.angularVelocity = 0.5f;
        }
    }

    public void Attack()
    {
        weapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
        SwardAttack();
    }

    private void SwardAttack()
    {
        animator.Play("SwardAttack");
    }
}