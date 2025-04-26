using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public enum targettingOption
{
    Enemy,
    Ally,
    Road,
    Random,
    None,
}
[System.Serializable]
public enum GameState
{
    Wave,
    BetweenWave,
    Placing,
    TowerMenu,
    ShopMenu,
    PauseMenu,
    Previous
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
    SFX,
    Music
}
[System.Serializable]
public enum StatType
{
    Damage,
    Pierce,
    SPA,
    Range,
    Growth
}
[System.Serializable]
public enum EffectType
{
    Passive,
    Destroy,
    Summon,
    Hit,
    WaveStart,
    WaveEnd,
    PPassive,
    PDestroy,
    PHit,
    PSummon
}
[System.Serializable]
public enum ProjectileTargetOptions
{
    Tower,
    Closest
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
public class StateMask
{
    public GameState state;
    public string[] masks;
    public StateMask newStateMask(GameState newState, string[] newMasks)
    {
        StateMask current = new StateMask();
        current.state = newState;
        current.masks = newMasks;
        return current;
    }
}
[System.Serializable]
public class NewStateMaskDic
{
    public StateMask[] dictItems;
    public Dictionary<GameState, LayerMask> toDictionary()
    {
        Dictionary<GameState, LayerMask> newDict = new Dictionary<GameState, LayerMask>();
        
        foreach (StateMask item in dictItems)
        {
            LayerMask newMask = LayerMask.GetMask(item.masks);
            newDict.Add(item.state, newMask);
        }

        return newDict;
    }
}

[System.Serializable]
public class Wave
{
    public int ID;
    public int endGold;
    public int waveNum;
    //public bool isFinalWave;
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
    public float baseVolume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource[] source;

    public SoundType type;
}
[System.Serializable]
public class EffectAndReq
{
    public SOC_Effect effect;

    public SOC_Requirement[] req;
    public bool CheckRequirement(baseTowerScript tScript, sumObj_Base oScript)
    {
        bool tempBool = true;
        foreach (SOC_Requirement req in req)
        {
            if (!req.ApplyRequirement(tScript, oScript))
            {
                tempBool = false;
            }
        }
        return tempBool;
    }
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
    [SerializeField]
    NewStateMaskDic maskDict;
    Dictionary<GameState, LayerMask> stateMaskDict;
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

        stateMaskDict = maskDict.toDictionary();

        StartCoroutine(startAfterLoad());
    }
    void Update()
    {
        /*PointerEventData eventData = new PointerEventData(EventSystem.current);
        mainInput_Ray.hit = eventData.pointerCurrentRaycast.gameObject;
        Debug.DrawRay(eventData.pointerCurrentRaycast.screenPosition, Vector3.forward, Color.black, 3f);
        Debug.Log(mainInput_Ray.hit);*/
        mainInput_Ray.ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mainInput_Ray.hittingSomething = Physics2D.Raycast(mainInput_Ray.ray.origin, mainInput_Ray.ray.direction, 1000, stateMaskDict[state]);
        if (mainInput_Ray.hittingSomething)
        {
            mainInput_Ray.hit = Physics2D.Raycast(mainInput_Ray.ray.origin, mainInput_Ray.ray.direction, 1000, stateMaskDict[state]).transform.gameObject;
        }
        else
        {
            mainInput_Ray.hit = null;
        }
    }

    public void FlagTrigger(int ID)
    {
        //spawns the pre-set waves
        //second number should be 1 number before max wave
        if (ID > 11000 && ID < 12000)
        {
            waveManager.spawnWave(ID + 1);
            deckManager.drawCard(1);
            if (ID % 5 == 0)
            {
                deckManager.openShop();
            }
        }
        //flippable flags
        if (ID > 20000 && ID < 21000)
        {
            flagDict[ID] = !flagDict[ID];
        }
        //each event an ID would trigger, use for really important events
        switch (ID)
        {
            case 20004:
                //insert game win shit
                break;
            case 20003:
                //insert game loss shit
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
        switch (newState)
        {
            case GameState.Wave:
                timeManager.resetTime();
                if (state == GameState.TowerMenu || state == GameState.Placing || state == GameState.ShopMenu)
                {
                    prevState = GameState.Wave;
                    return;
                }
                break;
            case GameState.BetweenWave:
                timeManager.buttonChange(-1);
                if (state == GameState.TowerMenu || state == GameState.Placing || state == GameState.ShopMenu)
                {
                    prevState = GameState.BetweenWave;
                    return;
                }
                break;
            case GameState.Placing:
                prevState = state;
                break;
            case GameState.TowerMenu:
                prevState = state;
                break;
            case GameState.ShopMenu:
                prevState = state;
                break;
            case GameState.PauseMenu:
                timeManager.pauseTime();
                prevState = state;
                break;
            case GameState.Previous:
            state = prevState;
                return;
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

    IEnumerator startAfterLoad()
    {
        yield return null;
        findCanvas();

        UpdateGameState(GameState.BetweenWave);

        timeManager.resetTime();
        waveManager.spawnWave(11001);
        timeManager.ChangeFps(60);
    }
}