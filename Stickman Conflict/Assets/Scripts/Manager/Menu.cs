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
        level = SaveManager.instance.saveData.level;
        if (level == 0)
        {
            level = 2;
            SaveManager.instance.saveData.level = level;
            SaveManager.instance.Save();
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
        if (sceneIndex == -1) sceneIndex = 2;
        SceneLoader.instance.LoadScene(sceneIndex);
    }

    public void QuitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }

    IEnumerator GenerateTips()
    {
        int tipCount = Random.Range(0, tips.Length);
        tipText.text = tips[tipCount];
        while (true)
        {
            // 5 sec
            yield return new WaitForSeconds(5);
            // fade to zero
            tipText.CrossFadeAlpha(0, 0.5f, false); // Text fade (Gone)
            // 1 sec
            yield return new WaitForSeconds(1);
            // new tip
            tipCount = (tipCount + 1) % tips.Length;
            tipText.text = tips[tipCount];
            // fade to one
            tipText.CrossFadeAlpha(1, 0.5f, false); // Text come 
        }
    }
}