using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    public EnemyHealth enemyHealth;
    [SerializeField] float force;
    [SerializeField] float targetRotation;

    private void Update()
    {
        if ((PlayerHealth.isPlayerDye && this.transform.parent.name == "Player") || (enemyHealth && (enemyHealth.currState == EnemyHealth.State.Dead)))
        {
            enabled = false;
            return;
        }
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
}
