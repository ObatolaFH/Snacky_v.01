using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject currentSnack;
    public float speed = 4f;

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
    }
}
