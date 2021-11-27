using UnityEngine;

public class FirstTalk : MonoBehaviour
{
    private int firstTalk;
    [SerializeField] PlayerMovement player;
    [SerializeField] NPCDialogue npcDialogue;
    [SerializeField] Dialogue dialogue;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameObject powersGetEffect;
    [SerializeField] GameObject controlCanvas;
    [SerializeField] GameObject helmet, sward;
    [SerializeField] SpriteRenderer leftHand, rightHand;
    [SerializeField] Color oldHandColor, powerHandColor;
    private bool isStartTalkAllow;

    private void Start()
    {
        //PlayerPrefs.DeleteKey("FirstTalk");
        firstTalk = PlayerPrefs.GetInt("FirstTalk", 0);
        if (firstTalk == 0)
        {
            npcDialogue.enabled = false;
            isStartTalkAllow = true;
            helmet.SetActive(false);
            sward.SetActive(false);
            leftHand.color = oldHandColor;
            rightHand.color = oldHandColor;
        }
        else 
        {
            Destroy(gameObject);
            GivePowers();
        }
    }

    private void Update()
    {
        if ((firstTalk == 0) && (transform.position.x - player.transform.position.x) <= 0)
        {
            if (dialogueManager.isEndDialogue) TalkEnd();
            else
            {
                if (isStartTalkAllow)
                {
                    TalkStart();
                }
            }
        }
    }

    private void TalkStart()
    {
        isStartTalkAllow = false;
        dialogueManager.StartDialogue(dialogue.sentences);
        player.moveJoystick.MouseUp();
        controlCanvas.SetActive(false);
        player.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    private void TalkEnd()
    {
        firstTalk = 1;
        PlayerPrefs.SetInt("FirstTalk", firstTalk);
        GivePowers();
        controlCanvas.SetActive(true);
        Destroy(Instantiate(powersGetEffect, player.transform.position, Quaternion.identity), 10);
        player.rigidBody.constraints = RigidbodyConstraints2D.None;
        Invoke("EnableDialogue", 45);
        Destroy(this.gameObject, 50);
    }

    private void GivePowers()
    {
        helmet.SetActive(true);
        sward.SetActive(true);
        leftHand.color = powerHandColor;
        rightHand.color = powerHandColor;
    }

    private void EnableDialogue()
    {
        npcDialogue.enabled = true;
    }
}
