using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    
    public bool LevelUp = false;

    public bool newGame;
    public bool clearedLevel;

    public AudioSource startGameAudio;
    public AudioSource death;

    public int lives;
    public int level;
    public int template;

    public Image popUpNextLevel;
    public Button homeButton;
    public Button nextLevelButton;

    public Image popUpGameOver;
    public Button retryButton;

    public Image life01;
    public Image life02;
    public Image life03;

    //public TextMeshPro successText;

    public enum GhostMode
    {
        chase, 
        scatter
    }

    public GhostMode currentGhostMode;

    // Start is called before the first frame update
    void Awake()
    {
        level = MainMenu.level;
        template = MainMenu.template;

        print(MainMenu.level);

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
            snacksThatAreLeft = totalSnacks;
            waitTimer = 4;
            //Snack will respawn when Snacky starts a new level or restarts the game
            for (int i = 0; i < snackControllers.Count; i++)
            {
                snackControllers[i].RespawnSnack();
            }
        }
        popUpNextLevel.enabled = false;
        homeButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        popUpGameOver.enabled = false;
        retryButton.gameObject.SetActive(false);

        if (newGame)
        {
            startGameAudio.Play();
            score = -10;
            scoreText.text = score.ToString();
            lives = 3;
            //level = 2;
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
        siren.Play();
    }

    void StopGame()
    {
        gameIsRunning = false;
        siren.Stop();
        if(clearedLevel) 
        {
            popUpNextLevel.enabled = true;
            homeButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            snacky.GetComponent<PlayerController>().Stop();
        }

        if (lives <= 0)
        {
            popUpGameOver.enabled = true;
            homeButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            snacky.GetComponent<PlayerController>().Stop();
        }
        
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

    public IEnumerator collectedPoint(SnackController snackController) { 

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

        /*
        if (score == possiblePoints)
        {
            Level++;
            LevelUp = true;
            print("LevelUP to Level:" + Level);

        }
        */

        if (snacksThatAreLeft == 0)
        {
            level++;
            MainMenu.level++;
            print(MainMenu.level);
            clearedLevel = true;
            StopGame();

            if (!clearedLevel)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(Setup());
            }
        }
    }
    public IEnumerator PlayerEaten()
    {
        lives--;

        if (lives == 2)
        {
            life03.enabled = false;
        }
        else if (lives == 1)
        {
            life02.enabled = false;
        }
        else
        {
            life01.enabled = false;
        }

        hadDeathOnThisLevel = true;
        StopGame();
        yield return new WaitForSeconds(1);

        redGhostController.SetVisible(false);
        blueGhostController.SetVisible(false);
        orangeGhostController.SetVisible(false);
        pinkGhostController.SetVisible(false);

        snacky.GetComponent<PlayerController>().Death();
        death.Play();
        yield return new WaitForSeconds(3);

        if (lives > 0) 
        {
            StartCoroutine(Setup());
        }
 
    }
}
