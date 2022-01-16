using UnityEngine;

public class BGScroll : MonoBehaviour
{
    [SerializeField] Renderer bgRenderer;
    [SerializeField] Transform walkCam;
    [SerializeField] float speedMultiplier;
    private float walkCamOldXpos;

    private void Start()
    {
        walkCamOldXpos = walkCam.position.x;
    }

    private void FixedUpdate()
    {
        bgRenderer.material.mainTextureOffset += new Vector2((walkCam.position.x - walkCamOldXpos) * speedMultiplier, 0);
        walkCamOldXpos = walkCam.position.x;
    }
}