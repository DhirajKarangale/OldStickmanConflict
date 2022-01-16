using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevPortal : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject controlCanvas, panelCanvas, loadingPanel;
    private bool isPlayerCollided;

    private void Start()
    {
        loadingPanel.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerCollided)
        {
            if (player.transform.position.y <= 40)
            {
                player.rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                player.rigidBody.isKinematic = true;
                loadingPanel.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            controlCanvas.SetActive(false);
            panelCanvas.SetActive(false);
            player.walkEffect.gameObject.SetActive(false);
            player.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            isPlayerCollided = true;
        }
    }
}