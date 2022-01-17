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

            if (player.transform.position.x < (transform.position.x - 3.5f))
            {
                player.rigidBody.AddForce(Vector2.right * 2);
            }
            else if (player.transform.position.x > (transform.position.x + 3.5f))
            {
                player.rigidBody.AddForce(Vector2.left * 2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            AudioManager.instance.Play("UsePortal");
            controlCanvas.SetActive(false);
            panelCanvas.SetActive(false);
            player.walkEffect.gameObject.SetActive(false);
            isPlayerCollided = true;
        }
    }
}