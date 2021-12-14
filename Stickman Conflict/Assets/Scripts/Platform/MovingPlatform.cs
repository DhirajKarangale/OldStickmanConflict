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

        if ((player.position.x > (transform.localPosition.x + leftStandPos)) && (player.position.x < (transform.localPosition.x + rightStandPos)))
        {
            if (!movingPlatform.isPlaying) movingPlatform.Play();
            player.parent = this.transform;
        }
        else
        {
            player.parent = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 1);
        Gizmos.DrawSphere(pos2, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(transform.localPosition.x + leftStandPos, transform.localPosition.y, 0), 1);
        Gizmos.DrawSphere(new Vector3(transform.localPosition.x + rightStandPos, transform.localPosition.y, 0), 1);
    }
}