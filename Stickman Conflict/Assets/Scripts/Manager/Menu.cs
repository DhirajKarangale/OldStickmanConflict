using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    private byte level;

    private void Start()
    {
        level = SaveManager.instance.saveData.level;
        if (level == 0)
        {
            level = 2;
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

    public void SceneChangeButton(int sceneIndex)
    {
        AudioManager.instance.Play("ButtonBig");
        if(sceneIndex == -1) sceneIndex = 2;
        SceneLoader.instance.LoadScene(sceneIndex);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}