using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isDataSaveAllow = true;
    public static event Action onCheckPointCross;

    private void Awake()
    {
        // PlayerPrefs.DeleteKey("PointCross" + transform.name);
     
        if (PlayerPrefs.GetInt("PointCross" + transform.name, 0) == 0) animator.Play("Close");
        else animator.Play("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerHealth.isPlayerDye) return;

        if (collision.gameObject.layer == 7 && isDataSaveAllow)
        {
            AudioManager.instance.Play("Button");
            isDataSaveAllow = false;

            GameSave.instance.gameData.playerSpwanPos[0] = this.transform.position.x;
            GameSave.instance.gameData.playerSpwanPos[1] = this.transform.position.y + 10;
            for (int i = 0; i < WeaponPickThrow.instance.weapons.Length; i++)
            {
                GameSave.instance.gameData.weaponsPosition[i, 0] = WeaponPickThrow.instance.weapons[i].position.x;
                GameSave.instance.gameData.weaponsPosition[i, 1] = WeaponPickThrow.instance.weapons[i].position.y;
            }
            GlobalSave.instance.globalData.stone = GameSave.instance.gameData.stone;
            GlobalSave.instance.globalData.level = (byte)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            GlobalSave.instance.globalData.coin = GameSave.instance.gameData.coin;
            GlobalSave.instance.Save();

            if (PlayerPrefs.GetInt("PointCross" + transform.name, 0) == 0) animator.Play("Cross");
            PlayerPrefs.SetInt("PointCross" + transform.name, 1);

            GameSave.instance.Save();
            onCheckPointCross();
            Invoke("ActiveDataSave", 10);
        }
    }

    private void ActiveDataSave()
    {
        isDataSaveAllow = true;
    }
}