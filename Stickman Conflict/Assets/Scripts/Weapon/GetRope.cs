using UnityEngine;

public class GetRope : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("RopeGet", 0) != 1)
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
        if (collision.gameObject.layer == 7)
        {
           PlayerPrefs.SetInt("RopeGet", 1);
            WeaponPickThrow.instance.ropeButton.SetActive(true);
            WeaponPickThrow.instance.ropeEmmiter.SetActive(true);
            Destroy(gameObject);
        }
    }

}