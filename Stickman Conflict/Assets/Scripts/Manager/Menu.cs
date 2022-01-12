using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    private byte level;

    private void Start()
    {
        level = SaveManager.instance.saveData.level;
        if (level != 0)
        {
            level = SaveManager.instance.saveData.level;
        }
        else
        {
            level = 1;
            SaveManager.instance.saveData.level = level;
            SaveManager.instance.Save();
        }

        DesableScreens();
        screens[0].SetActive(true);
    }

    private void DesableScreens()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }

    public void PlayButton(GameObject loadScreen)
    {
        AudioManager.instance.Play("ButtonBig");
        DesableScreens();
        loadScreen.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}