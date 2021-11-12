using UnityEngine.UI;
using UnityEngine;

public class WeaponPickDrop : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] GameObject weaponPickDropButton;
    [SerializeField] Text weaponPickDropButtonText;

    [Header("Pick")]
    [SerializeField] Transform weaponManager;
    [SerializeField] Grab grabLeft, grabRight;
    [SerializeField] float pickUpRange, pickRotation;
    private float time = 0;
    public bool isWeaponPicked; // Static
    private bool isDisCalcAllow = true;

    [Header("Throw")]
    [SerializeField] float buttonActiveTime;
    [SerializeField] float throwForce;
    private float currentButtonActiveTime;
    private Vector3 oldPosition;
    private bool isReturning, isButtonDisableAllow;

    private void Start()
    {
        currentButtonActiveTime = buttonActiveTime;
    }

    private void Update()
    {
        // Fix the Position and rotation of (left & right grab) and this game object
        if (isWeaponPicked)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(PlayerMovement.weaponRotation);
            grabLeft.transform.localPosition = new Vector3(0, -0.503f, 0);
            grabLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Calculate Distance between player and this object
        if (isDisCalcAllow)
        {
            if (Mathf.Abs(player.position.x - transform.position.x) <= pickUpRange) weaponPickDropButton.SetActive(true);
            else weaponPickDropButton.SetActive(false);
        }

        // Pick Up Object
        if (isReturning)
        {
            if (time < 1)
            {
                transform.position = ReturnCurve(time, oldPosition, grabLeft.transform.position, grabLeft.transform.position);
                transform.rotation = Quaternion.Slerp(rigidBody.transform.rotation, grabLeft.transform.rotation, pickRotation * Time.deltaTime);
                time += Time.deltaTime;
            }
        }

        // // Desable Button after some time
        if (isButtonDisableAllow)
        {
            if (currentButtonActiveTime <= 0) DesableWeaponButton();
            else currentButtonActiveTime -= Time.deltaTime;
        }
    }

    private void PickUp()
    {
        isWeaponPicked = true;
        isDisCalcAllow = false;
        isButtonDisableAllow = false;
        currentButtonActiveTime = buttonActiveTime;

        transform.parent = grabLeft.transform;
        time = 0;
        oldPosition = transform.position;
        isReturning = true;
        rigidBody.velocity = Vector3.zero;

        grabLeft.AttachObject(rigidBody);
        grabRight.AttachObject(rigidBody);

        weaponPickDropButton.SetActive(true);
        weaponPickDropButtonText.text = "Drop";
    }

    private void Throw()
    {
        weaponPickDropButtonText.text = "Pick Up";
        currentButtonActiveTime = buttonActiveTime;
        isWeaponPicked = false;
        isButtonDisableAllow = true;

        grabLeft.DeAttachObject();
        grabRight.DeAttachObject();
        transform.parent = weaponManager;

        rigidBody.AddForce((transform.forward + new Vector3(-Mathf.Sign(PlayerMovement.weaponRotation.z), 0.25f, 0)) * throwForce, ForceMode2D.Impulse);
    }

    private void DesableWeaponButton()
    {
        currentButtonActiveTime = buttonActiveTime;
        weaponPickDropButton.SetActive(false);
        isDisCalcAllow = true;
    }

    private Vector3 ReturnCurve(float time, Vector3 oldPos, Vector3 curvePoint, Vector3 conTainer)
    {
        float u = 1 - time;
        float tt = time * time;
        float uu = u * u;
        return (uu * oldPos) + (2 * u * time * curvePoint) + (tt * conTainer);
    }

    public void PickDropButton()
    {
        if (isWeaponPicked) Throw();
        else PickUp();
    }
}
