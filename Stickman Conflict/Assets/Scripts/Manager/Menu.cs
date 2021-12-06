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
        SceneManager.LoadScene(1);
    }
}
