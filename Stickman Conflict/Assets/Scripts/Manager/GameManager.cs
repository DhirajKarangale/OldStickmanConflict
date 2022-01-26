using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isPause;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] ParticleSystem healthEffect;
    [SerializeField] GameObject[] upLevelButtons;

    private void Awake()
    {
        instance = this;
        controlPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        // Desable Extra button in level
        for (int i = 0; i < upLevelButtons.Length; i++)
        {
            upLevelButtons[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (!PlayerHealth.isPlayerDye && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }
    }

    public void PauseButton()
    {
        isPause = true;
        AudioManager.instance.Play("ButtonSmall");
        pausePanel.SetActive(true);
        controlPanel.SetActive(false);
        AudioManager.instance.ModBG(0.08f);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        isPause = false;
        AudioManager.instance.Play("ButtonSmall");
        controlPanel.SetActive(true);
        pausePanel.SetActive(false);
        AudioManager.instance.ModBG(0.5f);
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play("ButtonBig");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneLoader.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play("Button");
        SceneLoader.instance.LoadScene(0);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }

    public void PalakButton()
    {
        GameSave.instance.gameData.palakCount -= 1;
        AudioManager.instance.Play("HealthIncrease");
        ParticleSystem.MainModule currEffect = Instantiate(healthEffect, playerHealth.transform.position, Quaternion.identity).main;
        currEffect.startColor = Color.green;
        playerHealth.IncreaseHralth(100);
    }
}