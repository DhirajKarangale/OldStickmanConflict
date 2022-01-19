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


        // while (true)
        // {
        //     yield return new WaitForSeconds(5);

        //     // StartCoroutine(FadeFullAlpha());
        //     StartCoroutine(FadeZeroAlpha());

        //     yield return new WaitForSeconds(1);

        //     tipCount = (tipCount + 1) % tips.Length;
        //     tipText.text = tips[tipCount];

        //     StartCoroutine(FadeFullAlpha());
        //     // StartCoroutine(FadeZeroAlpha());
        // }

        while (true)
        {
            tipText.text = tips[tipCount];

            // 5 sec
            yield return new WaitForSeconds(5);
            // fade to zero
            StopCoroutine(FadeTo0());
            // 1 sec
            yield return new WaitForSeconds(1);
            // new tip
            tipCount = (tipCount + 1) % tips.Length;
            // tipText.text = tips[tipCount];
            // fade to one
            StopCoroutine(FadeTo1());
        }
    }

    IEnumerator FadeTo1()
    {
        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 0);
        while (tipText.color.a < 1)
        {
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a + Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeTo0()
    {
        tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, 1);
        while (tipText.color.a > 0)
        {
            tipText.color = new Color(tipText.color.r, tipText.color.g, tipText.color.b, tipText.color.a - Time.deltaTime);
            yield return null;
        }
    }
}