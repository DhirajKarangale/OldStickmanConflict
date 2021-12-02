using UnityEngine;
using System.Collections.Generic;

public class ActiveNPC : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] List<Transform> NPC = new List<Transform>();

    private void Update()
    {
        for (int i = 0; i < NPC.Count; i++)
        {
            if (NPC[i] != null)
            {
                if (Mathf.Abs(player.position.x - NPC[i].position.x) < dist) NPC[i].gameObject.SetActive(true);
                else NPC[i].gameObject.SetActive(false);
            }
            else
            {
                NPC.RemoveAt(i);
            }
        }
    }
}
