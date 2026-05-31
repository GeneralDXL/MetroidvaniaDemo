using UnityEngine;
using System;

[Serializable]
public class ParallaxLayer 
{
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private Transform background;
    private float imageFullWidth;
    private float imageHalfWidth;
    private float cameraOffset = 10f;
    
    public void CaculateWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right*(distanceToMove * parallaxMultiplier);
    }

    public void loopBackground(float cameraLeftEdge,float cameraRightEdge)
    {
        float imageLeftEdge = background.transform.position.x - imageHalfWidth +cameraOffset;
        float imageRightEdge = background.transform.position.x + imageHalfWidth-cameraOffset;
        if (imageLeftEdge > cameraRightEdge)
            background.transform.position += Vector3.right * -imageFullWidth;
        else if (imageRightEdge < cameraLeftEdge)
            background.transform.position += Vector3.right * imageFullWidth;
    }
}
