using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] Vector3 pos1, pos2;
    private Vector3 nextPos;

    private void Start()
    {
        nextPos = pos1;
    }

    private void Update()
    {
        if (transform.position == pos1)
        {
            nextPos = pos2;
        }
        if (transform.position == pos2)
        {
            nextPos = pos1;
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

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == 7)
    //     {
    //         player.parent = null;
    //     }
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 1);
        Gizmos.DrawSphere(pos2, 1);
    }
}