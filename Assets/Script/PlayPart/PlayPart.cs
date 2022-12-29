using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayPart : MonoBehaviour
{
    public RectTransform gameBall;
    public Image gaugeCircleFG, gaugeCircleBG;
    public RectTransform outerBall, middleBall, innerBall;
    public Text gaugeText;
    public GameBall gameBallScript;
    public RectTransform gameBorderBoxBottom;

    public int neededTry = 10;
    public int curTry = 0;
    
    public float mappedY = 0;
    public float startingBallY;
    public float minBallYLayout;
    public float maxBallYLayout;

    public bool isGameJustStarted = true;
    public bool isGameReady = false;
    public bool noiseOn;

    public float testForceValueNoise = 0;


    void Update()
    {
        if(curTry <= 0) gaugeCircleFG.fillAmount = 1;
        else gaugeCircleFG.fillAmount =  1f - ((float)curTry / (float)neededTry);

        if(isGameJustStarted) 
        {
            StartCoroutine(ballToMinPos());
        }

        if(isGameReady)
        {
            simulateCurForceRaw(); // 테스트가 끝나면 주석 처리
            moveByCurForce();
        }
    }

    void moveByCurForce()
    {
        switch(GamePhaseManager.curGameMode)
        {
            case GamePhaseManager.GameMode.isotonic :
                maxBallYLayout = (gameBallScript.canvasRt.sizeDelta.y - 45/*bottomMagin*/ - gameBorderBoxBottom.sizeDelta.y - outerBall.sizeDelta.y / 2) * -1;
                minBallYLayout = -125 - outerBall.sizeDelta.y / 2;
                break;
            case GamePhaseManager.GameMode.isokinetic:
                maxBallYLayout = (gameBallScript.canvasRt.sizeDelta.y - 45/*bottomMagin*/ - gameBorderBoxBottom.sizeDelta.y - middleBall.sizeDelta.y / 2) * -1;
                minBallYLayout = -125 - middleBall.sizeDelta.y / 2;
                break;
            case GamePhaseManager.GameMode.isometric:
                maxBallYLayout = (gameBallScript.canvasRt.sizeDelta.y - 45/*bottomMagin*/ - gameBorderBoxBottom.sizeDelta.y - innerBall.sizeDelta.y / 2) * -1;
                minBallYLayout = -125 - innerBall.sizeDelta.y / 2;
                break;
            default :
                maxBallYLayout = (gameBallScript.canvasRt.sizeDelta.y - 45/*bottomMagin*/ - gameBorderBoxBottom.sizeDelta.y - outerBall.sizeDelta.y / 2) * -1;
                minBallYLayout = -125 - outerBall.sizeDelta.y / 2;
                break;
        }

        Vector2 target; // 공이 이동하기 위한 타겟
        float zeroToOne;
        float movableLength = (-1 * maxBallYLayout) + minBallYLayout;

        if(DataManager.normalizedCurForce > 1) zeroToOne = 1;
        else if(DataManager.normalizedCurForce < 0) zeroToOne = 0;
        else zeroToOne = DataManager.normalizedCurForce;

        
        if ((zeroToOne / 0.7f) <= 1) // 70퍼센트까지 누르면 바닥에 공이 닿음
        {
            gameBall.localScale = Vector3.one;
            target = new Vector2(
                gameBall.anchoredPosition.x,
                minBallYLayout - (movableLength * (zeroToOne / 0.7f))
            );
        }
        else // 바닥에 공이 닿으면
        {
            target = new Vector2(
                gameBall.anchoredPosition.x,
                minBallYLayout - movableLength
            );

            switch(GamePhaseManager.curGameMode)
            {
                case GamePhaseManager.GameMode.isotonic :
                    outerBall.localScale = new Vector3(
                        zeroToOne / 0.7f, 1, 1
                    );
                    break;
                case GamePhaseManager.GameMode.isokinetic :
                    middleBall.localScale = new Vector3(
                        zeroToOne / 0.7f, 1, 1
                    );
                    break;
                case GamePhaseManager.GameMode.isometric :
                    innerBall.localScale = new Vector3(
                        zeroToOne / 0.7f, 1, 1
                    );
                    break;
            }
        }

        gameBall.anchoredPosition = target;
    }

    void simulateCurForceRaw()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(UnityEngine.Random.Range(0f, 1f) < 0.1f && noiseOn)
            {
                testForceValueNoise = UnityEngine.Random.Range(
                    0f,
                    0.1f * DataManager.thresholdMax
                );
            }

            DataManager.curForceRaw = Mathf.Lerp( 
                DataManager.curForceRaw,
                DataManager.thresholdMax - testForceValueNoise,
                0.01f
            );
        }
        else
        {
            if (UnityEngine.Random.Range(0f, 1f) < 0.1f && noiseOn)
            {
                testForceValueNoise = UnityEngine.Random.Range(
                    0f,
                    0.1f * DataManager.thresholdMax
                );
            }

            DataManager.curForceRaw = Mathf.Lerp(
                DataManager.curForceRaw,
                DataManager.thresholdMin + testForceValueNoise,
                0.05f
            );
        }
    }

    IEnumerator ballToMinPos()
    {
        gameBall.anchoredPosition = new Vector2(gameBall.anchoredPosition.x, -485);
        isGameJustStarted = false;
        Vector2 _startingPos = new Vector2(gameBall.anchoredPosition.x, minBallYLayout);
        
        while (((Vector2)gameBall.anchoredPosition - _startingPos).magnitude > 0.1)
        {
            gameBall.anchoredPosition = Vector2.Lerp (
                gameBall.anchoredPosition,
                _startingPos,
                0.01f
            );

            yield return new WaitForEndOfFrame();
        }

        //바닥에 닿을 정도의 힘
        float subOfMinMax = DataManager.thresholdMax - DataManager.thresholdMin;

        isGameReady = true;
        yield return null;
    }

}
