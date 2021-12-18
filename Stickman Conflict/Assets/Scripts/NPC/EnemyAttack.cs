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
            weapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }

    public void Attack()
    {
        SwardAttack();
    }

    private void SwardAttack()
    {
        animator.Play("SwardAttack");
    }
}