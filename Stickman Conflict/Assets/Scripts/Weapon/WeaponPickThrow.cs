using UnityEngine.UI;
using UnityEngine;

public class WeaponPickThrow : MonoBehaviour
{
    public static WeaponPickThrow instance;

    [Header("Refrence")]
    [SerializeField] Transform player;
    [SerializeField] EasyJoystick.Joystick handRotateJoystick;
    [SerializeField] Grab grabLeft;
    [SerializeField] Button pickDropButton;
    [SerializeField] Sprite pick, drop;
    public Rigidbody2D[] weapons;
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
        SetWeaponOldPos();
    }


    private void Update()
    {
        if (PlayerHealth.isPlayerDye)
        {
            if (isWeaponPicked) Throw();
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
        if (GameSave.instance.gameData.weaponsPosition.Length >= weapons.Length)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].transform.position = new Vector3(GameSave.instance.gameData.weaponsPosition[i, 0], GameSave.instance.gameData.weaponsPosition[i, 1], 0);
                if (GameSave.instance.gameData.pickedWeaponName == weapons[i].name)
                {
                    closestWeapon = weapons[i];
                    PickUp();
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

        GameSave.instance.gameData.pickedWeaponName = closestWeapon.name;
    }

    private void Throw()
    {
        if (PlayerHealth.isPlayerDye) return;
        AudioManager.instance.Play("Throw");
        pickDropButton.image.sprite = pick;
        isWeaponPicked = false;
        Invoke("DesableWeaponButton", buttonActiveTime);

        grabLeft.DeAttachObject();
        closestWeapon.transform.parent = transform;

        closestWeapon.AddForce(new Vector2(handRotateJoystick.Horizontal(), handRotateJoystick.Vertical()) * throwForce, ForceMode2D.Impulse);

        GameSave.instance.gameData.pickedWeaponName = null;
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