using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

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
    [SerializeField] GameObject weaponPickDropButton;
    [SerializeField] Text weaponPickDropButtonText;
    public List<Rigidbody2D> weapons = new List<Rigidbody2D>();
    private Rigidbody2D closestWeapon;
    private float weaponTimer = 1;

    [Header("PickUp")]
    [SerializeField] float pickRange;
    public static bool isWeaponPicked;
    private bool isReturning, isDistCalcAllow = !isWeaponPicked;
    private Vector3 oldPosition;
    private float shortestDistance, time = 0;

    [Header("Throw")]
    [SerializeField] float buttonActiveTime;
    [SerializeField] float throwForce;

    [Header("Shield")]
    [SerializeField] GameObject shield;
    [SerializeField] Button shieldButton;
    [SerializeField] Text shieldButtonText;
    [SerializeField] float shieldTime;
    private float currShieldTime;
    [SerializeField] float shieldButtondesableTime;
    private float currShieldButtondesableTime;
    private bool isShieldEquip;

    private void Start()
    {
        shieldButtonText.gameObject.SetActive(false);
        currShieldTime = shieldTime;
        currShieldButtondesableTime = shieldButtondesableTime;
        shieldButton.interactable = true;

        instance = this;
        isWeaponPicked = false;
        if (SaveManager.instance.isDataLoaded)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i] != null)
                {
                    if (SaveManager.instance.saveData.pickedWeaponName == weapons[i].name)
                    {
                        closestWeapon = weapons[i];
                        PickUp();
                    }
                    else
                    {
                        if (SaveManager.instance.isDataLoaded) weapons[i].transform.position = new Vector3(SaveManager.instance.saveData.weaponsPosition[i, 0], SaveManager.instance.saveData.weaponsPosition[i, 1], 0);
                    }
                }
            }
        }
    }


    private void Update()
    {
        if (PlayerHealth.isPlayerDye)
        {
            if (!isWeaponPicked) Throw();
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PickDropButton();
        }

        // Active Deactive Shield
        if (!shieldButton.interactable)
        {
            if (isShieldEquip)
            {
                if (currShieldTime > 0)
                {
                    shieldButtonText.gameObject.SetActive(true);
                    shieldButtonText.text = ((int)currShieldTime).ToString();
                    currShieldTime -= Time.deltaTime;
                }
                else
                {
                    isShieldEquip = false;
                    currShieldTime = shieldTime;
                    shield.SetActive(false);
                }
            }
            else
            {
                if (currShieldButtondesableTime > 0)
                {
                    shieldButtonText.gameObject.SetActive(true);
                    shieldButtonText.text = ((int)currShieldButtondesableTime).ToString();
                    currShieldButtondesableTime -= Time.deltaTime;
                }
                else
                {
                    shieldButtonText.gameObject.SetActive(false);
                    currShieldButtondesableTime = shieldButtondesableTime;
                    shieldButton.interactable = true;
                }
            }
        }

        // Calcuate Distance Butween Player and Weapons
        if (isDistCalcAllow) CalculateDistance();

        // Fix the Position and rotation of (left & right grab) and this game object
        if (isWeaponPicked)
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
                weaponTimer -= Time.deltaTime;
                if (weaponTimer > 0)
                {
                    grabLeft.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    //grabLeft.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Clamp(grabLeft.transform.localRotation.z, -160, 10));
                    grabLeft.rigidBody.angularVelocity = 0.5f;
                }
            }
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
        shortestDistance = Mathf.Infinity;
        closestWeapon = weapons[0];
        for (var i = 0; i < weapons.Count; i++)
        {
            float distance = Vector2.Distance(player.position, weapons[i].transform.position);
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
        AudioManager.instance.Play("Pick");
        if (IsInvoking("DesableWeaponButton")) CancelInvoke("DesableWeaponButton");
        isWeaponPicked = true;
        isDistCalcAllow = false;

        time = 0;
        oldPosition = closestWeapon.transform.position;
        isReturning = true;
        closestWeapon.transform.parent = grabLeft.transform;
        grabLeft.AttachObject(closestWeapon);

        weaponPickDropButton.SetActive(true);
        weaponPickDropButtonText.text = "Drop";

        SaveManager.instance.saveData.pickedWeaponName = closestWeapon.name;
    }

    private void Throw()
    {
        AudioManager.instance.Play("Throw");
        weaponPickDropButtonText.text = "Pick Up";
        isWeaponPicked = false;
        Invoke("DesableWeaponButton", buttonActiveTime);

        grabLeft.DeAttachObject();
        closestWeapon.transform.parent = transform;

        closestWeapon.AddForce(new Vector2((Mathf.Clamp(handRotateJoystick.Horizontal(), -1, 1)), Mathf.Clamp(handRotateJoystick.Vertical(), -1, 1)) * throwForce, ForceMode2D.Impulse);

        SaveManager.instance.saveData.pickedWeaponName = null;
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

    public void ShieldButton()
    {
        shield.SetActive(true);
        shieldButton.interactable = false;
        isShieldEquip = true;
    }
}