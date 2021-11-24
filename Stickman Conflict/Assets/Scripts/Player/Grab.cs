using UnityEngine;
using EasyJoystick;

public class Grab : MonoBehaviour
{
    [SerializeField] Joystick joystickHandRotate;
    [SerializeField] PlayerHealth playerHealth;

    private void Update()
    {
        if ((playerHealth != null) && playerHealth.isPlayerDye) return;
        if ((!((joystickHandRotate.Horizontal() != 0) || (joystickHandRotate.Vertical() != 0))))
        {
            DeAttachObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((playerHealth != null) && playerHealth.isPlayerDye) return;
        if (WeaponPickThrow.isWeaponPicked) return;

        if (((joystickHandRotate.Horizontal() != 0) || (joystickHandRotate.Vertical() != 0)))
        {
            Rigidbody2D rigidbody2D = collision.transform.GetComponent<Rigidbody2D>();
            AttachObject(rigidbody2D);
        }
    }

    public void DeAttachObject()
    {
        Destroy(GetComponent<FixedJoint2D>());
    }

    public void AttachObject(Rigidbody2D rigidbody2D)
    {
        if (rigidbody2D != null)
        {
            FixedJoint2D fixedJoint2D = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            fixedJoint2D.connectedBody = rigidbody2D;
        }
        else
        {
            FixedJoint2D fixedJoint2D = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
        }
    }
}
