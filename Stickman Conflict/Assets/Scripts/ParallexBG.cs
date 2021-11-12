using UnityEngine;

public class ParallexBG : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float parallexAmount;
    private float lenght, startPos;

    private void Start()
    {
        startPos = transform.position.x;
        lenght = spriteRenderer.bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.position.x * (1 - parallexAmount));
        float dis = (cam.position.x * parallexAmount);

        transform.position = new Vector3(startPos + dis, transform.position.y, transform.position.z);
        if (temp > startPos + lenght) startPos += lenght;
        else if (temp < startPos - lenght) startPos -= lenght;
    }
}
