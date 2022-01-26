using UnityEngine;

public class LastLevel : MonoBehaviour
{
    private void Start()
    {
        GlobalSave.instance.globalData.level = (byte)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        GlobalSave.instance.Save();
    }

    public void MenuButton()
    {
        AudioManager.instance.Play("ButtonBig");
        SceneLoader.instance.LoadScene(0);
    }
}
