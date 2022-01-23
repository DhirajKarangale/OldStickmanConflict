using UnityEngine;

public class PickItem : MonoBehaviour
{
    private enum Item { Coin, Key, PowerUps, Palak, Bomb }
    [SerializeField] Item itemPick;

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ParticleSystem pickEffect;
    [SerializeField] ParticleSystem healthEffect;
    private bool isPickAllow;
    private Vector3 oldPos;

    private void Start()
    {
        if (rigidBody != null) isPickAllow = !rigidBody.isKinematic;
        else isPickAllow = true;
        oldPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < -100)
        {
            // transform.position += new Vector3(10, 110, 0);
            transform.position = oldPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 7) && isPickAllow)
        {
            ParticleSystem.MainModule currEffect = Instantiate(pickEffect, transform.position, Quaternion.identity).main;
            switch (itemPick)
            {
                case Item.PowerUps:
                    //  if (playerHealth.currHealth >= playerHealth.health) return;
                    AudioManager.instance.Play("HealthIncrease");
                    currEffect = Instantiate(healthEffect, transform.position, Quaternion.identity).main;
                    currEffect.startColor = Color.yellow;
                    playerHealth.IncreaseHralth(20);
                    Destroy(gameObject);
                    isPickAllow = false;
                    break;
                case Item.Coin:
                    AudioManager.instance.Play("Coin");
                    GameSaveManager.instance.saveData.coin++;
                    Destroy(this.gameObject);
                    isPickAllow = false;
                    break;
                case Item.Key:
                    AudioManager.instance.Play("Coin");
                    GameSaveManager.instance.saveData.key++;
                    Destroy(this.gameObject);
                    isPickAllow = false;
                    break;
                case Item.Palak:
                    currEffect.startColor = Color.green;
                    AudioManager.instance.Play("Coin");
                    GameSaveManager.instance.saveData.palakCount++;
                    Destroy(this.gameObject);
                    isPickAllow = false;
                    break;
                case Item.Bomb:
                    AudioManager.instance.Play("Coin");
                    GameSaveManager.instance.saveData.bomb += 10;
                    Destroy(this.gameObject);
                    isPickAllow = false;
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