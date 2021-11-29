using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text coinText;
    [SerializeField] Text keyText;

    private void Update()
    {
        if (SaveManager.instance.saveData.coin <= 0) coinText.gameObject.SetActive(false);
        else coinText.gameObject.SetActive(true);

        if (SaveManager.instance.saveData.key <= 0) keyText.gameObject.SetActive(false);
        else keyText.gameObject.SetActive(true);

        coinText.text = SaveManager.instance.saveData.coin.ToString();
        keyText.text = SaveManager.instance.saveData.key.ToString();
    }
}