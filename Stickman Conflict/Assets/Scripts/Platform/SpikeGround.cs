using UnityEngine;

public class SpikeGround : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] GameObject destroyGround, spikeGround;
    [SerializeField] ParticleSystem groundDestroyEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boxCollider.enabled = false;
        AudioManager.instance.Play("DestroyGround");
        groundDestroyEffect.Play();
        destroyGround.SetActive(false);
        spikeGround.SetActive(true);
        Destroy(this);
    }
}
