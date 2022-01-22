using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text coinText;
    [SerializeField] Text keyText;
    [SerializeField] Text palakText;

    private void Update()
    {
        if (SaveManager.instance.saveData.coin <= 0) coinText.gameObject.SetActive(false);
        else coinText.gameObject.SetActive(true);

        if (SaveManager.instance.saveData.key <= 0) keyText.gameObject.SetActive(false);
        else keyText.gameObject.SetActive(true);

        if (SaveManager.instance.saveData.palakCount <= 0) palakText.gameObject.SetActive(false);
        else palakText.gameObject.SetActive(true);

        palakText.text = SaveManager.instance.saveData.palakCount.ToString();
        coinText.text = SaveManager.instance.saveData.coin.ToString();
        keyText.text = SaveManager.instance.saveData.key.ToString();
    }
}