using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum State { Move, Hurt, Dead }
    public State currState;

    [SerializeField] MoveNPC moveNPC;
    [SerializeField] EnemyHealth enemyHealth;

    private void Update()
    {
        switch (currState)
        {
            case State.Move:
                if (moveNPC) moveNPC.Move();
                break;
            case State.Hurt:
                if (enemyHealth) enemyHealth.Hurt();
                break;
            case State.Dead:
                if (currState != State.Dead)
                    if (enemyHealth) enemyHealth.Dead();
                break;
        }
    }
}