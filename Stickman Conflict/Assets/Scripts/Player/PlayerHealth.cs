using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] GameObject controlCanvas, gameOverPanel;
    [SerializeField] Slider healthSlider;
    [SerializeField] Color low, high;
    public float currHealth;
    public static bool isPlayerDye;

    private void Start()
    {
        if (GameSave.instance.isDataLoaded) currHealth = GameSave.instance.gameData.currHealth;
        else currHealth = health;
        if (currHealth <= 2) currHealth = 100;
        GameSave.instance.gameData.currHealth = currHealth;

        isPlayerDye = false;
        SetHealthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            IncreaseHralth(health);
        }
    }

    public void Died()
    {
        if (isPlayerDye) return;
        AudioManager.instance.ModBG(0.07f);
        AudioManager.instance.Play("GameOver");
        controlCanvas.SetActive(false);
        Invoke("SetGameOverActive", 2);
        isPlayerDye = true;
    }

    public void TakeDamage(float damage)
    {
        AudioManager.instance.Play("PlayerHurt");
        if (currHealth <= 0) Died();
        else
        {
            currHealth -= damage;
            SetHealthBar();
            GameSave.instance.gameData.currHealth = currHealth;
        }
    }

    public void IncreaseHralth(float amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, health);
        SetHealthBar();
        GameSave.instance.gameData.currHealth = currHealth;
    }

    private void SetGameOverActive()
    {
        gameOverPanel.SetActive(true);
        // AudioManager.instance.Stop();
    }

    private void SetHealthBar()
    {
        healthSlider.value = currHealth / health;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);
    }
}