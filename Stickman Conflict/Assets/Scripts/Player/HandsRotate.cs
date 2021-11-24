using UnityEngine;
using EasyJoystick;

public class HandsRotate : MonoBehaviour
{
    [SerializeField] Joystick joystickHandRotate;
    [SerializeField] Balance balanceScript;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float offset;

    private void Update()
    {
        if ((playerHealth != null) && playerHealth.isPlayerDye) return;
        if ((joystickHandRotate.Horizontal() != 0) || (joystickHandRotate.Vertical() != 0))
        {
            balanceScript.enabled = false;
            rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, Mathf.Atan2(joystickHandRotate.Horizontal(), -joystickHandRotate.Vertical()) * Mathf.Rad2Deg + offset, 300 * Time.fixedDeltaTime));
        }
        else
        {
            balanceScript.enabled = true;
        }
    }
}
