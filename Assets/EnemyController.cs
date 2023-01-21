using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum GhostNodeStatesEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public GhostNodeStatesEnum ghostNodeState;
    public GhostNodeStatesEnum respawnState;

    public enum GhostType
    {
        red,
        blue,
        pink,
        orange
    }

    public GhostType ghostType;

    public GameObject ghostNodeStart;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;

    public MovementController movementController;

    public GameObject startingNode;

    public bool readyToLeaveHome = false;

    public GameManager gameManager;

    public bool testRespawn = false;

    public bool isFrightened = false;

    public GameObject[] scatterNodes;
    public int scatterNodeIndex;

    // Start is called before the first frame update
    void Awake()
    {
        scatterNodeIndex = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = GetComponent<MovementController>();
        

        if (ghostType == GhostType.red)
        {
            ghostNodeState = GhostNodeStatesEnum.startNode;
            respawnState = GhostNodeStatesEnum.centerNode;
            startingNode = ghostNodeStart;
            readyToLeaveHome = true;
        }
        else if( ghostType == GhostType.pink)
        {
            ghostNodeState = GhostNodeStatesEnum.centerNode;
            respawnState = GhostNodeStatesEnum.centerNode;
            startingNode = ghostNodeCenter;

        }
        else if (ghostType == GhostType.blue)
        {
            ghostNodeState = GhostNodeStatesEnum.leftNode;
            respawnState = GhostNodeStatesEnum.leftNode;
            startingNode = ghostNodeLeft;
        }
        else if (ghostType == GhostType.orange)
        {
            ghostNodeState = GhostNodeStatesEnum.rightNode;
            respawnState = GhostNodeStatesEnum.rightNode;
            startingNode = ghostNodeRight;
        }
        movementController.currentSnack = startingNode;
        transform.position = startingNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (testRespawn)
        {
            readyToLeaveHome = false;
            ghostNodeState = GhostNodeStatesEnum.respawning;
            testRespawn = false;
        }
    }

    public void ReachedCenterOfNode(SnackController snackController)
    {
        if (ghostNodeState == GhostNodeStatesEnum.movingInNodes)
        {
            //scatter mode
            if (gameManager.currentGhostMode == GameManager.GhostMode.scatter)
            {
                DetermineGhostScatterModeDirection();
            }
            else if (isFrightened)
            {
                print("hi");
            }
            //chase mode
            else
            {
                //Determine next game node to go to 
                if (ghostType == GhostType.red)
                {
                    DetermineRedGhostDirection();
                }
                else if (ghostType == GhostType.pink)
                {
                    DeterminePinkGhostDirection();
                }
                else if (ghostType == GhostType.blue)
                {
                    DetermineBlueGhostDirection();
                }
                else if (ghostType == GhostType.orange)
                {
                    DetermineOrangeGhostDirection();
                }
            }
            
        }
        else if (ghostNodeState == GhostNodeStatesEnum.respawning)
        {
            string direction = "";

            //we have reached our start move, move to the center node
            if (transform.position.x == ghostNodeStart.transform.position.x && transform.position.y == ghostNodeStart.transform.position.y)
            {
                direction = "down";
            }
            //we have reached the center node, either finish respawn or go to left/right node
            else if (transform.position.x == ghostNodeCenter.transform.position.x && transform.position.y == ghostNodeCenter.transform.position.y)
            {
                if (respawnState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = respawnState;
                }
                else if (respawnState == GhostNodeStatesEnum.leftNode)
                {
                    direction = "left";
                }
                else if (respawnState == GhostNodeStatesEnum.rightNode)
                {
                    direction = "right";
                }
            }
            //if the respawn state is either the left or right node and its reached, leave home again
            else if ((transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
                    || (transform.position.x == ghostNodeRight.transform.position.x && transform.position.y == ghostNodeRight.transform.position.y))
            {
                ghostNodeState = respawnState;
            }
            //We are still on the gameboard locate our start node
            else 
            {
                //Determine quickest direction to home
                direction = GetClosestDirection(ghostNodeStart.transform.position);
            }

            movementController.SetDirection(direction);
        }
        else
        {
            //if we are ready to leave home
            if (readyToLeaveHome)
            {   
                //if we are in the left node move to the center
                if (ghostNodeState == GhostNodeStatesEnum.leftNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("right");
                }
                else if (ghostNodeState == GhostNodeStatesEnum.rightNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.centerNode;
                    movementController.SetDirection("left");
                }
                else if (ghostNodeState == GhostNodeStatesEnum.centerNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.startNode;
                    movementController.SetDirection("up");
                }
                else if (ghostNodeState == GhostNodeStatesEnum.startNode)
                {
                    ghostNodeState = GhostNodeStatesEnum.movingInNodes;
                    movementController.SetDirection("left");
                }
            }
        }
    }

    void DetermineGhostScatterModeDirection()
    {
        string direction = GetClosestDirection(scatterNodes[scatterNodeIndex].transform.position);
        //scatterNodeIndex++;
        movementController.SetDirection(direction);
    }

    void DetermineRedGhostDirection()
    {
        string direction = GetClosestDirection(gameManager.snacky.transform.position);
        movementController.SetDirection(direction);
    }

    void DeterminePinkGhostDirection()
    {
        string snackyDirection = gameManager.snacky.GetComponent<MovementController>().lastMovingDirection;
        float distanceBetweenNodes = 0.37f;

        Vector2 target = gameManager.snacky.transform.position;
        if (snackyDirection == "left")
        {
            target.x -= distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "right")
        {
            target.x += distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "up")
        {
            target.y += distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "down")
        {
            target.y -= distanceBetweenNodes * 2;
        }

        string direction = GetClosestDirection(target);
        movementController.SetDirection(direction);
    }

    void DetermineBlueGhostDirection()
    {
        string snackyDirection = gameManager.snacky.GetComponent<MovementController>().lastMovingDirection;
        float distanceBetweenNodes = 0.37f;

        Vector2 target = gameManager.snacky.transform.position;
        if (snackyDirection == "left")
        {
            target.x -= distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "right")
        {
            target.x += distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "up")
        {
            target.y += distanceBetweenNodes * 2;
        }
        else if (snackyDirection == "down")
        {
            target.y -= distanceBetweenNodes * 2;
        }

        GameObject redGhost = gameManager.redGhost;
        float xDistance = target.x - redGhost.transform.position.x;
        float yDistance = target.y - redGhost.transform.position.y;

        Vector2 blueTarget = new Vector2(target.x + xDistance, target.y + yDistance);
        string direction = GetClosestDirection(blueTarget);
        movementController.SetDirection(direction);
    }

    void DetermineOrangeGhostDirection()
    {
        float distance = Vector2.Distance(gameManager.snacky.transform.position, transform.position);
        float distanceBetweenNodes = 0.37f;

        if (distance < 0)
        {
            distance *= -1;
;        }

        //if we are within 8 nodes of snacky, chase him using red's logic
        if (distance <= distanceBetweenNodes * 8)
        {
            DetermineRedGhostDirection();
        }
        //otherwise use scatter mode logic
        else
        {
            //scatter mode
            DetermineGhostScatterModeDirection();
        }
    }

    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";
        SnackController snackController = movementController.currentSnack.GetComponent<SnackController>();

        if (snackController.canMoveUp && lastMovingDirection != "down")
        {
            GameObject snackUp = snackController.snackUp;
            //get the distance between our top node and snacky
            float distance = Vector2.Distance(snackUp.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (snackController.canMoveDown && lastMovingDirection != "up")
        {
            GameObject snackDown = snackController.snackDown;
            //get the distance between our top node and snacky
            float distance = Vector2.Distance(snackDown.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (snackController.canMoveLeft && lastMovingDirection != "right")
        {
            GameObject snackLeft = snackController.snackLeft;
            //get the distance between our top node and snacky
            float distance = Vector2.Distance(snackLeft.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (snackController.canMoveRight && lastMovingDirection != "left")
        {
            GameObject snackRight = snackController.snackRight;
            //get the distance between our top node and snacky
            float distance = Vector2.Distance(snackRight.transform.position, target);

            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        return newDirection;
    }
}
