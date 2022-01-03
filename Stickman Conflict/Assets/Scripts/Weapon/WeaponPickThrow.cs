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
    public GameObject ropeButton;
    public GameObject ropeEmmiter;

    [Header("PickUp")]
    [SerializeField] float pickRange;
    public static bool isWeaponPicked;
    private bool isReturning, isDistCalcAllow = !isWeaponPicked;
    private Vector3 oldPosition;
    private float shortestDistance, distance, time = 0;

    [Header("Throw")]
    [SerializeField] float buttonActiveTime;
    [SerializeField] float throwForce;


    private void Start()
    {
        //PlayerPrefs.DeleteKey("RopeGet");
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
                        weapons[i].transform.position = new Vector3(SaveManager.instance.saveData.weaponsPosition[i, 0], SaveManager.instance.saveData.weaponsPosition[i, 1], 0);
                    }
                }
            }
        }

        if (PlayerPrefs.GetInt("RopeGet", 0) != 1)
        {
            ropeButton.SetActive(false);
            ropeEmmiter.SetActive(false);
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

        // Calcuate Distance Butween Player and Weapons
        if (isDistCalcAllow) CalculateDistance();

        // Fix the Position and rotation of (left & right grab) and this game object
        if (isWeaponPicked)
        {
            closestWeapon.transform.localPosition = Vector3.zero;
            closestWeapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
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
        shortestDistance = Mathf.Infinity;
        closestWeapon = weapons[0];
        for (var i = 0; i < weapons.Count; i++)
        {
            distance = Mathf.Abs(player.position.x - weapons[i].transform.position.x);
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
}