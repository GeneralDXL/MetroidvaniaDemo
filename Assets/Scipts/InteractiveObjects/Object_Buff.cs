using System;
using System.Collections;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity_Stats statsToModify;
    [Header("buff details")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private float buffDuration = 4;
    [SerializeField] private bool canBeUsed = true;
    [SerializeField] private string buffName;

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
        statsToModify = collision.GetComponent<Entity_Stats>();
        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        ApplyBuff(true);
        yield return new WaitForSeconds(duration);
        ApplyBuff(false);
        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach (var buff in buffs)
        {
            if (apply)
                statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName);
            else
                statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
        }
    }
}

[Serializable]
public class Buff
{
    public StatType type;
    public float value;
}
