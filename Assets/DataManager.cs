using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static float thresholdMax = 90;
    public static float thresholdMin = 10;
    public static float curForceRaw = 0;
    public static float normalizedCurForce = 0;

    public float curForceRawCpy = 0;
    public float normalizedCurForceCpy = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        curForceRawCpy = curForceRaw;
        normalizedCurForceCpy = normalizedCurForce;
        normalizeCurForce();
    }
    

    float normalizeCurForce()
    {
        normalizedCurForce = (DataManager.curForceRaw - DataManager.thresholdMin) / (DataManager.thresholdMax - DataManager.thresholdMin);
        return normalizedCurForce;
    }
}
