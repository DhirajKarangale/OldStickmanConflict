using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float force;
    [SerializeField] float targetRotation;

    private void Update()
    {
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
