using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    public enum GameSequence
    {
        tutorial,
        training,
        resting
    }

    public enum GameMode
    {
        isotonic,
        isokinetic,
        isometric
    }

    public static GameSequence curGameSeq = GameSequence.tutorial;
    public static GameMode curGameMode = GameMode.isotonic;

}
