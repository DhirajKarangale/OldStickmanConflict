using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevPortal : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    private bool isPlayerCollided;

    private void Update()
    {
        if (isPlayerCollided)
        {
            if ((player.transform.position.y - transform.position.y) <= -10)
            {
                player.rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                player.rigidBody.isKinematic = true;
                SceneLoader.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                this.enabled = false;
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
            player.walkEffect.gameObject.SetActive(false);
            isPlayerCollided = true;
        }
    }
}