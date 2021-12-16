using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;
    [SerializeField] float force;
    [SerializeField] float targetRotation;

    private void Update()
    {
        if ((playerHealth && playerHealth.isPlayerDye) || (enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead)))
        {
            enabled = false;
            return;
        }
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
