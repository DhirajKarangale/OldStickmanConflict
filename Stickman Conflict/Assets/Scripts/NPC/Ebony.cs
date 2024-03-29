using System.Collections;
using UnityEngine;

public class Ebony : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] GameObject stone;
    [SerializeField] string[] introDialogue, midDialogue;
    private bool isIntroAllow = true;
    private bool isMidDialogueAllow = true;
    private float spwanAmount = 3;

    [Header("Weapons")]
    [SerializeField] GameObject rocks;
    [SerializeField] float rockForce;
    [SerializeField] GameObject goblin;
    [SerializeField] GameObject spikeFace;
    [SerializeField] GameObject palak, bombCaret;
    private int rockDir = 90;

    private void Update()
    {
        if (PlayerHealth.isPlayerDye)
        {
            this.enabled = false;
            return;
        }

        if (enemyHealth.currState == EnemyHealth.State.Dead)
        {
            if (GameSave.instance.gameData.stone < 1)
            {
                Instantiate(stone, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            }

            this.enabled = false;
            return;
        }

        if (player.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1.55f, 1.3f, 1);
            rockDir = 90;
            if (isIntroAllow) StartCoroutine(Intro());
        }
        else
        {
            transform.localScale = new Vector3(1.55f, 1.3f, 1);
            rockDir = -90;
        }
    }

    IEnumerator Intro()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        if (dist < 30)
        {
            dialogueManager.StartDialogue(introDialogue);
            isIntroAllow = false;

            yield return new WaitForSeconds(2);
            StartCoroutine(RockAttack());

            yield return new WaitForSeconds(10);
            StartCoroutine(SpwanEen(spikeFace));
        }
    }

    private Vector3 Pos()
    {
        return new Vector3(Random.Range(transform.position.x - 10, transform.position.x - 2),
        Random.Range(transform.position.y - 1, transform.position.y + 8), 0);
    }

    IEnumerator RockAttack()
    {
        if (PlayerHealth.isPlayerDye || enemyHealth.currState == EnemyHealth.State.Dead)
        {
            yield break;
        }
        animator.Play("Attack");
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            GameObject currRock = Instantiate(rocks, Pos() - new Vector3(0, 2, 0), Quaternion.Euler(0, 0, rockDir));
            Vector2 forceDir = new Vector2(player.position.x - transform.position.x, 0);
            forceDir = forceDir.normalized;
            currRock.GetComponent<Rigidbody2D>().AddForce(forceDir * rockForce, ForceMode2D.Impulse);
            Destroy(currRock, 5);
        }

        yield return new WaitForSeconds(8);
        StartCoroutine(RockAttack());
    }

    IEnumerator SpwanEen(GameObject spwan)
    {
        if (PlayerHealth.isPlayerDye || enemyHealth.currState == EnemyHealth.State.Dead)
        {
            yield break;
        }
        animator.Play("Attack");
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.Play("Spawns");
        for (int i = 0; i < spwanAmount; i++)
        {
            GameObject currEnemy = Instantiate(spwan, Pos(), Quaternion.identity);
            MoveNPC moveNPC = currEnemy.GetComponent<MoveNPC>();
            moveNPC.player = player;
            moveNPC.leftDist = transform.position.x - Random.Range(20, 50);
            moveNPC.rightDist = transform.position.x - Random.Range(5, 10);

            if (Random.value < 0.35f)
            {
                GameObject spwanObj;
                if (Random.value < 0.25f) spwanObj = bombCaret;
                else spwanObj = palak;
                currEnemy.GetComponent<EnemyHealth>().spwanObj = spwanObj;
            }
        }

        GameObject enemy;
        if (enemyHealth.currHealth < 200)
        {
            if (enemyHealth.currHealth < 50)
            {
                spwanAmount = 6;
                enemy = goblin;
            }
            else if (enemyHealth.currHealth < 100)
            {
                spwanAmount = 5;
                enemy = goblin;
            }
            else if (enemyHealth.currHealth < 150)
            {
                spwanAmount = 4;
                if (Random.value < 0.5) enemy = goblin;
                else enemy = spikeFace;
            }
            else
            {
                if (Random.value < 0.5) enemy = goblin;
                else enemy = spikeFace;
            }
            if (isMidDialogueAllow) dialogueManager.StartDialogue(midDialogue);
        }
        else enemy = spikeFace;
        yield return new WaitForSeconds(25);
        StartCoroutine(SpwanEen(enemy));
    }
}