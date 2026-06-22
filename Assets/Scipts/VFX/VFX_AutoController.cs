using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    private SpriteRenderer sr;
    private float fadeSpeed=1;
    [SerializeField]private bool canFade;
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;
    [Header("Offset")]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;
    [Header("Rotation")]
    [SerializeField] private bool randomRot = true;
    [SerializeField] private float minRot = 0;
    [SerializeField] private float maxRot = 360;
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        if (canFade)
            StartCoroutine(FadeCo());
        ApplyRandomOffset();
        ApplyRandomRot();
        if(autoDestroy)
            Destroy(gameObject,destroyDelay);
    }

    private IEnumerator FadeCo()
    {
        Color targetColor=Color.white;
        while(targetColor.a>0)
        {
            targetColor.a-=fadeSpeed*Time.deltaTime;
            sr.color = targetColor;
            yield return null;  
        }
        sr.color=targetColor;
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
        float angle = Random.Range(minRot, maxRot);
        transform.Rotate(0,0,angle);
    }
}
