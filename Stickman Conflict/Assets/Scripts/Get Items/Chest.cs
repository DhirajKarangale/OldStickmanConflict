using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject openLight;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject item;
    [SerializeField] int numberOfItem;
    private int chestOpen;

    private void Start()
    {
       // PlayerPrefs.DeleteKey("Chest" + transform.name);
        chestOpen = PlayerPrefs.GetInt("Chest" + transform.name, 0);
        if (chestOpen == 0) animator.Play("Close");
        else if (chestOpen == 1) animator.Play("Open");
        openLight.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7) && (chestOpen == 0))
        {
            animator.Play("Play");
            Destroy(Instantiate(effect, transform.position, Quaternion.identity), 1);
            for (int i = 0; i < numberOfItem; i++)
            {
                Instantiate(item, new Vector3(Random.Range(transform.position.x - 1,transform.position.x + 1), Random.Range(transform.position.y + 3,transform.position.y + 6), transform.position.z), Quaternion.identity);
            }
            chestOpen = 1;
            PlayerPrefs.SetInt("Chest" + transform.name, chestOpen);
            openLight.SetActive(true);
        }
    }
}
