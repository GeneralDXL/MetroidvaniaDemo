using UnityEngine;


public class MiniHealthbar : MonoBehaviour
{
    private Entity entity;
    private void Awake()
    {
        entity= GetComponentInParent<Entity>();
        entity.OnEntityDead += HideBar;
    }
    private void OnEnable()
    {
        entity.OnFlipped += HandleFlip;
    }

    private void OnDisable()
    {
        entity.OnFlipped-= HandleFlip;
    }

    private void HideBar()
    {
        gameObject.SetActive(false);
    }

    private void HandleFlip() => transform.rotation = Quaternion.identity;
}
