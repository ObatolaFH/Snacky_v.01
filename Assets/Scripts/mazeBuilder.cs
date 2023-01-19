using System;
using UnityEngine;

public class mazeBuilder : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;
    //public Sprite test;
    public Transform parent;
    public float factor;

    //public Sprite one;
    // Start is called before the first frame update
    int[,] mazeAr = new int[27, 25] {
                                    { 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4 },
                                    { 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1, 0, 1, 4, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 6, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 0, 1, 1, 1, 1, 1, 1, 5 },
                                    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    { 3, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 0, 1, 1, 1, 1, 1, 1, 4 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 1, 1, 1, 5, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17, 16, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                                    { 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5 }
    };
    void Start()
    {
        builder(mazeAr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void builder(int[,] maze)
    {
        GameObject[] mazeObjects = GameObject.FindGameObjectsWithTag("mazeObj");
        for (int i = 0; i < mazeObjects.Length; i++)
        {
            print(mazeObjects[i].name + ", " + i);
        }


        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Vector3 eulerRotation = new Vector3(mazeObjects[maze[x, y]].transform.eulerAngles.x, mazeObjects[maze[x, y]].transform.eulerAngles.y, mazeObjects[maze[x, y]].transform.eulerAngles.z);


                GameObject clone = Instantiate(mazeObjects[maze[x, y]], new Vector3(transform.position.x + (y * factor), transform.position.y - (x * factor), 0), Quaternion.Euler(eulerRotation));
                clone.name = mazeObjects[maze[x, y]].name + x + " " + y;
                clone.transform.parent = parent;
            }
        }
    }
}
