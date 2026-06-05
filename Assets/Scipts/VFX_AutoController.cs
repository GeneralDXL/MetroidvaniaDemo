using Unity.VisualScripting;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;
    [SerializeField] private bool randomRot = true;
    private void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRot();
        if(autoDestroy)
            Destroy(gameObject,destroyDelay);
    }
    private void ApplyRandomOffset()
    {
        if (!randomOffset) return;
        float xOffset=Random.Range(xMinOffset,xMaxOffset);
        float yOffset=Random.Range(yMinOffset,yMaxOffset);
        transform.position += new Vector3(xOffset, yOffset, 0);
    }

    private void ApplyRandomRot()
    {
        if(!randomRot) return;
        float angle = Random.Range(0, 360);
        transform.Rotate(0,0,angle);
    }
}
