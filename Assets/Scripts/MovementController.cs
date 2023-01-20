using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject currentSnack;
    public float speed = 3.25f;

    public string direction = "";
    public string lastMovingDirection = "";

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (transform.position.x == currentSnack.transform.position.x && transform.position.y == currentSnack.transform.position.y)
        {
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

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
