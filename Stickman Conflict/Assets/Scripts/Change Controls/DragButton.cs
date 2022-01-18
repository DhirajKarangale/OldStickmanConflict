using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private Vector2 currPos;
    private Vector3 newPos;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] ControlsCustomizer controlsCustomizer;
    [SerializeField] Vector2 defaultPos;
    [SerializeField] Vector2 defaultSize;
    private UnityEngine.UI.Button button;
    private EventTrigger eventTrigger;

    private void Start()
    {
        // PlayerPrefs.DeleteKey(transform.name + "ButtonSize");
        // PlayerPrefs.DeleteKey(transform.name + "ButtonAlpha");
        // PlayerPrefs.DeleteKey(transform.name + "ButtonXPos");
        // PlayerPrefs.DeleteKey(transform.name + "ButtonYPos");

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        eventTrigger = GetComponent<EventTrigger>();
        button = GetComponent<UnityEngine.UI.Button>();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1)
        {
            canvasGroup.enabled = false;
            this.enabled = false;
        }
        else
        {
            if (eventTrigger) eventTrigger.enabled = false;
            if (button) button.enabled = false;
        }

        Load();
    }

    private void Update()
    {
        currPos = RectTransformUtility.WorldToScreenPoint(new Camera(), transform.position);
        currPos.x = Mathf.Clamp(currPos.x, rectTransform.sizeDelta.x / 2, Screen.width - rectTransform.sizeDelta.x / 2);
        currPos.y = Mathf.Clamp(currPos.y, rectTransform.sizeDelta.y / 2, Screen.height - rectTransform.sizeDelta.y / 2);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, currPos, new Camera(), out newPos);
        transform.position = newPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controlsCustomizer.selectButton = this;
        controlsCustomizer.SetButtonData(rectTransform.sizeDelta.x / defaultSize.x, canvasGroup.alpha);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void SetSizeAlpha(float size, float alpha)
    {
        rectTransform.sizeDelta = defaultSize * size;
        // foreach (var g in GetComponentsInChildren<RectTransform>())
        // {
        //     g.sizeDelta = defaultSize * size;
        // }
        canvasGroup.alpha = alpha;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(transform.name + "ButtonSize", rectTransform.sizeDelta.x / defaultSize.x);
        PlayerPrefs.SetFloat(transform.name + "ButtonAlpha", canvasGroup.alpha);
        PlayerPrefs.SetFloat(transform.name + "ButtonXPos", rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(transform.name + "ButtonYPos", rectTransform.anchoredPosition.y);
    }

    public void Reset()
    {
        SetSizeAlpha(1, 1);
        rectTransform.anchoredPosition = defaultPos;
    }

    public void Load()
    {
        SetSizeAlpha(PlayerPrefs.GetFloat(transform.name + "ButtonSize", rectTransform.sizeDelta.x / defaultSize.x), PlayerPrefs.GetFloat(transform.name + "ButtonAlpha", 1));
        rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(transform.name + "ButtonXPos", defaultPos.x), PlayerPrefs.GetFloat(transform.name + "ButtonYPos", defaultPos.y));
    }
}
