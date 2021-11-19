using UnityEngine.UI;
using UnityEngine;

public class WeaponPickThrow : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] Grab grabLeft, grabRight;
    [SerializeField] GameObject weaponPickDropButton;
    [SerializeField] Text weaponPickDropButtonText;
    [SerializeField] Rigidbody2D[] weapons;
    private Rigidbody2D closestWeapon;

    [Header("PickUp")]
    [SerializeField] float pickRange;
    public static bool isWeaponPicked;
    private bool isReturning, isDistCalcAllow = !isWeaponPicked;
    private Vector3 oldPosition;
    private float time = 0;

    [Header("Throw")]
    [SerializeField] float buttonActiveTime;
    [SerializeField] float throwForce;

    private void Update()
    {
        // Calcuate Distance Butween Player and Weapons
        if (isDistCalcAllow) CalculateDistance();

        // Fix the Position and rotation of (left & right grab) and this game object
        if (isWeaponPicked)
        {
            closestWeapon.transform.localPosition = Vector3.zero;
            closestWeapon.transform.localRotation = Quaternion.Euler(0,0,-90);
            grabLeft.transform.localPosition = new Vector3(0, -0.503f, 0);
            grabLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Pick Up Object
        if (isReturning && (time < 1))
        {
            closestWeapon.transform.position = ((1 - time) * (1 - time) * oldPosition) + (2 * (1 - time) * time * grabLeft.transform.position) + (time * time * grabLeft.transform.position);
            closestWeapon.transform.rotation = Quaternion.Slerp(closestWeapon.transform.rotation, grabLeft.transform.rotation, 0);
            time += Time.deltaTime;
        }
    }

    private void CalculateDistance()
    {
        float shortestDistance = Mathf.Infinity;
        closestWeapon = weapons[0];
        for (var i = 0; i < weapons.Length; i++)
        {
            float distance = Mathf.Abs(player.position.x - weapons[i].transform.position.x);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestWeapon = weapons[i];
            }
        }

        if (shortestDistance < pickRange) weaponPickDropButton.SetActive(true);
        else weaponPickDropButton.SetActive(false);
    }

    private void PickUp()
    {
        if (IsInvoking("DesableWeaponButton")) CancelInvoke("DesableWeaponButton");
        isWeaponPicked = true;
        isDistCalcAllow = false;

        time = 0;
        oldPosition = closestWeapon.transform.position;
        closestWeapon.transform.parent = grabLeft.transform;
        isReturning = true;
        grabLeft.AttachObject(closestWeapon);
        grabRight.AttachObject(closestWeapon);

        weaponPickDropButton.SetActive(true);
        weaponPickDropButtonText.text = "Drop";
    }

    private void Throw()
    {
        weaponPickDropButtonText.text = "Pick Up";
        isWeaponPicked = false;
        Invoke("DesableWeaponButton", buttonActiveTime);

        grabLeft.DeAttachObject();
        grabRight.DeAttachObject();
        closestWeapon.transform.parent = transform;

        closestWeapon.AddForce(new Vector3(PlayerMovement.weaponRotation, 0.8f, 0) * throwForce, ForceMode2D.Impulse);
    }

    private void DesableWeaponButton()
    {
        weaponPickDropButton.SetActive(false);
        isDistCalcAllow = true;
    }

    public void PickDropButton()
    {
        if (isWeaponPicked) Throw();
        else PickUp();
    }
}