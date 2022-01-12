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

    private void Update()
    {
        if (transform.position.y < -100)
        {
            transform.position += new Vector3(10, 110, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && isPickAllow)
        {
            Instantiate(pickEffect, transform.position, Quaternion.identity);
            switch (itemPick)
            {
                case Item.PowerUps:
                    //  if (playerHealth.currHealth >= playerHealth.health) return;
                    AudioManager.instance.Play("HealthIncrease");
                    currEffect = Instantiate(healthEffect, transform.position, Quaternion.identity).main;
                    currEffect.startColor = effectColor;
                    playerHealth.IncreaseHralth(healthIncreaseAmount);
                    Destroy(gameObject);
                    break;
                case Item.Coin:
                    AudioManager.instance.Play("Coin");
                    SaveManager.instance.saveData.coin++;
                    Destroy(this.gameObject);
                    break;
                case Item.Key:
                    AudioManager.instance.Play("Coin");
                    SaveManager.instance.saveData.key++;
                    Destroy(this.gameObject);
                    break;
            }
            Destroy(gameObject);
        }

        if (!isPickAllow && rigidBody.isKinematic)
        {
            rigidBody.isKinematic = false;
            Invoke("AllowPick", 0.5f);
        }
    }

    private void AllowPick()
    {
        isPickAllow = true;
    }
}