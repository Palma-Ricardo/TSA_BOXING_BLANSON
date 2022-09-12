using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;


public enum GameState {INIT, START, PLAYING, WINNER};


public class Game_Manager : MonoBehaviour
{
    public static string TheWinner;

    public static bool KeyboardEnabled = true;
    public static int DeathCount = 0;

    [SerializeField]
    public static List<GameObject> Players = new List<GameObject>();
    public GameState State;
    public Transform P1Spawn;
    public Transform P2Spawn;

    private GameObject ActiveP1;
    private GameObject ActiveP2;
    public GameObject P1;
    public GameObject P2;

    public GameObject Cameras;
    public GameObject WinnerCam;

    ///Start 
    public TextMeshProUGUI text;
    public TextMeshProUGUI Winnertext;
    public float matchtime;
    public GameObject UI;
    public GameObject WinnerUI;
    public GameObject CountDown;
    public GameObject Referee;




    // Start is called before the first frame update
    void Start()
    {
        State = GameState.INIT;
        Cameras.SetActive(false);
        //text.enabled = false;
        WinnerCam.SetActive(false);
        //UI.SetActive(false);
        WinnerUI.SetActive(false);
        CountDown.SetActive(false);
        Referee.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case GameState.INIT:
                InitState();
                break;
            case GameState.START:
                StartCoroutine(StartState());
                break;
            case GameState.PLAYING:
                StartCoroutine(Playing());
                break;
            case GameState.WINNER:
                Winner();
                break;
            default:
                break;  
        }
    }


    void InitState()
    {
        Time.timeScale = 1;
        KeyboardEnabled = false;

        //Clear Players List
        if (Players.Count > 0)
            Players.Clear();

        //Add Instances To Players List
        ActiveP1 = GameObject.Instantiate(P1, P1Spawn);
        //ActiveP2 = GameObject.Instantiate(P2, P2Spawn);
        ActiveP1.gameObject.name = "Player1";
        //ActiveP2.gameObject.name = "Player2";

        Players.Add(ActiveP1);
        //Players.Add(ActiveP2);
        //Add The Enemy

        if(Players.Count >= 1) //Todo: Change this back to 2
        {
            State = GameState.START;
        }
        else
        {
            Debug.Log("There Arn't Enough Players");
        }
    }

    IEnumerator StartState()
    {
        Cameras.SetActive(true);
        //text.enabled = true;

        CinemachineVirtualCamera P1Cam = GameObject.FindWithTag("P1").GetComponent<CinemachineVirtualCamera>();
        //CinemachineVirtualCamera P2Cam = GameObject.FindWithTag("P2").GetComponent<CinemachineVirtualCamera>();
        P1Cam.Follow = ActiveP1.transform;
        P1Cam.LookAt = ActiveP1.transform;
        //P2Cam.Follow = ActiveP2.transform;
        //P2Cam.LookAt = ActiveP2.transform;

        CountDown.SetActive(true);
        Referee.SetActive(true);
        int Countdown = 3;
        //text.text = Countdown.ToString();
        while (Countdown != 0)
        {
            
            yield return new WaitForSeconds(2);
            Countdown -= 1;
            //text.text = Countdown.ToString();
        }
        //text.text = "GO!";

        yield return new WaitForSeconds(0.1f);
        Referee.SetActive(false);
        CountDown.SetActive(false);
        //text.enabled = false;
        State = GameState.PLAYING;
    }

    IEnumerator Playing()
    {
        KeyboardEnabled = true;
        //UI.SetActive(true);

        Debug.Log(Players.Count);
        Debug.Log(DeathCount);
        while (Players.Count >= 2 && DeathCount <= 0)
        {
            
            //Players.Add(GameObject.FindGameObjectWithTag("P1"));
            yield return new WaitForSeconds(1);
            matchtime = Time.time;
        }
        matchtime = 0;
        State = GameState.WINNER;
    }

    void Winner()
    {
        KeyboardEnabled = false;
        //UI.SetActive(false);
        WinnerUI.SetActive(true);
        Cameras.SetActive(false);
        WinnerCam.SetActive(true);
        Winnertext.text = (TheWinner+" GOT KNOCKED OUT!");
        //Open Winner Menu
        DeathCount = 0;
    }
}
