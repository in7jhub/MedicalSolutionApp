using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameBall : MonoBehaviour
{
    public RectTransform outer;
    public RectTransform middle;
    public RectTransform inner;
    public RectTransform rt;
    public RectTransform canvasRt;
    public float parentRtX;

    void Update()
    {
        float ratio = canvasRt.sizeDelta.x / canvasRt.sizeDelta.y; 
        if(ratio < 1)
        {
            if(canvasRt.sizeDelta.y * 0.3f < canvasRt.sizeDelta.x * 0.3f)
            {
                outer.sizeDelta = new Vector2(
                    canvasRt.sizeDelta.y * 0.3f,
                    canvasRt.sizeDelta.y * 0.3f
                );
            }
            else
            {
                outer.sizeDelta = new Vector2(
                    canvasRt.sizeDelta.x * 0.3f,
                    canvasRt.sizeDelta.x * 0.3f
                );
            }
        }
        else
        {
            if (canvasRt.sizeDelta.x * 0.3f < canvasRt.sizeDelta.y * 0.3f)
            {
                outer.sizeDelta = new Vector2(
                    canvasRt.sizeDelta.x * 0.3f,
                    canvasRt.sizeDelta.x * 0.3f
                );
            }
            else
            {
                outer.sizeDelta = new Vector2(
                    canvasRt.sizeDelta.y * 0.3f,
                    canvasRt.sizeDelta.y * 0.3f
                );
            }
        }

        middle.sizeDelta = outer.sizeDelta * 0.7f;
        inner.sizeDelta = outer.sizeDelta * 0.4f;

        Debug.Log(ratio);
        Debug.Log(rt.sizeDelta);

        parentRtX = canvasRt.sizeDelta.x;
    }
}
