using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject snacky;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;

    public enum GhostMode
    {
        chase, 
        scatter
    }

    public GhostMode currentGhostMode;

    public int PointCounter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        currentGhostMode = GhostMode.chase;
        ghostNodeStart.GetComponent<SnackController>().isGhostStartingNode = true;


        snacky = GameObject.Find("Player");
        siren.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
