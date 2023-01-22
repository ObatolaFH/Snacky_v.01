using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject snacky;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;

    public int currentMunch = 0;

    public int score;
    public Text scoreText;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;

    public GameObject redGhost;
    public GameObject pinkGhost;
    public GameObject blueGhost;
    public GameObject orangeGhost;

    public EnemyController redGhostController;
    public EnemyController pinkGhostController;
    public EnemyController orangeGhostController;
    public EnemyController blueGhostController;

    public int totalSnacks;
    public int snacksThatAreLeft;
    public int snacksCollectedInThisLife;

    public bool hadDeathOnThisLevel = false;

    public bool gameIsRunning;

    public List<SnackController> snackControllers= new List<SnackController>();

    //Zoes Part
    public int possiblePoints;
    public int pointValue = 10;

    public int Level = 1;
    public bool LevelUp = false;

    public bool newGame;
    public bool clearedLevel;

    public AudioSource startGameAudio;
    public int lives;
    public int level;

    public enum GhostMode
    {
        chase, 
        scatter
    }

    public GhostMode currentGhostMode;

    // Start is called before the first frame update
    void Awake()
    {
        newGame = true;
        clearedLevel = false;

        redGhostController = redGhost.GetComponent<EnemyController>();
        pinkGhostController = pinkGhost.GetComponent<EnemyController>();
        orangeGhostController = orangeGhost.GetComponent<EnemyController>();
        blueGhostController = blueGhost.GetComponent<EnemyController>();

  
        ghostNodeStart.GetComponent<SnackController>().isGhostStartingNode = true;

        snacky = GameObject.Find("Player");
        
        
        possiblePoints = 5 * pointValue;

        StartCoroutine(Setup());
    }

    public IEnumerator Setup()
    {
        //if Snacky clears a level, a background will appear covering the level, and the game will pause for 0.1 seconds
        if (clearedLevel)
        {
            //Activate background
            yield return new WaitForSeconds(0.1f);
        }

        snacksCollectedInThisLife = 0;
        currentGhostMode = GhostMode.scatter;
        gameIsRunning = false;
        currentMunch = 0;

        float waitTimer = 1f;

        if (clearedLevel || newGame)
        {
            waitTimer = 4;
            //Snack will respawn when Snacky starts a new level or restarts the game
            for (int i = 0; i < snackControllers.Count; i++)
            {
                snackControllers[i].RespawnSnack();
            }
        }

        if (newGame)
        {
            startGameAudio.Play();
            score = 0;
            scoreText.text = score.ToString();
            lives = 3;
            level = 1;
        }
        
        snacky.GetComponent<PlayerController>().Setup();

        redGhostController.Setup();
        pinkGhostController.Setup();
        orangeGhostController.Setup();
        blueGhostController.Setup();

        newGame = false;
        clearedLevel = false;

        yield return new WaitForSeconds(waitTimer);

        StartGame();
    }

    void StartGame()
    {
        gameIsRunning = true;
        print("hi jj j ");
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotSnackFromNodeController(SnackController snackController)
    {
        snackControllers.Add(snackController);
        totalSnacks++;
        snacksThatAreLeft++;
    }
    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void collectedPoint(SnackController snackController) { 

        if(currentMunch == 0)
        {
            munch1.Play();
            currentMunch = 1;
        }
        else if(currentMunch == 1){
            munch2.Play();
            currentMunch = 0;
        }
        
        snacksThatAreLeft--;
        snacksCollectedInThisLife++;

        int requiredBlueSnacks = 0;
        int requiredOrangeSnacks = 0;

        if (hadDeathOnThisLevel)
        {
            requiredBlueSnacks = 12;
            requiredOrangeSnacks = 32;
        }
        else
        {
            requiredBlueSnacks = 30;
            requiredOrangeSnacks = 60;
        }

        if (snacksCollectedInThisLife >= requiredBlueSnacks && !blueGhost.GetComponent<EnemyController>().leftHomeBefore) 
        {
            blueGhost.GetComponent<EnemyController>().readyToLeaveHome = true;
        }

        if (snacksCollectedInThisLife >= requiredOrangeSnacks && !orangeGhost.GetComponent<EnemyController>().leftHomeBefore)
        {
            orangeGhost.GetComponent<EnemyController>().readyToLeaveHome = true;
        }

        AddToScore(1*pointValue);

        if (score == possiblePoints)
        {
            Level++;
            LevelUp = true;
            print("LevelUP to Level:" + Level);

        }
    }

}
