using UnityEngine;
using System.Collections.Generic;

public class ActiveObj : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist;
    [SerializeField] List<Transform> Objects = new List<Transform>();

    private void Update()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i] != null)
            {
                if (Mathf.Abs(player.position.x - Objects[i].position.x) < dist) Objects[i].gameObject.SetActive(true);
                else Objects[i].gameObject.SetActive(false);
            }
            else
            {
                Objects.RemoveAt(i);
            }
        }
    }

}
