using UnityEngine;

public class GateMachine : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Animator GateAnimator;
    private bool isDialogueAllow = true;
    private string[] keyNotFoundDialogue = new string[2];
    private string[] keyFoundDialogue = new string[2];
    private int open;

    private void Start()
    {
        // PlayerPrefs.DeleteKey("GateMachine" + transform.name);
        keyNotFoundDialogue[0] = "Machine : Need a Key to open route.";
        keyNotFoundDialogue[1] = "Machine : Find a key behind this then come.";

        keyFoundDialogue[0] = "Machine : Key found. You'r Welcome.....";
        keyFoundDialogue[1] = "Machine : Wait to open way, Enjoy your adventure Gamerz !!!";

        CheckPoint.onCheckPointCross += OnCheckPointCross;
        open = PlayerPrefs.GetInt("GateMachine" + transform.name, 0);

        if (open == 0)
        {
            GateAnimator.Play("Close");
        }
        else
        {
            GateAnimator.Play("Open");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerHealth.isPlayerDye && (open == 1))
        {
            this.enabled = false;
            return;
        }

        if ((collision.gameObject.layer == 7) && (open == 0))
        {
            if (GameSaveManager.instance.saveData.key > 0)
            {
                AudioManager.instance.Play("Coin");
                dialogueManager.StartDialogue(keyFoundDialogue);
                GameSaveManager.instance.saveData.key--;
                GateAnimator.Play("Play");
                open = 1;
            }
            else
            {
                if (isDialogueAllow)
                {
                    dialogueManager.StartDialogue(keyNotFoundDialogue);
                    isDialogueAllow = false;
                }
            }
            if (!IsInvoking("EndDialogue")) Invoke("EndDialogue", 5);
        }
    }

    private void OnDestroy()
    {
        CheckPoint.onCheckPointCross -= OnCheckPointCross;
    }

    private void EndDialogue()
    {
        dialogueManager.EndDialogue();
        isDialogueAllow = true;
    }

    private void OnCheckPointCross()
    {
        PlayerPrefs.SetInt("GateMachine" + transform.name, open);
    }
}