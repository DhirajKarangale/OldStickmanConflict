using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
          SaveManager.instance.saveData.playerSpwanPos[0] = this.transform.position.x; 
          SaveManager.instance.saveData.playerSpwanPos[1] = this.transform.position.y + 10; 
          SaveManager.instance.saveData.playerSpwanPos[2] = this.transform.position.z; 

          SaveManager.instance.Save();
        }
    }
}
