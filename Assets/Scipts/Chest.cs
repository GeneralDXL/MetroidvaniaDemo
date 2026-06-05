using UnityEngine;

public class Chest : MonoBehaviour,IDamagable
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb=>GetComponent<Rigidbody2D>();
    private Entity_VFX vfx => GetComponent<Entity_VFX>();
    [Header("Open details")]
    [SerializeField] private Vector2 knockback;
    public void TakeDamage(float damage,Transform dealer)
    {
        vfx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
