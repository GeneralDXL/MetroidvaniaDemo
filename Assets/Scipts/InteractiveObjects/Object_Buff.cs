using System;
using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("buff details")]
    [SerializeField] private EffectDataSO[] effects;
    [SerializeField] private bool canBeUsed = true;

    [Header("Float movement details")]
    [SerializeField] private float floatSpeed = 1;
    [SerializeField] private float floatRange = 0.2f;
    private Vector3 originalPos;
    private void Awake()
    {
        originalPos=transform.position;
        sr=GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        float yOffset=Mathf.Sin(Time.time * floatSpeed)*floatRange;
        transform.position = originalPos+new Vector3(0,yOffset,0);   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed) return;
        Player_EffectManager effectManager = collision.GetComponent<Player_EffectManager>();
        if (effectManager == null) return;

        canBeUsed = false;
        sr.color = Color.clear;
        foreach (var effectData in effects)
            effectManager.AddEffect(new Effect(effectData));
        Destroy(gameObject, 0.1f);
    }
}
