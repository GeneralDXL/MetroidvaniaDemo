using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("VFX details")]
    [SerializeField] private float VfxDuration = .15f;
    [SerializeField] private Material OnDamageVfx;
    private Material originalMaterial;
    private Coroutine onDamageVfxCo;

    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject onHitVfx;
    [SerializeField] private Color hitVfxColor=Color.white;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }
    
    public void CreateOnHitVFX(Transform target)
    {
        GameObject vfx= Instantiate(onHitVfx,target.position,Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
    }
    public void PlayOnDamageVfx()
    {
        if(onDamageVfxCo != null)
            StopCoroutine(onDamageVfxCo);
        onDamageVfxCo = StartCoroutine(OnDamageVfxCo());
    }
    private IEnumerator OnDamageVfxCo()
    {
        sr.material = OnDamageVfx;
        yield return new WaitForSeconds(VfxDuration);
        sr.material = originalMaterial;
    }
}
