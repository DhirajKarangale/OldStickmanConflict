using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            SaveManager.instance.saveData.coin++;
            Destroy(Instantiate(effect, transform.position, Quaternion.identity), 0.5f);
            Destroy(this.gameObject);
        }
    }
}