using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hitsDown;
        //Raycast going down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);
        for (int i = 0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            if (distance < 0.5f)
            {
                canMoveDown = true;
                snackDown = hitsDown[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsUp;
        //Raycast going down
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);
        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            if (distance < 0.5f)
            {
                canMoveUp = true;
                snackUp = hitsUp[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsRight;
        //Raycast going down
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);
        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            if (distance < 0.5f)
            {
                canMoveRight = true;
                snackRight = hitsRight[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitsLeft;
        //Raycast going down
        hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left);
        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            if (distance < 0.5f)
            {
                canMoveLeft = true;
                snackLeft = hitsLeft[i].collider.gameObject;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
