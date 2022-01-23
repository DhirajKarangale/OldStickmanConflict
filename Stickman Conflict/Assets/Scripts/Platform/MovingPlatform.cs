using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] float leftStandPos, rightStandPos;
    [SerializeField] Vector3 pos1, pos2;
    private Vector3 nextPos;
    [SerializeField] AudioSource movingPlatform;

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

        if ((player.position.x >= (transform.position.x + leftStandPos)) && (player.position.x <= (transform.position.x + rightStandPos)))
        {
            if (!movingPlatform.isPlaying) movingPlatform.Play();
            player.SetParent(transform);
        }
        else
        {
            player.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 1);
        Gizmos.DrawSphere(pos2, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(transform.position.x + leftStandPos, transform.position.y, 0), 1);
        Gizmos.DrawSphere(new Vector3(transform.position.x + rightStandPos, transform.position.y, 0), 1);
    }

}