using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] layers;
    private Camera cam;
    private float lastCameraPositonX;
    private float halfCameraWidth;
    private void Awake()
    {
        cam = Camera.main;
        lastCameraPositonX = cam.transform.position.x;
        halfCameraWidth = cam.orthographicSize * cam.aspect;
        CacuImageWidth();
    }

    private void CacuImageWidth()
    {
        foreach (ParallaxLayer layer in layers)
            layer.CaculateWidth();
    }

    private void FixedUpdate()
    {
        float currentCamarePositionX = cam.transform.position.x;
        float distanceToMove = currentCamarePositionX- lastCameraPositonX;
        lastCameraPositonX = currentCamarePositionX;
        float cameraLeftEdge = currentCamarePositionX - halfCameraWidth;
        float cameraRightEdge=currentCamarePositionX + halfCameraWidth;
        foreach (ParallaxLayer layer in layers)
        {
            layer.Move(distanceToMove);
            layer.loopBackground(cameraLeftEdge,cameraRightEdge);
        }
    }
}
