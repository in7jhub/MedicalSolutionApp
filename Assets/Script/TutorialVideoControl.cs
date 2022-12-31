using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TutorialVideoControl : MonoBehaviour
{
    public VideoPlayer vp;
    double time;
    bool firstTouch = true;
    bool secondTouch = false;
    bool nextVideoReady = false;
    public GameObject nextTutorial;
    public bool playOnAwake;

    private void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        firstTouch = true;
        secondTouch = false;
        nextVideoReady = false;
        vp.frame = 0;
        if(nextTutorial != null)
        {
            nextTutorial.GetComponent<TutorialVideoControl>().vp.Pause();
            nextTutorial.GetComponent<TutorialVideoControl>().vp.frame = 0;
            nextTutorial.GetComponent<Image>().enabled = false;
        }
        if(playOnAwake) vp.Play();
        else vp.Pause();
        vp.loopPointReached += CheckOver; // 비디오 끝남 다른 버전
        time = vp.GetComponent<VideoPlayer>().clip.length;
    }

    void OnEnable()
    {
        firstTouch = true;
        secondTouch = false;
        vp.frame = 0;
        if (playOnAwake) vp.Play();
        else vp.Pause();
    }

    void OnDisable()
    {
        firstTouch = true;
        secondTouch = false;
        vp.frame = 0;
        vp.Pause();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && firstTouch) // 아무 곳이나 터치
        {
            vp.Play();
            firstTouch = false;
        }

        if (vp.frame == (long)vp.frameCount) // 비디오 끝남 
        {
            vp.frame = (long)vp.frameCount - 3;
            vp.Pause();
            nextVideoReady = true;
            secondTouch = true;
            if(nextTutorial != null) {
                nextTutorial.GetComponent<Image>().enabled = true;
                nextTutorial.SetActive(true);
                nextTutorial.GetComponent<TutorialVideoControl>().vp.Pause();
                nextTutorial.GetComponent<TutorialVideoControl>().firstTouch = false;
            }
        }

        if(nextVideoReady && secondTouch && Input.GetMouseButtonDown(0)) // 비디오 끝나고 터치하면 초기화
        {
            gameObject.SetActive(false);
            if(nextTutorial!=null)
            {
                nextTutorial.GetComponent<Image>().enabled = true;
                nextTutorial.SetActive(true);
                nextTutorial.GetComponent<TutorialVideoControl>().playOnAwake = true;
                nextTutorial.GetComponent<TutorialVideoControl>().vp.Play();
                nextTutorial.GetComponent<TutorialVideoControl>().firstTouch = true;    
            }
            firstTouch = true;
            secondTouch = false;
        }
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        vp.frame = 0;
        vp.Pause();
        nextVideoReady = true;
        firstTouch = false;
        secondTouch = true;
    }
}
