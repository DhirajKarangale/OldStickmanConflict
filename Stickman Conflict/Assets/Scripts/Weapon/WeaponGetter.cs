using UnityEngine;

public class WeaponGetter : MonoBehaviour
{
    private int weaponGetter;
    private void Start()
    {
        //PlayerPrefs.DeleteKey("WeaponGetter" + transform.name);
        weaponGetter = PlayerPrefs.GetInt("WeaponGetter" + transform.name, 0);
        CheckPoint.onCheckPointCross += OnCheckPointCross;
        if (weaponGetter != 1)
        {
            WeaponPickThrow.instance.ropeButton.SetActive(false);
            WeaponPickThrow.instance.ropeEmmiter.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && (weaponGetter == 0))
        {
            WeaponPickThrow.instance.ropeButton.SetActive(true);
            WeaponPickThrow.instance.ropeEmmiter.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            weaponGetter = 1;
        }
    }


    private void OnDestroy()
    {
        CheckPoint.onCheckPointCross -= OnCheckPointCross;
    }

    private void OnCheckPointCross()
    {
        PlayerPrefs.SetInt("WeaponGetter" + transform.name, weaponGetter);
        if (weaponGetter == 1) Destroy(this.gameObject);
    }
}