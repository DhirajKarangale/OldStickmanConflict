using UnityEngine;

public class LastLevel : MonoBehaviour
{
    public void MenuButton()
    {
        AudioManager.instance.Play("ButtonBig");
        SceneLoader.instance.LoadScene(0);
    }
}
