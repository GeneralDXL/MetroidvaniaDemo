using System;
using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;
    [Header("VFX details")]
    [SerializeField] private float VfxDuration = .15f;
    [SerializeField] private Material OnDamageVfx;
    private Material originalMaterial;
    private Coroutine onDamageVfxCo;

    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject onHitVfx;
    [SerializeField] private GameObject onCritVfx;
    [SerializeField] private Color hitVfxColor = Color.white;

    [Header("Element Colors")]
    [SerializeField] private Color chillVfxColor = Color.cyan;
    [SerializeField] private Color burnVfxColor = Color.red;
    [SerializeField] private Color electrifyVfxColor = Color.yellow;
    private Color originalVfxColor;
    private Coroutine statusVfxCo;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        entity = GetComponent<Entity>();
        originalVfxColor = hitVfxColor;
    }

    public void PlayElementVfx(float duration, ElementType type)
    {
        if (type == ElementType.Ice)
            StartCoroutine(PlayStatusVfxCo(duration, chillVfxColor));
        if (type == ElementType.Fire)
            StartCoroutine(PlayStatusVfxCo(duration, burnVfxColor));
        if(type==ElementType.Lightning)
            StartCoroutine(PlayStatusVfxCo(duration, electrifyVfxColor));
        
    }
    private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
    {
        float tickInterval = .25f;
        float timeHasPassed = 0;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * 0.9f;
        bool toggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timeHasPassed += tickInterval;
        }
        sr.color = Color.white;
    }
    public void UpdateOnHitColor(ElementType type)
    {
        switch (type)
        {
            case ElementType.Ice:
                hitVfxColor = chillVfxColor;
                break;
            case ElementType.Fire:
                hitVfxColor = burnVfxColor;
                break;
            case ElementType.Lightning:
                hitVfxColor=electrifyVfxColor; 
                break;
            default:
                hitVfxColor = originalVfxColor;
                break;

        }
    }

    public void StopAllVfx()
    {
        StopAllCoroutines();
        sr.color = Color.white ;
        sr.material=originalMaterial;
    }
    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject Prefab = isCrit ? onCritVfx : onHitVfx;
        GameObject vfx = Instantiate(Prefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
        if (isCrit && entity.facingDir == -1)
            vfx.transform.Rotate(0, 180, 0);
    }
    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCo != null)
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
