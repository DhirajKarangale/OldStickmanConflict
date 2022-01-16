using UnityEngine;

public class GetWeapon : MonoBehaviour
{
    [SerializeField] Animator chestAnim;
    [SerializeField] GameObject[] weaponGets;
    private int weaponGetter;
    private void Start()
    {
        //PlayerPrefs.DeleteKey("WeaponGetter" + transform.name);
        weaponGetter = PlayerPrefs.GetInt("WeaponGetter" + transform.name, 0);
        CheckPoint.onCheckPointCross += OnCheckPointCross;

        if (weaponGetter == 1)
        {
            Destroy(gameObject);
        }
        else
        {
            for (int i = 0; i < weaponGets.Length; i++)
            {
                weaponGets[i].SetActive(false);
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
        }

    }

    private void Update()
    {
        if ((weaponGetter == 0) && (chestAnim.GetCurrentAnimatorStateInfo(0).IsName("Play")))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && (weaponGetter == 0))
        {
            for (int i = 0; i < weaponGets.Length; i++)
            {
                weaponGets[i].SetActive(true);
            }
            weaponGetter = 1;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
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