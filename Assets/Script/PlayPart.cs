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

    public int neededTry = 10;
    public int curTry = 0;
    
    public float mappedY = 0;
    public float startingBallY = -483;
    public float minBallYasLayout = -303;
    public float maxBallYasLayout = -670;

    public bool isGameJustStarted = true;
    public bool isGameReady = false;

    public float testForceValueNoise = 0;

    void Start()
    {
        gaugeCircleFG.fillAmount = neededTry - curTry;
    }

    void Update()
    {
        if(isGameJustStarted) 
        {
            StartCoroutine(ballToMinPos());
        }

        if(isGameReady)
        {
            makeTestVal(); // 테스트가 끝나면 주석 해제
            moveByCurForce();
        }
    }

    void moveByCurForce()
    {
        float zeroToOne;
        float movableLength = (-1 * maxBallYasLayout) + minBallYasLayout ;
        
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

        Vector2 target = new Vector2(
            gameBall.anchoredPosition.x,
            minBallYasLayout - (movableLength * zeroToOne)
        );

        Debug.Log(minBallYasLayout - (movableLength * zeroToOne));

        gameBall.anchoredPosition = target;
    }

    void makeTestVal()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(UnityEngine.Random.Range(0f, 1f) < 0.1f)
            {
                testForceValueNoise = UnityEngine.Random.Range(
                    0f * DataManager.thresholdMax,
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
            if (UnityEngine.Random.Range(0f, 1f) < 0.1f)
            {
                testForceValueNoise = UnityEngine.Random.Range(
                    0f * DataManager.thresholdMax,
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
        Vector2 _startingPos = new Vector2(gameBall.anchoredPosition.x, minBallYasLayout);
        
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
