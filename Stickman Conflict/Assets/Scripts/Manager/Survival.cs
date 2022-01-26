using System.Collections;
using UnityEngine;

public class Survival : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] introDialogues, spwanDialogues;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] spwanObjs;
    private static float spwanTime;

    private void Start()
    {
        spwanTime = 20;
        Invoke("IntroDialogue", 1);
        StartCoroutine(SpwanEnemie());
    }

    private void IntroDialogue()
    {
        dialogueManager.StartDialogue(introDialogues[Random.Range(0, introDialogues.Length)].sentences);
    }

    private Vector3 EnemyPos()
    {
        return new Vector3(Random.Range(player.position.x - 10, player.position.x + 10), Random.Range(-5, 10), 0);
    }

    IEnumerator SpwanEnemie()
    {
        if (PlayerHealth.isPlayerDye)
        {
            this.enabled = false;
            yield break;
        }

        yield return new WaitForSeconds(5);
        dialogueManager.StartDialogue(spwanDialogues[Random.Range(0, spwanDialogues.Length)].sentences);
        AudioManager.instance.Play("Spawns");
        int swpanRate = Random.Range(0, enemies.Length);
        for (int i = 0; i < swpanRate + 1; i++)
        {
            GameObject currEnemy = Instantiate(enemies[swpanRate], EnemyPos(), Quaternion.identity);
            MoveNPC moveNPC = currEnemy.GetComponent<MoveNPC>();
            moveNPC.player = player;
            moveNPC.leftDist = currEnemy.transform.position.x - Random.Range(5, 20);
            moveNPC.rightDist = currEnemy.transform.position.x + Random.Range(5, 20);

            if (Random.value < 0.8f)
            {
                GameObject spwanObj = spwanObjs[Random.Range(0, spwanObjs.Length)];
                currEnemy.GetComponent<EnemyHealth>().spwanObj = spwanObj;
            }
        }

        yield return new WaitForSeconds(spwanTime);
        spwanTime -= 2;
        StartCoroutine(SpwanEnemie());
    }
}