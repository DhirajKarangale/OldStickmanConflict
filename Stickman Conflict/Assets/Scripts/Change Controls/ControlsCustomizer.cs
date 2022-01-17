using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsCustomizer : MonoBehaviour
{
    [SerializeField] Slider sizeSlider;
    [SerializeField] Slider alphaSlider;
    [SerializeField] Text sizeText;
    [SerializeField] Text alphaText;
    public DragButton selectButton;
    [SerializeField] DragButton[] buttons;

    private void Update()
    {
        if (selectButton)
        {
            sizeText.text = "Size : " + (int)(sizeSlider.value * 100) + "%";
            alphaText.text = "Alpha : " + (int)(alphaSlider.value * 100) + "%";
            sizeSlider.interactable = true;
            alphaSlider.interactable = true;
            selectButton.SetSizeAlpha(sizeSlider.value, alphaSlider.value);
        }
        else
        {
            sizeText.text = "Not Selected";
            alphaText.text = "Not Selected";
            sizeSlider.interactable = false;
            alphaSlider.interactable = false;
        }
    }

    public void SetButtonData(float size, float alpha)
    {
        sizeSlider.value = size;
        alphaSlider.value = alpha;
    }

    public void SaveData()
    {
        foreach (var b in buttons)
        {
            b.SaveData();
        }
    }

    public void ResetData()
    {
        foreach (var b in buttons)
        {
            b.Reset();
        }
    }
}
