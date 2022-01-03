using UnityEngine;
using EasyJoystick;

public class Rope : MonoBehaviour
{
    [SerializeField] Rigidbody2D origin;
    [SerializeField] LineRenderer line;
    [SerializeField] Joystick joystickHandRotate;
    private Vector3 velocity;
    private bool isPullRope = false;
    private bool isRopeActive = false;
    private Rigidbody2D objectToPull;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActiveRope();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            DeactiveRope();
        }

        if (!isRopeActive) return;

        if (isPullRope)
        {
            float pullForce = 1500; // 1500 is pullForce
            Vector2 pullDirection = (Vector2)transform.position - origin.position;
            pullDirection = pullDirection.normalized;
            if (objectToPull != origin)
            {
                pullForce = -30; // -pullForce/50
                transform.position = objectToPull.position;

                if (Mathf.Abs(transform.position.x - origin.position.x) < 2.3f)
                {
                    objectToPull.velocity = Vector2.zero;
                    DeactiveRope();
                    return;
                }
            }
            objectToPull.AddForce(pullDirection * pullForce);
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            if (distance > 50)
            {
                DeactiveRope();
                return;
            }
        }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        velocity = Vector3.zero;
        Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody && (rigidBody.mass < 1.5f)) objectToPull = rigidBody;
        else objectToPull = origin;

        isPullRope = true;
    }

    private void SetRope(Vector2 targetPos)
    {
        Vector2 throughDirection = targetPos;
        throughDirection = throughDirection.normalized;
        velocity = throughDirection * 75; // 75 is speed
        transform.position = origin.position + throughDirection;
        isPullRope = false;
        isRopeActive = true;
    }

    public void ActiveRope()
    {
        Vector2 joystickPos = new Vector2(joystickHandRotate.Horizontal(), joystickHandRotate.Vertical());
        if (joystickPos == Vector2.zero) return;
        SetRope(joystickPos * 100);
    }

    public void DeactiveRope()
    {
        isRopeActive = false;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }
}