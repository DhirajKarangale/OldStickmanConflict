using UnityEngine;

public class BombThrow : MonoBehaviour
{
    [SerializeField] float throwForce = 20;
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject bombButton;

    private void Update()
    {
        if (SaveManager.instance.saveData.bomb > 0) bombButton.SetActive(true);
        else bombButton.SetActive(false);

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBomb();
        }
    }

    public void ThrowBomb()
    {
        GameObject currBomb = Instantiate(bomb, transform.position, transform.rotation);
        currBomb.GetComponent<Rigidbody2D>().
        AddForce(new Vector2((Mathf.Clamp(handRotateJoystick.Horizontal(), -1, 1)), Mathf.Clamp(handRotateJoystick.Vertical(), -1, 1)) * throwForce, ForceMode2D.Impulse);
        SaveManager.instance.saveData.bomb--;
        return;
    }
}