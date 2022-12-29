using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TutorialArrows : MonoBehaviour
{
    public RectTransform gameBall;
    public RectTransform canvas;
    public RectTransform bottom;
    RectTransform rt;
    
    void Update()
    {
        rt = gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(gameBall.anchoredPosition.x, 45 + bottom.sizeDelta.y);
        rt.sizeDelta = new Vector2(canvas.sizeDelta.y / (5f * 3f), canvas.sizeDelta.y / 5f);
    }
}
