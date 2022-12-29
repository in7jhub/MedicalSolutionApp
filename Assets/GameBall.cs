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

        switch(GamePhaseManager.curGameMode)
        {
            case GamePhaseManager.GameMode.isotonic :
                outer.gameObject.SetActive(true);
                middle.gameObject.SetActive(true);
                inner.gameObject.SetActive(true);
                break;
            case GamePhaseManager.GameMode.isokinetic :
                outer.gameObject.SetActive(false);
                middle.gameObject.SetActive(true);
                inner.gameObject.SetActive(true);
                break;
            case GamePhaseManager.GameMode.isometric :
                outer.gameObject.SetActive(false);
                middle.gameObject.SetActive(false);
                inner.gameObject.SetActive(true);
                break;
        }

        parentRtX = canvasRt.sizeDelta.x;
    }
}
