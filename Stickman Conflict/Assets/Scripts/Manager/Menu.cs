using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] Text tipText;
    [SerializeField] Text coinText;
    [SerializeField] GameObject[] screens;
    [SerializeField] string[] tips;
    private byte level;

    private void Start()
    {
        level = GlobalSave.instance.globalData.level;
        if (level == 0)
        {
            level = 3;
            GlobalSave.instance.globalData.level = level;
            GlobalSave.instance.Save();
        }

        PanelButton(screens[0]);
        StartCoroutine(GenerateTips());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (screens[0].activeInHierarchy)
            {
                PanelButton(screens[1]);
            }
            else
            {
                PanelButton(screens[0]);
            }
        }

        if (GlobalSave.instance.globalData.coin < 0) coinText.gameObject.SetActive(false);
        else coinText.gameObject.SetActive(true);
        coinText.text = GlobalSave.instance.globalData.coin.ToString();
    }

    private void DesableScreens()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
    }

    public void PanelButton(GameObject panel)
    {
        AudioManager.instance.Play("Button");
        DesableScreens();
        panel.SetActive(true);
    }

    public void SceneButton(int sceneIndex)
    {
        if (sceneIndex == -1)
        {
            AudioManager.instance.Play("ButtonBig");
            sceneIndex = level;
        }
        else
        {
            AudioManager.instance.Play("Button");
        }
        SceneLoader.instance.LoadScene(sceneIndex);
    }

    public void LinkButton(string link)
    {
        AudioManager.instance.Play("Button");
        Application.OpenURL(link);
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