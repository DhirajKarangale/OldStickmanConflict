using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float force;
    [SerializeField] float targetRotation;

    private void Update()
    {
        if ((playerHealth != null) && playerHealth.isPlayerDye) return;
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
