using System;
using System.Data;
using UnityEngine;

public class SkillObject_Shard : SkillObject_Base
{
    [SerializeField] private GameObject shardExplodsionPrefab;
    public event Action OnExlopde;
    private Transform target;
    private float speed;
    private Skill_Shard shardManager;
    public void Explode()
    {
        DamageEnemiesInRadius(transform, checkRadius);
        SpriteRenderer sr= Instantiate(shardExplodsionPrefab, transform.position, Quaternion.identity).GetComponentInChildren<SpriteRenderer>();
        sr.color = shardManager.player.vfx.GetElementColor(type);
        OnExlopde?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (target == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void MoveTowardsClosestTarget(float speed)
    {
        target = GetClosestTarget();
        this.speed = speed;
    }
    public void SetupShard(Skill_Shard shardManager)
    {
        this.shardManager = shardManager;
        stats = shardManager.player.stats;
        scaleData = shardManager.damageScaleData;
        float detonationTime = shardManager.GetDetonateTime();
        Invoke(nameof(Explode), detonationTime);
    }

    public void SetupShard(Skill_Shard shardManager,float detonationTime,bool canMove,float shardSpeed)
    {
        this.shardManager = shardManager;
        stats = shardManager.player.stats;
        scaleData = shardManager.damageScaleData;
        Invoke(nameof(Explode), detonationTime);

        if (canMove)
            MoveTowardsClosestTarget(shardSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;
        Explode();
    }

}
