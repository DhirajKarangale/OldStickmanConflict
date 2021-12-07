using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("MenuBG");
    }

    public void PlayButton()
    {
        AudioManager.instance.Play("ButtonBig");
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}