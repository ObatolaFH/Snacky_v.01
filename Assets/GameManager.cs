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

    public enum GhostMode
    {
        chase, 
        scatter
    }

    public GhostMode currentGhostMode;

    // Start is called before the first frame update
    void Awake()
    {
        score= 0;
        currentMunch= 0;

        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<SnackController>().isGhostStartingNode = true;


        snacky = GameObject.Find("Player");
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
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
        
        AddToScore(1);
    
    }

}
