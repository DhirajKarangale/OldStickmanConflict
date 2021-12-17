using UnityEngine;
using System.Linq;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Rigidbody2D weapon;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerweapon;

    private void Update()
    {
        if (enemyHealth.currState == EnemyHealth.State.Dead)
        {
            SetPlayerWeapon();
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

    private void SetPlayerWeapon()
    {
        Rigidbody2D tempWeapon = WeaponPickThrow.instance.weapons.Where(temp => temp.name == playerweapon.name).SingleOrDefault();
        if (tempWeapon != null) return;
        Destroy(weapon.gameObject);
        GameObject currPlayerWeapon = Instantiate(playerweapon, transform.position, Quaternion.identity, WeaponPickThrow.instance.transform);
        currPlayerWeapon.name = playerweapon.name;
        WeaponPickThrow.instance.weapons.Add(currPlayerWeapon.GetComponent<Rigidbody2D>());
    }
}
