using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnackController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject snackLeft;
    public GameObject snackRight;
    public GameObject snackUp;
    public GameObject snackDown;

    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    public GameManager gameManager;
    public bool hasPoint = true;

    public bool isGhostStartingNode = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            hasPoint = false;
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        RaycastHit2D[] hitsDown;
        //Raycast going down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);
        for (int i = 0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            if (distance < 0.5f && hitsDown[i].collider.gameObject.tag == "mazeObj")
            {
                canMoveDown = true;
                snackDown = hitsDown[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsUp;
        //Raycast going up
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);
        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            if (distance < 0.5f && hitsUp[i].collider.gameObject.tag == "mazeObj")
            {
                canMoveUp = true;
                snackUp = hitsUp[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsRight;
        //Raycast going right
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            if (distance < 0.5f && hitsRight[i].collider.gameObject.tag == "mazeObj")
            {
                canMoveRight = true;
                snackRight = hitsRight[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsLeft;
        //Raycast going left
        hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left);
        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            if (distance < 0.5f && hitsLeft[i].collider.gameObject.tag == "mazeObj")
            {
                canMoveLeft = true;
                snackLeft = hitsLeft[i].collider.gameObject;
            }
        }

        if (isGhostStartingNode)
        {
            canMoveDown = true;
            snackDown = gameManager.ghostNodeCenter;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetSnackFromDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return snackLeft;
        }
        else if (direction == "right" && canMoveRight)
        {
            return snackRight;
        }
        else if (direction == "down" && canMoveDown)
        {
            return snackDown;
        }
        else if (direction == "up" && canMoveUp)
        {
            return snackUp;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasPoint)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            hasPoint = false;

            gameManager.collectedPoint(this);

        }
    }
}
