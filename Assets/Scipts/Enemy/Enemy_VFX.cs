using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [SerializeField]private GameObject alert;
    
    public void EnableAlert()
    {
        alert.SetActive(true);
    }

    public void DisableAlert()
    {
        alert.SetActive(false);
    }
}
