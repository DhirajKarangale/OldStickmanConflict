using UnityEngine;

public class PickItem : MonoBehaviour
{
    private enum Item { Coin, Key, PowerUps }
    [SerializeField] Item itemPick;

    [Header("Pick Item")]
    [SerializeField] GameObject pickEffect;

    [Header("Power Ups")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ParticleSystem healthEffect;
    private ParticleSystem.MainModule currEffect;
    [SerializeField] Color effectColor;
    [SerializeField] float healthIncreaseAmount;
    private bool isPickAllow;

    private void Start()
    {
        if (rigidBody != null) isPickAllow = !rigidBody.isKinematic;
        else isPickAllow = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && isPickAllow)
        {

            Instantiate(pickEffect, transform.position, Quaternion.identity);
            switch (itemPick)
            {
                case Item.PowerUps:
                    if (playerHealth.currHealth >= playerHealth.health) return;
                    currEffect = Instantiate(healthEffect, transform.position, Quaternion.identity).main;
                    currEffect.startColor = effectColor;
                    playerHealth.IncreaseHralth(healthIncreaseAmount);
                    break;
                case Item.Coin:
                    SaveManager.instance.saveData.coin++;
                    break;
                case Item.Key:
                    SaveManager.instance.saveData.key++;
                    break;
            }
            Destroy(this.gameObject);
        }

        if (!isPickAllow)
        {
            if (rigidBody.isKinematic)
            {
                rigidBody.isKinematic = false;
                Invoke("AllowHealthIncrease", 0.5f);
            }
        }
    }

    private void AllowHealthIncrease()
    {
        isPickAllow = true;
    }
}