using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text coinText;

    private void Update()
    {
        coinText.text = SaveManager.instance.saveData.coin.ToString();
    }
}