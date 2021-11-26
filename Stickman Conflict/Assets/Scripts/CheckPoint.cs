using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Transform[] weapons;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            SaveManager.instance.saveData.playerSpwanPos[0] = this.transform.position.x;
            SaveManager.instance.saveData.playerSpwanPos[1] = this.transform.position.y + 10;
            SaveManager.instance.saveData.playerSpwanPos[2] = this.transform.position.z;

            for (int i = 0; i < weapons.Length; i++)
            {
                SaveManager.instance.saveData.weaponsPos[i][0] = weapons[i].transform.position.x;
                SaveManager.instance.saveData.weaponsPos[i][1] = weapons[i].transform.position.y;
                SaveManager.instance.saveData.weaponsPos[i][2] = weapons[i].transform.position.z;
            }

            SaveManager.instance.Save();
        }
    }
}
