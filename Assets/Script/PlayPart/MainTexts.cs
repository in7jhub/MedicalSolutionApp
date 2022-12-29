using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTexts : MonoBehaviour
{
    public Text gameSequenceTxt;
    public Text isotonicTxt;
    public Text isokineticTxt;
    public Text isometricTxt;
    Color grey = new Color(200f / 255f, 212f / 255f, 223f / 255f);
    Color green = new Color(169f / 255f, 197f / 255f, 58f / 255f);

    void Update()
    {
        switch(GamePhaseManager.curGameMode)
        {
            case GamePhaseManager.GameMode.isotonic :
                isotonicTxt.color = green;
                isokineticTxt.color = grey;
                isometricTxt.color = grey;
                break;
            case GamePhaseManager.GameMode.isokinetic:
                isotonicTxt.color = grey;
                isokineticTxt.color = green; 
                isometricTxt.color = grey;
                break;
            case GamePhaseManager.GameMode.isometric:
                isotonicTxt.color = grey;
                isokineticTxt.color = grey;
                isometricTxt.color = green; 
                break;
        }

        switch (GamePhaseManager.curGameSeq)
        {
            case GamePhaseManager.GameSequence.tutorial:
                gameSequenceTxt.text = "TUTORIAL";
                break;
            case GamePhaseManager.GameSequence.training:
                gameSequenceTxt.text = "TRAINING";
                break;
            case GamePhaseManager.GameSequence.resting:
                gameSequenceTxt.text = "RESTING";
                break;
        }
    }
}
