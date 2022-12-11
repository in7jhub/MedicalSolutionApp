using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public enum Page {Home, Patients, Settings, MeasuringSession, SessionSettings, Game, SessionSummary, None}
    public enum GamePage {Default, Tutorial, Prepare, Play, None}
    static Page page = Page.None;
    static GamePage gamePage = GamePage.None;
    public GameObject[] pagegobjs;
    string[] pageNames;

    public static Dictionary<string, GameObject> pageSet = new Dictionary<string, GameObject>();
    // private GameObject Default, Tutorial, Prepare, Play;

    void Awake()
    {
        pageNames = Enum.GetNames(typeof(Page));
        initPages();
        setPage(Page.Home);
        setGamePage(GamePage.Default);
        DontDestroyOnLoad(this.gameObject);
    }

    public void initPages()
    {
        for(int i = 0; i < pagegobjs.Length; i++)
        {
            if(pagegobjs[i] == null)
                return;
            else if(pageNames[i] == pagegobjs[i].name)
                pageSet.Add(pageNames[i], pagegobjs[i]);
            else
                Debug.LogError("페이지 Enum과 GameObject가 일치하지 않음");

            pageSet[pageNames[i]].SetActive(false);
        }
    }

    public static void setGamePage(string _page)
    {
        if (_page == PageManager.gamePage.ToString())
        {
            return;
        }

        // 게임페이지 케이스 처리
    }

    public static void setGamePage(PageManager.GamePage _page)
    {
        gamePage = _page;
    }

    public static void setPage(string _page)
    {
        if (_page == PageManager.page.ToString())
        {
            return;
        }

        try 
        {
            //페이지 static enum 할당
            page = EnumUtil<Page>.Parse(_page);
        }
        catch
        {
            Debug.LogError("ParseError : 존재하지 않는 이름의 페이지로 전환하려 했습니다.");
        }

        //기존 페이지 끄기
        pageSet[_page.ToString()].SetActive(false);

        //요청된 페이지 켜기
        if(pageSet.ContainsKey(_page.ToString()))
        {
            pageSet[_page.ToString()].SetActive(true);
        }
        else
        {
            Debug.LogError("존재하지 않는 페이지로 전환하려 했습니다.");
        }
    }

    public static void setPage(Page _page)
    {
        if (_page == PageManager.page)
        {
            return;
        }

        //기존 페이지 끄기
        pageSet[_page.ToString()].SetActive(false);

        //요청된 페이지 켜기
        if (pageSet.ContainsKey(_page.ToString()))
        {
            pageSet[_page.ToString()].SetActive(true);
        }
        else
        {
            Debug.LogError("존재하지 않는 페이지로 전환하려 했습니다.");
        }
    }

    public static PageManager.Page getPage()
    {
        return page;
    }

    public static PageManager.Page getGamePage()
    {
        return page;
    }
}

public static class EnumUtil<T>
{
    public static T Parse(string s)
    {
        return (T)Enum.Parse(typeof(T), s);
    }
}