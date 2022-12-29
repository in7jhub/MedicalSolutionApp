using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TutorialText : MonoBehaviour
{
    public RectTransform ball;

    void Update()
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(
            ball.anchoredPosition.x, 
            ball.anchoredPosition.y + ball.sizeDelta.y / 1.3f
        );
    }
}
