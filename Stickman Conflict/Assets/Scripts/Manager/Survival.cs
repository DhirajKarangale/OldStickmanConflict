using System.Collections;
using UnityEngine;

public class Survival : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Dialogue[] introDialogues, byDialogue, spwanDialogues;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] spwanObjs;
    private static float spwanTime;
    private bool isByDialogueAllow;

    private void Start()
    {
        isByDialogueAllow = true;
        spwanTime = 20;
        StartCoroutine(ShowDialogue(introDialogues));
        StartCoroutine(SpwanEnemie());
    }

    private void Update()
    {
        if (PlayerHealth.isPlayerDye && isByDialogueAllow)
        {
            StartCoroutine(ShowDialogue(byDialogue));
            isByDialogueAllow = false;
        }
    }

    IEnumerator SpwanEnemie()
    {
        if (PlayerHealth.isPlayerDye)
        {
            yield break;
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(ShowDialogue(spwanDialogues));
        yield return new WaitForSeconds(3);

        AudioManager.instance.Play("Spawns");
        int swpanRate = Random.Range(0, enemies.Length);
        for (int i = 0; i < swpanRate + 1; i++)
        {
            GameObject currEnemy = Instantiate(enemies[swpanRate], EnemyPos(), Quaternion.identity);
            MoveNPC moveNPC = currEnemy.GetComponent<MoveNPC>();
            moveNPC.player = player;
            moveNPC.leftDist = currEnemy.transform.position.x - Random.Range(5, 20);
            moveNPC.rightDist = currEnemy.transform.position.x + Random.Range(5, 20);

            if (Random.value < 0.5f)
            {
                GameObject spwanObj = spwanObjs[Random.Range(0, spwanObjs.Length)];
                currEnemy.GetComponent<EnemyHealth>().spwanObj = spwanObj;
            }
        }

        yield return new WaitForSeconds(spwanTime);
        spwanTime -= 2;
        StartCoroutine(SpwanEnemie());
    }


    IEnumerator ShowDialogue(Dialogue[] dialogues)
    {
        yield return new WaitForSeconds(1);
        dialogueManager.StartDialogue(dialogues[Random.Range(0, dialogues.Length)].sentences);
        // StopCoroutine(ShowDialogue(introDialogues));
    }

    private Vector3 EnemyPos()
    {
        return new Vector3(Random.Range(player.position.x - 10, player.position.x + 10), Random.Range(-5, 10), 0);
    }
}