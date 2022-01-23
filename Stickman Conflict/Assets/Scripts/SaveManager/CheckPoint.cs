using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform[] weapons;
    private int oldCoin;
    private byte oldKey, oldPalak, oldBomb;
    private bool isDataSaveAllow = true;
    public static event Action onCheckPointCross;

    private void Awake()
    {
        //  PlayerPrefs.DeleteKey("PointCross" + transform.name);
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

            GameSaveManager.instance.saveData.playerSpwanPos[0] = this.transform.position.x;
            GameSaveManager.instance.saveData.playerSpwanPos[1] = this.transform.position.y + 10;

            for (int i = 0; i < weapons.Length; i++)
            {
                GameSaveManager.instance.saveData.weaponsPosition[i, 0] = weapons[i].position.x;
                GameSaveManager.instance.saveData.weaponsPosition[i, 1] = weapons[i].position.y;
            }

            oldCoin = GameSaveManager.instance.saveData.coin;
            oldKey = GameSaveManager.instance.saveData.key;
            oldPalak = GameSaveManager.instance.saveData.palakCount;
            oldBomb = GameSaveManager.instance.saveData.bomb;

            if (PlayerPrefs.GetInt("PointCross" + transform.name, 0) == 0) animator.Play("Cross");
            PlayerPrefs.SetInt("PointCross" + transform.name, 1);
            GameSaveManager.instance.Save();
            onCheckPointCross();
            Invoke("ActiveDataSave", 10);
        }
    }

    private void ActiveDataSave()
    {
        isDataSaveAllow = true;
    }
}