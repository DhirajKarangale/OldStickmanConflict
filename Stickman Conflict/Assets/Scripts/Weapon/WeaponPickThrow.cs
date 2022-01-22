using UnityEngine.UI;
using UnityEngine;

public class WeaponPickThrow : MonoBehaviour
{
    public static WeaponPickThrow instance = null;
    public static WeaponPickThrow Instance
    {
        get { return instance; }
    }

    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    [SerializeField] Grab grabLeft;
    [SerializeField] Button pickDropButton;
    [SerializeField] Sprite pick, drop;
    [SerializeField] Rigidbody2D[] weapons;
    private Rigidbody2D closestWeapon;
    private float weaponTimer = 1;

    [Header("PickUp")]
    [SerializeField] float pickRange;
    public static bool isWeaponPicked;
    private bool isReturning, isDistCalcAllow = !isWeaponPicked;
    private Vector3 oldPosition;
    private float time = 0;

    [Header("Throw")]
    [SerializeField] float buttonActiveTime;
    [SerializeField] float throwForce;

    private void Start()
    {
        instance = this;
        isWeaponPicked = false;
        SaveManager.instance.saveData.weaponsPosition = new float[weapons.Length, 2];
        SetWeaponOldPos();
    }


    private void Update()
    {
        if (PlayerHealth.isPlayerDye)
        {
            if (!isWeaponPicked) Throw();
            return;
        }

        // Calcuate Distance Butween Player and Weapons
        if (isDistCalcAllow) CalculateDistance();

        // Fix the Position and rotation of (left & right grab) and this game object
        if (isWeaponPicked) FixWeaponPos();

        // Pick Up Object
        if (isReturning && (time < 1))
        {
            closestWeapon.transform.position = ((1 - time) * (1 - time) * oldPosition) + (2 * (1 - time) * time * grabLeft.transform.position) + (time * time * grabLeft.transform.position);
            closestWeapon.transform.rotation = Quaternion.Slerp(closestWeapon.transform.rotation, grabLeft.transform.rotation, 0);
            time += Time.deltaTime;
        }
    }

    private void FixWeaponPos()
    {
        grabLeft.transform.localPosition = new Vector3(0, -0.503f, 0);
        closestWeapon.transform.localPosition = Vector3.zero;
        closestWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90);

        if ((handRotateJoystick.Vertical() != 0) || (handRotateJoystick.Horizontal() != 0))
        {
            grabLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weaponTimer = 1;
        }
        else
        {
            if (weaponTimer > 0)
            {
                grabLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
                weaponTimer -= Time.deltaTime;
            }
            else
            {
                grabLeft.rigidBody.angularVelocity = 0.5f;
            }
        }
    }

    private void SetWeaponOldPos()
    {
        if (SaveManager.instance.saveData.weaponsPosition.Length >= weapons.Length)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (SaveManager.instance.saveData.pickedWeaponName == weapons[i].name)
                {
                    closestWeapon = weapons[i];
                    PickUp();
                }
                else
                {
                    weapons[i].transform.position = new Vector3(SaveManager.instance.saveData.weaponsPosition[i, 0], SaveManager.instance.saveData.weaponsPosition[i, 1], 0);
                }
            }
        }
    }

    private void CalculateDistance()
    {
        float shortestDistance = Mathf.Infinity;
        closestWeapon = weapons[0];
        for (var i = 0; i < weapons.Length; i++)
        {
            float distance = Vector2.Distance(player.position, weapons[i].transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestWeapon = weapons[i];
            }
        }

        if (shortestDistance < pickRange) pickDropButton.gameObject.SetActive(true);
        else pickDropButton.gameObject.SetActive(false);
    }


    private void PickUp()
    {
        AudioManager.instance.Play("Pick");
        if (IsInvoking("DesableWeaponButton")) CancelInvoke("DesableWeaponButton");
        isWeaponPicked = true;
        isDistCalcAllow = false;

        time = 0;
        oldPosition = closestWeapon.transform.position;
        isReturning = true;
        closestWeapon.transform.parent = grabLeft.transform;
        grabLeft.AttachObject(closestWeapon);

        pickDropButton.gameObject.SetActive(true);
        pickDropButton.image.sprite = drop;

        SaveManager.instance.saveData.pickedWeaponName = closestWeapon.name;
    }

    private void Throw()
    {
        AudioManager.instance.Play("Throw");
        pickDropButton.image.sprite = pick;
        isWeaponPicked = false;
        Invoke("DesableWeaponButton", buttonActiveTime);

        grabLeft.DeAttachObject();
        closestWeapon.transform.parent = transform;

        closestWeapon.AddForce(new Vector2((Mathf.Clamp(handRotateJoystick.Horizontal(), -1, 1)), Mathf.Clamp(handRotateJoystick.Vertical(), -1, 1)) * throwForce, ForceMode2D.Impulse);

        SaveManager.instance.saveData.pickedWeaponName = null;
    }

    private void DesableWeaponButton()
    {
        pickDropButton.gameObject.SetActive(false);
        isDistCalcAllow = true;
    }

    public void PickDropButton()
    {
        if (isWeaponPicked) Throw();
        else PickUp();
    }
}