using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] Vector3 leftPos, rightPos;
    private Vector3 currleftPos, currrightPos;
    [SerializeField] Vector3 pos1, pos2;
    private Vector3 nextPos;

    private void Start()
    {
        nextPos = pos1;
        currleftPos = leftPos;
        currrightPos = rightPos;
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

        if ((player.position.x > currleftPos.x) && (player.position.x < currrightPos.x))
        {
            player.parent = this.transform;
        }
        else
        {
            player.parent = null;
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == 7)
    //     {
    //         player.parent = this.transform;
    //     }
    // }

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

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(leftPos, 1);
        Gizmos.DrawSphere(rightPos, 1);
    }
}