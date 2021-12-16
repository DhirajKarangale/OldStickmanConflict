using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackRate, smooth;
    [SerializeField] Transform grab, weapon;
    [SerializeField] Transform armUp, armlow;
    private float timeToAttack;
    private Vector3 targetAngles;

    private void Update()
    {
        weapon.localPosition = Vector3.zero;
        weapon.localRotation = Quaternion.Euler(0, 0, -90);
        grab.localPosition = new Vector3(0, -0.503f, 0);
        grab.localRotation = Quaternion.Euler(0, 0, 0);

        if (Time.time > timeToAttack)
        {
            timeToAttack = Time.time + 1 / attackRate;
            Attack();
        }
    }

    public void Attack()
    {
        Rotate(armUp);
        Rotate(armlow);
    }

    private void Rotate(Transform arm)
    {
        targetAngles = arm.eulerAngles + 180 * Vector3.up;
        arm.eulerAngles = Vector3.Lerp(arm.eulerAngles, targetAngles, smooth * Time.deltaTime);
    }
}
