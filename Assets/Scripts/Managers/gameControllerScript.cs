using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public enum targettingOption
{
    Enemy,
    Ally,
    Road,
    None,
}
[System.Serializable]
public enum GameState
{
    Wave,
    GameMenu,
    PauseMenu
}
[System.Serializable]
public class Flag
{
    public int ID;
    public bool check;
    public Flag(int newID, bool newCheck)
    {
        ID = newID;
        check = newCheck;
    }
}
[System.Serializable]
public class NewFlagDic
{
    public Flag[] dictItems;
    public Dictionary<int, bool> toDictionary()
    {
        Dictionary<int, bool> newDict = new Dictionary<int, bool>();

        foreach (Flag item in dictItems)
        {
            newDict.Add(item.ID, item.check);
        }

        return newDict;
    }
}

[System.Serializable]
public class MainInput_Ray
{
    public Ray ray;
    public GameObject hit;
    public bool hittingSomething;
}

[System.Serializable]
public class Wave
{
    public int ID;
    public EnemyAndDelay[] spawnOrder;
}
[System.Serializable]
public class TimelineAnimation
{
    public int ID;
    public TimelineAsset timelineAnim;
}

[System.Serializable]
public class EnemyAndDelay
{
    public int enemyID;
    public float delay;
    public int repeat;
}
public class gameControllerScript : MonoBehaviour
{
    public static gameControllerScript instance;
    
    public resourceManagerScript resourceManager;
    public deckManagerScript deckManager;
    public waveManagerScript waveManager;
    public timeManagerScript timeManager;
    public timelineManagerScript timelineManager;

    public MainInput_Ray mainInput_Ray;
    private LayerMask MouseArea;
    private Camera mainCamera;
    public GameObject handCanvas;
    public GameObject topCanvas;

    [SerializeField]
    NewFlagDic eventDict;
    //for keeping track of important events
    Dictionary<int, bool> flagDict;

    public GameState state;
    public bool isCardDragged;

    void Start()
    {
        instance = this;

        flagDict = eventDict.toDictionary();
        findManagers();
        mainInput_Ray = new MainInput_Ray();
        mainInput_Ray.hit = gameObject;
        MouseArea = LayerMask.GetMask("Mouse");

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        handCanvas = GameObject.Find("Hand Canvas");
        topCanvas = GameObject.Find("Top Canvas");

        state = GameState.Wave;

        timeManager.resetTime();
        waveManager.spawnWave(11001);
    }
    void Update()
    {
        /*PointerEventData eventData = new PointerEventData(EventSystem.current);
        mainInput_Ray.hit = eventData.pointerCurrentRaycast.gameObject;
        Debug.DrawRay(eventData.pointerCurrentRaycast.screenPosition, Vector3.forward, Color.black, 3f);
        Debug.Log(mainInput_Ray.hit);*/
        mainInput_Ray.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mainInput_Ray.hittingSomething = Physics2D.Raycast(mainInput_Ray.ray.origin, mainInput_Ray.ray.direction, 1000, MouseArea);
        if (mainInput_Ray.hittingSomething)
        {
            mainInput_Ray.hit = Physics2D.Raycast(mainInput_Ray.ray.origin, mainInput_Ray.ray.direction, 1000, MouseArea).transform.gameObject;
        }
        else
        {
            mainInput_Ray.hit = gameObject;
        }
    }

    void findManagers()
    {
        resourceManager = GameObject.Find("resourceManager").GetComponent<resourceManagerScript>();
        deckManager = GameObject.Find("deckManager").GetComponent<deckManagerScript>();
        waveManager = GameObject.Find("waveManager").GetComponent<waveManagerScript>();
        timeManager = GameObject.Find("timeManager").GetComponent<timeManagerScript>();
        timelineManager = GameObject.Find("timelineManager").GetComponent<timelineManagerScript>();
    }

    public void checkFlag(int ID)
    {
        //flagDict[ID] = true;
        Debug.Log(ID + 1);
        //spawns the pre-set waves
        if (ID > 11000 && ID < 11010)
        {
            waveManager.spawnWave(ID + 1);
            deckManager.drawCard(1);
            if (ID % 5 == 0)
            {
                deckManager.openShop();
            }
        }
        //each event an ID would trigger, use for really important events
        switch (ID)
        {
            case 11010:
                break;
            default:
                break;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.Wave:
                timeManager.resetTime();
                break;
            case GameState.GameMenu:
                break;
            case GameState.PauseMenu:
                timeManager.pauseTime();
                break;
            default:
                break;
        }
    }
}