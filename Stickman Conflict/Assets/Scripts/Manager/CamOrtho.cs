using UnityEngine;

public class CamOrtho : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            CamManager.Instance.OrthoSize(30);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            CamManager.Instance.ReserOrtho();
        }
    }
}