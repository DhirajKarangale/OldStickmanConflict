using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] GameObject controlCanvas, gameOverPanel;
    [SerializeField] Slider healthSlider;
    [SerializeField] Color low, high;
    public float currHealth;
    public bool isPlayerDye;

    private void Start()
    {
        if (SaveManager.instance.isDataLoaded) currHealth = SaveManager.instance.saveData.currHealth;
        else currHealth = health;
        SaveManager.instance.saveData.currHealth = currHealth;

        isPlayerDye = false;
        healthSlider.value = currHealth / health;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);
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
        isPlayerDye = true;
        controlCanvas.SetActive(false);
        Invoke("SetGameOverActive", 2);
    }

    public void TakeDamage(float damage)
    {
        AudioManager.instance.Play("PlayerHurt");
        if (currHealth <= 0) Died();
        else
        {
            currHealth -= damage;
            healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);
            healthSlider.value = currHealth / health;
            SaveManager.instance.saveData.currHealth = currHealth;
        }
    }

    public void IncreaseHralth(float amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, health);
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);
        healthSlider.value = currHealth / health;
        SaveManager.instance.saveData.currHealth = currHealth;
    }

    private void SetGameOverActive()
    {
        gameOverPanel.SetActive(true);
    }
}