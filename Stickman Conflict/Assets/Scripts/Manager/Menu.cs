using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] screens;

    private void Start()
    {
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
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}