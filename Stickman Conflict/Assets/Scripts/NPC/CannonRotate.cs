using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] Transform player;
    [SerializeField] Transform cannonUp;

    [Header("Fire")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform attackPoint;
    [SerializeField] float fireDist;
    [SerializeField] float bulletForce;
    [SerializeField] float fireRate;
    private GameObject currBullet;
    private float timeToFire;

    private void Update()
    {
        if (enemyHealth.currState == EnemyHealth.State.Dead) return;

        cannonUp.right = cannonUp.position - player.position;

        if ((transform.position.x - player.position.x < fireDist) && (Time.time > timeToFire))
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        currBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        currBullet.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * bulletForce);
        Destroy(currBullet, 1);
    }
}