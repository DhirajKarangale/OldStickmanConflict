using UnityEngine;

public class ActDeactNPC : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] Transform[] NPCs;

    private void Update()
    {
        for(int i =0;i<NPCs.Length;i++)
        {
            if(Mathf.Abs(player.position.x - NPCs[i].position.x) < dist) NPCs[i].gameObject.SetActive(true);
            else NPCs[i].gameObject.SetActive(false);
        }
    }
}
