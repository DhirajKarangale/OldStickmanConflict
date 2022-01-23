using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    [SerializeField] Text tipText;
    [SerializeField] string[] tips;
    private byte level;

    private void Start()
    {
        level = GameSaveManager.instance.saveData.level;
        if (level == 0)
        {
            level = 2;
            GameSaveManager.instance.saveData.level = level;
            GameSaveManager.instance.Save();
        }

        DesableScreens();
        screens[0].SetActive(true);

        StartCoroutine(GenerateTips());
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
        // if (sceneIndex == -1) sceneIndex = level;
        SceneLoader.instance.LoadScene(2);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }

    IEnumerator GenerateTips()
    {
        tipText.text = tips[Random.Range(0, tips.Length)];
        yield return new WaitForSeconds(5);
        tipText.CrossFadeAlpha(0, 0.5f, false); // Text fade (Gone)
        yield return new WaitForSeconds(1);
        tipText.CrossFadeAlpha(1, 0.5f, false); // Text come 

        StartCoroutine(GenerateTips());
    }
}