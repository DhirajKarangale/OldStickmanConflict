using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextPop : MonoBehaviour
{
    private Text text;
    private Vector3 originalScale = Vector3.one;
    private Vector3 changeScale = Vector3.one * 0.85f;
    private string previousText;

    private void Start()
    {
        text = GetComponent<Text>();
        previousText = text.text;
        StartCoroutine(GetScale());
    }

    private void FixedUpdate()
    {
        if (text.text != previousText)
        {
            StartCoroutine(Scaling());
        }
        previousText = text.text;
    }

    IEnumerator GetScale()
    {
        yield return new WaitForSeconds(2);
        originalScale = transform.localScale;
        changeScale = originalScale * 0.85f;
    }

    IEnumerator Scaling()
    {
        transform.localScale = changeScale;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 3);
        transform.localScale = originalScale;
    }
}