using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform[] weapons;
    private bool isDataSaveAllow = true;
    public static event Action onCheckPointCross;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PointCross" + transform.name, 0) == 0) animator.Play("Close");
        else animator.Play("Open");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && isDataSaveAllow)
        {
            isDataSaveAllow = false;

            SaveManager.instance.saveData.playerSpwanPos[0] = this.transform.position.x;
            SaveManager.instance.saveData.playerSpwanPos[1] = this.transform.position.y + 10;
            SaveManager.instance.saveData.playerSpwanPos[2] = this.transform.position.z;

            for (int i = 0; i < weapons.Length; i++)
            {
                SaveManager.instance.saveData.weaponsPosition = new float[weapons.Length, 3];

                SaveManager.instance.saveData.weaponsPosition[i, 0] = weapons[i].position.x;
                SaveManager.instance.saveData.weaponsPosition[i, 1] = weapons[i].position.y;
                SaveManager.instance.saveData.weaponsPosition[i, 2] = weapons[i].position.z;
            }

            SaveManager.instance.Save();
            Invoke("ActiveDataSave", 10);
            if (PlayerPrefs.GetInt("PointCross" + transform.name, 0) == 0) animator.Play("Cross");
            onCheckPointCross();
            PlayerPrefs.SetInt("PointCross" + transform.name, 1);
        }
    }

    private void ActiveDataSave()
    {
        isDataSaveAllow = true;
    }
}