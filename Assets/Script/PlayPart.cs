using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayPart : MonoBehaviour
{
    public RectTransform gameBall;
    public Image gaugeCircleFG, gaugeCircleBG;
    public Transform outerBall, middleBall, innerBall;
    public Text gaugeText;
    public GameBall gameBallScript;
    public GameObject gameBorderBoxBottom;

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

    void Start()
    {
        gaugeCircleFG.fillAmount = neededTry - curTry;
        maxBallYLayout = (gameBallScript.canvasRt.sizeDelta.y - minBallYLayout) * -1;
    }

    void Update()
    {
        if(isGameJustStarted) 
        {
            StartCoroutine(ballToMinPos());
        }

        if(isGameReady)
        {
            simulateCurForceRaw(); // 테스트가 끝나면 주석 해제
            moveByCurForce();
        }
    }

    void moveByCurForce()
    {
        float zeroToOne;
        //게임모드가 아이소토닉이면
        float movableLength = (-1 * maxBallYLayout) + minBallYLayout ;
        // //게임모드가 아이소키네틱이면
        // movableLength = (-1 * maxBallYLayout) + minBallYLayout + 50;
        // //게임모드가 아이소메트릭이면
        // movableLength = (-1 * maxBallYLayout) + minBallYLayout + 100;

        if(DataManager.normalizedCurForce > 1)
        {
            zeroToOne = 1;
        }
        else if(DataManager.normalizedCurForce < 0)
        {
            zeroToOne = 0;
        }
        else
        {
            zeroToOne = DataManager.normalizedCurForce;
        }

        Vector2 target;
        
        if ((zeroToOne / 0.7f) <= 1)
        {
            gameBall.localScale = Vector3.one;
            target = new Vector2(
                gameBall.anchoredPosition.x,
                minBallYLayout - (movableLength * (zeroToOne / 0.7f))
            );
        }
        else
        {
            target = new Vector2(
                gameBall.anchoredPosition.x,
                minBallYLayout - movableLength
            );

            // 게임모드가 아이소토닉이면
            outerBall.localScale = new Vector3(
                zeroToOne / 0.7f, 1, 1
            );

            // // 게임모드가 아이소키네틱이면
            // middleBall.localScale = new Vector3(
            //     zeroToOne / 0.8f, 1, 1
            // );

            // // 게임모드가 아이소메트릭이면
            // innerBall.localScale = new Vector3(
            //     zeroToOne / 0.8f, 1, 1
            // );
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
