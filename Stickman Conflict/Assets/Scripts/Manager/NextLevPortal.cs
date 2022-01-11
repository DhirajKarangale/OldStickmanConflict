using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevPortal : MonoBehaviour
{
    [SerializeField] SpriteRenderer blue, gray;
    [SerializeField] Rigidbody2D player;
    [SerializeField] GameObject controlCanvas, panelCanvas, loadingPanel, playerWalkEffect;

    private void Start()
    {
        loadingPanel.SetActive(false);
        StartCoroutine(AlterSprite());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        controlCanvas.SetActive(false);
        panelCanvas.SetActive(false);
        playerWalkEffect.SetActive(false);
        player.constraints = RigidbodyConstraints2D.FreezePosition;
        player.isKinematic = true;
        player.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;

        yield return new WaitForSeconds(2);

        player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        loadingPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator AlterSprite()
    {
        blue.sortingOrder = 8;
        gray.sortingOrder = 7;

        yield return new WaitForSeconds(0.25f);

        blue.sortingOrder = 7;
        gray.sortingOrder = 8;

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(AlterSprite());
    }
}