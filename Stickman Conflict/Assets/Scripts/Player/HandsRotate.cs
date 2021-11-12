using UnityEngine;
using EasyJoystick;

public class HandsRotate : MonoBehaviour
{
    [SerializeField] Joystick joystickHandRotate;
    [SerializeField] Balance balanceScript;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float offset;

    private void Update()
    {
        if ((joystickHandRotate.Horizontal() != 0) || (joystickHandRotate.Vertical() != 0))
        {
            balanceScript.enabled = false;
            float rotationZ = Mathf.Atan2(joystickHandRotate.Horizontal(), -joystickHandRotate.Vertical()) * Mathf.Rad2Deg;
            rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, rotationZ + offset, 300 * Time.fixedDeltaTime));
        }
        else
        {
            balanceScript.enabled = true;
        }
    }
}
