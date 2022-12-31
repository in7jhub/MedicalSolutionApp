using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TutorialFitter : MonoBehaviour
{
    RectTransform rt;
    public RectTransform bottom;
    public RectTransform canvasRt;
    Vector2 curCanvasSize, prevCanvasSize;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        curCanvasSize = canvasRt.sizeDelta;
        prevCanvasSize = canvasRt.sizeDelta;
        float halfHeightLimit = (curCanvasSize.y / 2f) - bottom.sizeDelta.y - 45;
        rt.sizeDelta = new Vector2(halfHeightLimit * 2 * 1.86f, halfHeightLimit * 2);
    }

    void Update()
    {
        curCanvasSize = canvasRt.sizeDelta;
        
        if(curCanvasSize != prevCanvasSize)
        {
            float halfHeightLimit = (curCanvasSize.y / 2f) - bottom.sizeDelta.y - 45;
            rt.sizeDelta = new Vector2(halfHeightLimit * 2 * 1.86f, halfHeightLimit * 2);
        }

        prevCanvasSize = curCanvasSize;
    }
}
