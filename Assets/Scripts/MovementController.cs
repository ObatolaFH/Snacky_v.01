using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject currentSnack;
    public float speed = 3.25f;

    public string direction = "";
    public string lastMovingDirection = "";

    public bool canWarp = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SnackController currentSnackController = currentSnack.GetComponent<SnackController>();

        transform.position = Vector2.MoveTowards(transform.position, currentSnack.transform.position, speed * Time.deltaTime);

        bool reverseDirection = false;
        if (
           (direction == "left" && lastMovingDirection == "right")
           || (direction == "right" && lastMovingDirection == "left")
           || (direction == "up" && lastMovingDirection == "down")
           || (direction == "down" && lastMovingDirection == "up")
           )
        {
            reverseDirection = true;
        }

        //Figure out if we are at the center of the current snack
        if (transform.position.x == currentSnack.transform.position.x && transform.position.y == currentSnack.transform.position.y || reverseDirection)
        {
            if (currentSnackController.isWarpLeftNode && canWarp)
            {
                currentSnack = gameManager.rightWarpNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = currentSnack.transform.position;
                canWarp= false;
            }
            else if (currentSnackController.isWarpRightNode && canWarp)
            {
                currentSnack = gameManager.leftWarpNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = currentSnack.transform.position;
                canWarp= false;
            }
            else {
                GameObject newSnack = currentSnackController.GetSnackFromDirection(direction);
                if (newSnack != null)
                {
                    currentSnack = newSnack;
                    lastMovingDirection = direction;
                }
                else
                {
                    direction = lastMovingDirection;
                    newSnack = currentSnackController.GetSnackFromDirection(direction);
                    if (newSnack != null)
                    {
                        currentSnack = newSnack;
                    }
                }
            }
              
        }
        else
        {
            canWarp = true;
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
