using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed;
    private Vector3 nextPos;

    private void Start()
    {
        nextPos = pos1.position;
    }

    private void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.parent = null;
        }
    }
}