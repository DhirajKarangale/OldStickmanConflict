using UnityEngine;

public class Cannon : MonoBehaviour
{
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
        if (PlayerHealth.isPlayerDye) return;

        cannonUp.right = cannonUp.position - player.position + new Vector3(0, -1.7f, 0);
        cannonUp.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(cannonUp.rotation.eulerAngles.z, 270, 360));

        if (((transform.position.x - player.position.x) < fireDist) && (transform.position.x - player.position.x > 0) && (Time.time > timeToFire))
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        AudioManager.instance.Play("Cannon");
        currBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        currBullet.GetComponent<Rigidbody2D>().AddForce(attackPoint.right * bulletForce);
        Destroy(currBullet, 1);
    }
}