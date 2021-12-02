using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject controlCanvas, gameOverPanel;
    [SerializeField] Slider healthSlider;
    [SerializeField] Color low, high;
    private float currHealth;
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

    public void Died()
    {
        isPlayerDye = true;
        controlCanvas.SetActive(false);
        Invoke("SetGameOverActive", 2);
    }

    public void TakeDamage(float damage)
    {
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
        healthSlider.value = currHealth / health;
        SaveManager.instance.saveData.currHealth = currHealth;
    }

    private void SetGameOverActive()
    {
        gameOverPanel.SetActive(true);
    }
}