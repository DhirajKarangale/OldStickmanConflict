using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spwanPos;
    [SerializeField] Transform grabLeft;
    [SerializeField] float force;
    [SerializeField] int bulletNo;
    [SerializeField] float fireRate;
    private bool isShootAllow = true;

    private void Update()
    {
        if (transform.parent != grabLeft) return;

        Vector2 joystick = new Vector2(handRotateJoystick.Vertical(), handRotateJoystick.Horizontal());
        if (joystick != Vector2.zero && isShootAllow)
        {
            StartCoroutine(Shoot());
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, -90);
    }

    IEnumerator Shoot()
    {
        isShootAllow = false;
        for (int i = 0; i < bulletNo; i++)
        {
            GameObject currBullet = Instantiate(bullet, spwanPos.position, spwanPos.rotation);
            currBullet.GetComponent<Rigidbody2D>().AddForce(spwanPos.right * force);
            Destroy(currBullet, 10);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(fireRate);
        isShootAllow = true;
    }
}
