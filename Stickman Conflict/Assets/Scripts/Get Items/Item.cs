using UnityEngine;

public class Item : MonoBehaviour
{
    private enum ItemPick { Coin, Key }
    [SerializeField] ItemPick pickedItem;
    [SerializeField] GameObject effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if(pickedItem == ItemPick.Coin) SaveManager.instance.saveData.coin++;
            else if(pickedItem == ItemPick.Key) SaveManager.instance.saveData.key++;
            Destroy(Instantiate(effect, transform.position, Quaternion.identity), 0.5f);
            Destroy(this.gameObject);
        }
    }
}