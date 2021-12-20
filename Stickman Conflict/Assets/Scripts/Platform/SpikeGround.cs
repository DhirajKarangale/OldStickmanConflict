using UnityEngine;

public class SpikeGround : MonoBehaviour
{
    [SerializeField] GameObject destroyGround, spikeGround;
    [SerializeField] ParticleSystem groundDestroyEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            AudioManager.instance.Play("DestroyGround");
            groundDestroyEffect.Play();
            destroyGround.SetActive(false);
            spikeGround.SetActive(true);
            Destroy(this);
        }
    }
}