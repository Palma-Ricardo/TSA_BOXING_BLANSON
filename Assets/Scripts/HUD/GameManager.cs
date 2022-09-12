using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Level/Player Creation")]
    List<GameObject> Players = new List<GameObject>();
    public GameObject P1;
    public GameObject P2;

    public bool P1Ready;
    public bool P2Ready;

    public GameObject P1Cam;
    public GameObject P1SCam;
    public GameObject P2Cam;

    public Transform P1Spawn;
    public Transform P2Spawn;

    private GameObject ActiveP1;
    private GameObject ActiveP2;
    private GameObject ActiveP1Cam;
    private GameObject ActiveP2Cam;

    public GameObject HoverCam;
    public GameObject HoverCamBrain;
    public GameObject HUD;
    public GameObject ReadyHUD;

    public GameObject GameOver;

    public string Winner;
    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
        HUD.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        isOver();
    }

    void Create()
    {
        //Clear Players List
        if(Players.Count > 0)
            Players.Clear();

        //Create HUD
        HUD.SetActive(true);

        //Disable Hover Cam
        HoverCam.SetActive(false);
        HoverCamBrain.SetActive(false);

        

        //Add Instances To Players List
        Players.Add(ActiveP1);

       if (P2) //If splitscreen
        {
            ActiveP2 = GameObject.Instantiate(P2, P2Spawn);
            ActiveP2Cam = GameObject.Instantiate(P2Cam, P2Spawn);
            Players.Add(ActiveP2);

            ActiveP1 = GameObject.Instantiate(P1, P1Spawn);
            ActiveP1Cam = GameObject.Instantiate(P1Cam, P1Spawn);
        }
        else
        {
            ActiveP1 = GameObject.Instantiate(P1, P1Spawn);
            ActiveP1Cam = GameObject.Instantiate(P1SCam, P1Spawn);
            Players.Add(ActiveP1);

            //Reset HUD Location Since Its Single player
            HUD.transform.position = new Vector3(HUD.transform.position.x, 355, HUD.transform.position.z);
        }

        

    }

    public void Ready()
    {
        //Set Players To Ready
        P1Ready = true;
        P2Ready = true;

        //Check if Both Players Ready
        if(P1Ready && P2Ready)
        {
            //Play Countdown
            StartCoroutine(WaitNDisable(3f, ReadyHUD));
            //Start Game/ Create Players
            Create();
        }
       

    }

    void isOver()
    {
        //Check If Only One Player Remains
        if(GameObject.FindGameObjectWithTag("P1")&& GameObject.FindGameObjectWithTag("P2"))
        {
            
        }
        else
        {
            //Declare Winner
            if (GameObject.FindGameObjectWithTag("P1"))
            {
                Winner = GameObject.FindGameObjectWithTag("P1").gameObject.name;
            }
            if (GameObject.FindGameObjectWithTag("P2"))
            {
                Winner = GameObject.FindGameObjectWithTag("P2").gameObject.name;
            }
            //Enable Hover Cam
            HoverCam.SetActive(true);
            HoverCamBrain.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            //open gameover menu
            GameOver.SetActive(true);
            HUD.SetActive(false);
            GameOver.GetComponentInChildren<TMPro.TMP_Text>().SetText(Winner+" Wins!");
        }

    }

    void ResetAll()
    {
        //Destroy GameObjects
        Destroy(ActiveP1);
        if (ActiveP2)
        {
            Destroy(ActiveP2);
            Destroy(ActiveP2Cam);
        }
        
        Destroy(ActiveP1Cam);
        
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator WaitNDisable(float time, GameObject Item)
    {
        yield return new WaitForSeconds(time);
        Item.SetActive(false);
         
    }
}
