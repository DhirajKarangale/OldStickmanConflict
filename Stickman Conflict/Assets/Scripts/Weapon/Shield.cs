using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] GameObject shieldObject;
    [SerializeField] Button button;
    [SerializeField] Text buttonText;
    [SerializeField] float shieldTime;
    [SerializeField] float buttonTime;

    private float currShieldTime;
    private float currButtonTime;
    private bool isShiedlDesableStart, isButtonActiveStart;

    private void Start()
    {
        shieldObject.SetActive(false);
        currShieldTime = shieldTime;
        currButtonTime = buttonTime;
        buttonText.gameObject.SetActive(false);
        button.interactable = true;
    }

    private void Update()
    {
        if (isShiedlDesableStart) StartShieldDesable();
        else if (isButtonActiveStart) StartButtonActive();
    }



    IEnumerator StartButtonActiveCO()
    {
        buttonText.text = "Use in";

        yield return new WaitForSeconds(2);

        isButtonActiveStart = true;
    }

    private void StartButtonActive()
    {
        if (currButtonTime > 0)
        {
            buttonText.text = "Use in\n" + (int)(currButtonTime);
            currButtonTime -= Time.deltaTime;
        }
        else
        {
            buttonText.gameObject.SetActive(false);
            currButtonTime = buttonTime;
            isButtonActiveStart = false;
            button.interactable = true;
        }
    }



    IEnumerator StartShieldDesableCO()
    {
        buttonText.text = "Out in";

        yield return new WaitForSeconds(2);

        isShiedlDesableStart = true;
    }

    private void StartShieldDesable()
    {
        if (currShieldTime > 0)
        {
            buttonText.text = "Out in\n" + (int)currShieldTime;
            currShieldTime -= Time.deltaTime;
        }
        else
        {
            currShieldTime = shieldTime;
            shieldObject.SetActive(false);
            isShiedlDesableStart = false;
            StartCoroutine(StartButtonActiveCO());
        }
    }



    public void ShieldButton()
    {
        shieldObject.SetActive(true);
        button.interactable = false;
        buttonText.gameObject.SetActive(true);
        StartCoroutine(StartShieldDesableCO());
    }
}