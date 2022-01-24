using UnityEngine;

public class BombThrow : MonoBehaviour
{
    [SerializeField] float throwForce = 20;
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject bombButton;
    [SerializeField] UnityEngine.UI.Text countText;

    private void Update()
    {
        if (GameSave.instance.gameData.bomb > 0) bombButton.SetActive(true);
        else bombButton.SetActive(false);
        countText.text = GameSave.instance.gameData.bomb.ToString();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBomb();
        }
    }

    public void ThrowBomb()
    {
        GameObject currBomb = Instantiate(bomb, transform.position, transform.rotation);
     
        // currBomb.GetComponent<Rigidbody2D>().
        // AddForce(new Vector2((Mathf.Clamp(handRotateJoystick.Horizontal(), -1, 1)), Mathf.Clamp(handRotateJoystick.Vertical(), -1, 1)) * throwForce, ForceMode2D.Impulse);
     
        currBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(handRotateJoystick.Horizontal(), handRotateJoystick.Vertical()) * throwForce, ForceMode2D.Impulse);
        GameSave.instance.gameData.bomb--;
        return;
    }
}