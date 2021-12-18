using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isPause;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    private float defaultSlowTime = -1;

    private void Awake()
    {
        Instance = this;
        controlPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if (!isPause && (Time.timeScale < 1) && (defaultSlowTime != -1))
        {
            Time.timeScale += (1 / defaultSlowTime) * Time.unscaledDeltaTime;
            AudioManager.instance.ModBG((0.4f + (1 / defaultSlowTime) * Time.unscaledDeltaTime), (0.5f + (1 / defaultSlowTime) * Time.unscaledDeltaTime));
        }
    }

    public void SlowMo(float slowFactor, float slowTime)
    {
        AudioManager.instance.ModBG(0.4f, 0.5f);
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        defaultSlowTime = slowTime;
    }

    public void PauseButton()
    {
        AudioManager.instance.Play("ButtonSmall");
        isPause = true;
        pausePanel.SetActive(true);
        controlPanel.SetActive(false);
        AudioManager.instance.ModBG(0.08f, 0.8f);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        AudioManager.instance.Play("ButtonSmall");
        isPause = false;
        pausePanel.SetActive(false);
        controlPanel.SetActive(true);
        AudioManager.instance.ModBG(0.5f, 1);
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        AudioManager.instance.Play("ButtonBig");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeButton();
    }

    public void MenuButton()
    {
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}
