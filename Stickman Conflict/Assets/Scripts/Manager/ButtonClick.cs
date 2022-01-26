using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    private Vector3 originalScale = Vector3.one;
    private Vector3 changeScale = Vector3.one * 0.9f;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Scaling()));
        StartCoroutine(GetScale());
    }

    IEnumerator GetScale()
    {
        yield return new WaitForSeconds(2);
      
        originalScale = transform.localScale;
        changeScale = 0.9f * originalScale;
        transform.localScale = originalScale;
    }

    IEnumerator Scaling()
    {
        transform.localScale = changeScale;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 3);
        transform.localScale = originalScale;
    }
}