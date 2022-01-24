using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text coinText;
    [SerializeField] Text keyText;
    [SerializeField] Text palakText;

    private void Update()
    {
        if (GameSave.instance.gameData.coin <= 0) coinText.gameObject.SetActive(false);
        else coinText.gameObject.SetActive(true);

        if (GameSave.instance.gameData.key <= 0) keyText.gameObject.SetActive(false);
        else keyText.gameObject.SetActive(true);

        if (GameSave.instance.gameData.palakCount <= 0) palakText.gameObject.SetActive(false);
        else palakText.gameObject.SetActive(true);

        palakText.text = GameSave.instance.gameData.palakCount.ToString();
        coinText.text = GameSave.instance.gameData.coin.ToString();
        keyText.text = GameSave.instance.gameData.key.ToString();
    }
}