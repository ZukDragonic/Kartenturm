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
public enum ImageSide
{
    Left,
    Right,
    None
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
    Previous,
    Loading,
    MainMenu
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
    Audio,
    Dialogue,
    Scene
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
public class SceneNameAndID
{
    public int ID;

    public string sceneName;
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
public class Route
{
    public List<GameObject> checkpointList;
}
[System.Serializable]
public class WholeDialogue
{
    public int ID;

    public SingleDialogue[] dialogue;
}
[System.Serializable]
public class SingleDialogue
{
    public string name;
    public Color textColor;
    [TextArea(3, 8)]
    public string text;

    public Sprite image;

    public ImageSide imageSide;
}
[System.Serializable]
public class WholeAnimation
{
    public int ID;
    public TimelineAsset timelineAnim;
    public TimelineObject[] boundObjects;
}
[System.Serializable]
public class TimelineObject
{
    public GameObject boundObjects;
    public bool findObjectInScene;
    public string objectInSceneName;
    public int boundTrack;
}

[System.Serializable]
public class EnemyAndDelay
{
    public int enemyID;
    public float delay;
    public int repeat;
}
[System.Serializable]
public class EnemyAttack
{
    public SOETargettingBase target;
    public SOC_Effect[] appliedEffect;
    public SOC_Effect[] endEffect; //the effect that reverses the previous effect

    public float startLag;
    public AnimationClip anim;
    public float endLag;
    public bool moveDuringAttack;
    public float timeTillNextAttack;
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
    public bool CheckRequirement(sumObj_Base script)
    {
        bool tempBool = true;
        foreach (SOC_Requirement req in req)
        {
            if (!req.ApplyRequirement(script))
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
    public dialogueManagerScript dialogueManager;
    public sceneManagerScript sceneManager;

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
    public GameState prevState;
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
        if (ID >= 11000 && ID < 12000)
        {
            if (waveManager.currentWave != null)
            {
                switch (ID)
                {
                    //stops spawning whenever a wave is a "final" wave
                    case 11105: //world 1-1 clear
                        FlagTrigger(21000);
                        //tell waveManager to spawn an endWave menu
                        return;
                    case 11112: //world 1-2 clear
                        FlagTrigger(21001);
                        //tell waveManager to spawn an endWave menu
                        break;
                    case 11122: //world 1-3 clear
                        FlagTrigger(21002);
                        //tell waveManager to spawn an endWave menu
                        break;
                    case 11132: //world 1-4 clear
                        FlagTrigger(21003);
                        //tell waveManager to spawn an endWave menu
                        break;
                    case 11142: //world 1-5 clear
                        FlagTrigger(21004);
                        //tell waveManager to spawn an endWave menu
                        break;
                    case 11160: //world 1-6 clear
                        FlagTrigger(21005);
                        //tell waveManager to spawn an endWave menu
                        break;
                    case 11181: //world 1-7 clear
                        FlagTrigger(21006);
                        //tell waveManager to spawn an endWave menu
                        break;
                    default:
                        break;
                }
            }


            waveManager.spawnWave(ID + 1);
            deckManager.drawCard(1);
            if (waveManager.findWave(ID + 1).waveNum % deckManager.shopWave == 0)
            {
                deckManager.openShop();
            }
            if (waveManager.findWave(ID + 1).waveNum == 1)
            {
                deckManager.drawCard(3);
            }
            if (FlagInfo(20002))
            {
                switch (ID)
                {
                    //spawns at the end of the current wave ID
                    case 11000:
                        dialogueManager.CreateDialogue(22001);
                        break;
                    case 11002:
                        dialogueManager.CreateDialogue(22002);
                        break;
                    case 11005:
                        dialogueManager.CreateDialogue(22003);
                        break;
                    case 11013:
                        dialogueManager.CreateDialogue(22004);
                        break;
                    case 11100:
                        dialogueManager.CreateDialogue(22005);
                        break;
                    default:
                        break;
                }
            }
        }
        //flippable flags
        if (ID >= 20000 && ID < 21000)
        {
            flagDict[ID] = !flagDict[ID];
        }
        //single flip flags
        if (ID >= 21000 && ID < 22000)
        {
            flagDict[ID] = true;
        }

        if (ID >= 23000 && ID < 24000)
        {
            //sceneManager stuff
            int waveWhenLoaded = 0;
            switch (ID)
            {
                case 23001:
                    UpdateGameState(GameState.Loading);
                    break;
                case 23002:
                    UpdateGameState(GameState.MainMenu);
                    break;
                case 23012: //world 1-1
                    waveWhenLoaded = 11100;
                    break;
                case 23013: //world 1-2
                    Debug.Log(1);
                    waveWhenLoaded = 11105;
                    break;
                case 23014: //world 1-3
                    waveWhenLoaded = 11112;
                    break;
                case 23015: //world 1-4
                    waveWhenLoaded = 11122;
                    break;
                case 23016: //world 1-5
                    waveWhenLoaded = 11132;
                    break;
                case 23017: //world 1-6
                    waveWhenLoaded = 11142;
                    break;
                case 23018: //world 1-7
                    waveWhenLoaded = 11160;
                    break;
                case 23100: //free play
                    waveWhenLoaded = 11000;
                    break;
                case 23999: //testing
                    waveWhenLoaded = 11112;
                    break;
                default:
                    break;
            }
            StartAction();
            if (waveWhenLoaded != 0) //if waveWhenLoaded is not zero, then a wave should be spawned;
            {
                UpdateGameState(GameState.BetweenWave);
                StartAction();
                timeManager.resetTime();
                deckManager.shuffleDeck();
                waveManager.currentWave = null;
                resourceManager.resetResources();
                FlagTrigger(waveWhenLoaded);
            }
           switch (ID)
            {
                default:
                    break;
            }
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
                prevState = GameState.Wave;
                if (state == GameState.TowerMenu || state == GameState.Placing || state == GameState.ShopMenu)
                {
                    return;
                }
                break;
            case GameState.BetweenWave:
                prevState = GameState.BetweenWave;
                if (state == GameState.TowerMenu || state == GameState.Placing || state == GameState.ShopMenu)
                {
                    return;
                }
                break;
            case GameState.Placing:
                break;
            case GameState.TowerMenu:
                break;
            case GameState.ShopMenu:
                //Could cause some bugs in the future
                if (FlagInfo(20001))
                {
                    prevState = GameState.Wave;
                }
                else
                {
                    prevState = GameState.BetweenWave;
                }
                break;
            case GameState.PauseMenu:
                timeManager.pauseTime();
                prevState = state;
                break;
            case GameState.Previous:
                if (prevState == GameState.TowerMenu)
                {
                    //there are certain state that must not be previous-ed
                    return;
                }
                state = prevState;
                return;
            case GameState.Loading:
                break;
            case GameState.MainMenu:
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
                resourceManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Time:
                timeManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Deck:
                deckManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Wave:
                waveManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Timeline:
                timelineManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Audio:
                audioManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Dialogue:
                dialogueManager.SendMessage(voidCalled, IDCalled);
                break;
            case Managers.Scene:
                sceneManager.SendMessage(voidCalled, IDCalled);
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
        dialogueManager = GameObject.Find("dialogueManager").GetComponent<dialogueManagerScript>();
        sceneManager = GameObject.Find("sceneManager").GetComponent<sceneManagerScript>();
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

        StartAction();

        FlagTrigger(20002);
    }

    public void StartAction()
    {
        findCanvas();

        deckManager.StartAction();
        timeManager.StartAction();
        resourceManager.StartAction();
        waveManager.StartAction();

        timeManager.ChangeFps(60);
    }
}