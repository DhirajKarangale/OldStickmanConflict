using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPause;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;

    private void Start()
    {
        AudioManager.instance.StopBG();
        AudioManager.instance.Play("IdelBG");

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
    }

    public void PauseButton()
    {
        AudioManager.instance.Play("ButtonSmall");
        isPause = true;
        pausePanel.SetActive(true);
        controlPanel.SetActive(false);
        AudioManager.instance.ModBG(0.08f, 1);
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
        Time.timeScale = 1;
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
