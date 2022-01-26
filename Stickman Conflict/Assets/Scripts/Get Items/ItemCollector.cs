using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] Text coinText;
    [SerializeField] Text keyText;
    [SerializeField] Text palakText;
    [SerializeField] Text stone;

    private void Update()
    {
        if (coinText)
        {
            if (GameSave.instance.gameData.coin <= 0) coinText.gameObject.SetActive(false);
            else coinText.gameObject.SetActive(true);
            coinText.text = GameSave.instance.gameData.coin.ToString();
        }

        if (keyText)
        {
            if (GameSave.instance.gameData.key <= 0) keyText.gameObject.SetActive(false);
            else keyText.gameObject.SetActive(true);
            keyText.text = GameSave.instance.gameData.key.ToString();
        }

        if (palakText)
        {
            if (GameSave.instance.gameData.palakCount <= 0) palakText.gameObject.SetActive(false);
            else palakText.gameObject.SetActive(true);
            palakText.text = GameSave.instance.gameData.palakCount.ToString();
        }

        if (stone)
        {
            if (GameSave.instance.gameData.stone <= 0) stone.gameObject.SetActive(false);
            else stone.gameObject.SetActive(true);
        }
    }
}