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
    TowerMenu,
    ShopMenu,
    PauseMenu
}
[System.Serializable]
public enum Managers
{
    None,
    Game,
    Resource,
    Time,
    Deck,
    Wave,
    Timeline,
    Audio
}
[System.Serializable]
public enum SoundType
{
    Master,
    SFX,
    Music
}
[System.Serializable]
public enum StatType
{
    Damage,
    Pierce,
    Speed,
}

[System.Serializable]
public class Flag
{
    public int ID;
    public bool check;
    public string name;
    public Flag NewFlag(int newID, bool newCheck, string newName)
    {
        Flag current = new Flag();
        current.ID = newID;
        current.check = newCheck;
        current.name = newName;
        return current;
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
[System.Serializable]
public class Sound
{
    public int ID;
    
    public AudioClip[] clips;
    public int maxInstances;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource[] source;

    public SoundType type;
}
public class gameControllerScript : MonoBehaviour
{
    public static gameControllerScript instance;
    
    public resourceManagerScript resourceManager;
    public deckManagerScript deckManager;
    public waveManagerScript waveManager;
    public timeManagerScript timeManager;
    public timelineManagerScript timelineManager;
    public audioManagerScript audioManager;

    public MainInput_Ray mainInput_Ray;
    private LayerMask MouseArea;
    private Camera mainCamera;
    public GameObject[] allCanvas;

    [SerializeField]
    NewFlagDic eventDict;
    //for keeping track of important events
    Dictionary<int, bool> flagDict;

    public GameState state;
    private GameState prevState;
    public bool isCardDragged;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        flagDict = eventDict.toDictionary();
        findManagers();
        mainInput_Ray = new MainInput_Ray();
        mainInput_Ray.hit = gameObject;
        MouseArea = LayerMask.GetMask("Mouse");

        findCanvas();

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

    public void FlagTrigger(int ID)
    {
        //flagDict[ID] = true;
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
        if (ID > 20000 && ID < 21000)
        {
            flagDict[ID] = !flagDict[ID];
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

    public bool FlagInfo(int ID)
    {
        return (flagDict[ID]);
    }

    public void UpdateGameState(GameState newState)
    {
        if (state == GameState.PauseMenu)
        {
            state = prevState;
            return;
        }
        switch (newState)
        {
            case GameState.Wave:
                timeManager.resetTime();
                break;
            case GameState.TowerMenu:
                break;
            case GameState.ShopMenu:
                break;
            case GameState.PauseMenu:
                timeManager.pauseTime();
                prevState = state;
                break;
            default:
                break;
        }
        state = newState;
    }

    public void MiddleMan(Managers managerCalled, string voidCalled, int IDCalled)
    {
        switch (managerCalled)
        {
            case Managers.Game:
                gameControllerScript.instance.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Resource:
                gameControllerScript.instance.resourceManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Time:
                gameControllerScript.instance.timeManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Deck:
                gameControllerScript.instance.deckManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Wave:
                gameControllerScript.instance.waveManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Timeline:
                gameControllerScript.instance.timelineManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Audio:
                gameControllerScript.instance.audioManager.SendMessage(voidCalled, IDCalled);
                break;
            default:
                break;
        }
    }

    public void ResetGame()
    {
        //scene manager to reset scene
    }
    void findManagers()
    {
        resourceManager = GameObject.Find("resourceManager").GetComponent<resourceManagerScript>();
        deckManager = GameObject.Find("deckManager").GetComponent<deckManagerScript>();
        waveManager = GameObject.Find("waveManager").GetComponent<waveManagerScript>();
        timeManager = GameObject.Find("timeManager").GetComponent<timeManagerScript>();
        timelineManager = GameObject.Find("timelineManager").GetComponent<timelineManagerScript>();
        audioManager = GameObject.Find("audioManager").GetComponent<audioManagerScript>();
    }

    void findCanvas()
    {
        List<GameObject> allCanvasList = new List<GameObject>();
        allCanvasList.Add(GameObject.Find("UI Canvas"));
        allCanvasList.Add(GameObject.Find("Game Menu Canvas"));
        allCanvasList.Add(GameObject.Find("Hand Canvas"));
        allCanvasList.Add(GameObject.Find("Top Canvas"));
        allCanvasList.Add(GameObject.Find("Stop Canvas"));
        allCanvas = allCanvasList.ToArray();
    }
}