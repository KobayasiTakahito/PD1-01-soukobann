using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //’Ç‰Á
    public GameObject playerPrefab;
    int[,] map;
    GameObject[,] field;
    GameObject obj;


    //void PrintArray()
    //{
    //    string debugText = "";
    //    for(int i = 0;i < map.Length; i++)
    //    {
    //        debugText += map[i].ToString() + ",";
    //    }
    //    Debug.Log(debugText);
    //}

    Vector2Int GetPlayerIndex()
    {

        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == null)
                {
                    continue;
                }
                if (field[i, j].tag == "Player")
                {

                    return new Vector2Int(j, i);
                }
            }

        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveFrom + velocity);
            if (!success) { return false; }
        }

        field[moveTo.y, moveTo.x].transform.position =
            new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }




    // Start is called before the first frame update
    void Start()
    {

        // Debug.Log("Hallo world");
        map = new int[,] {
             { 1, 0, 2, 0, 0, 2, 2, 0 },
             { 0, 0, 2, 0, 0, 2, 2, 0 },
             { 0, 0, 2, 0, 0, 2, 2, 0 },
        };
        field = new GameObject
              [
              map.GetLength(0),
              map.GetLength(1)
  ];

        string debugText = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    GameObject instance = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                };


                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";
        }
        Debug.Log(debugText);

        // PrintArray();



    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int vectorFrom = new Vector2Int(-1,-1);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();


            MoveNumber("1", playerIndex, playerIndex - vectorFrom);
            //    PrintArray();

            //}
           
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber("1", playerIndex, playerIndex -vectorFrom);
            

        }

    }
}


