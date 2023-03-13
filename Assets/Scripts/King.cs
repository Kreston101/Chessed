using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    private int[] directions = { 8, -8, -1, 1, 7, 9, -9, -7 };

    public GameManager gm;
    public Pieces pieceScript;
    [HideInInspector]
    public Vector3 posAtTurnStart;
    public List<GameObject> positions;
    public bool kingSide, queenSide;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        pieceScript = GetComponent<Pieces>();
    }

    public void OnMouseDown()
    {
        if (pieceScript.canMove)
        {
            positions.Clear();
            int i;
            gm.PieceSelected(gameObject);
            for (i = 0; i < gm.tiles.Count; i++)
            {
                if (transform.IsChildOf(gm.tiles[i].transform))
                {
                    CalculateMovement(i);
                    //print check
                    Debug.Log(positions.Count);
                    gm.MoveLocation(positions);
                }
            }
        }
    }

    //u have that array of directions
    //so just make a list of everything that can be moved to based on each direction
    //something in the way? stop calculating that direction (but include the tile)
    //out of board? stop also
    //still alive?
    public void CalculateMovement(int currentTileIndex)
    {
        //going up
        int i = currentTileIndex + directions[0];
        if(i <= 63)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //going down
        i = currentTileIndex + directions[1];
        if (i >= 0)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //going left
        i = currentTileIndex + directions[2];
        if (i >= 0)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //going right
        i = currentTileIndex + directions[3];
        if (i <= 63)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //up left
        i = currentTileIndex + directions[4];
        if (i <= 63)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //up right
        i = currentTileIndex + directions[5];
        if (i <= 63)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //down left
        i = currentTileIndex + directions[6];
        if (i >= 0)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //down right
        i = currentTileIndex + directions[7];
        if (i >= 0)
        {
            if (gm.tiles[i].transform.childCount == 0)
            {
                Debug.Log(gm.tiles[i].transform.childCount);
                positions.Add(gm.tiles[i]);
            }
            else if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
            {
                Debug.Log("this is a kill tile");
                positions.Add(gm.tiles[i]);
            }
        }

        //check if king moved
        //check if rooks moved
        if(pieceScript.takenFirstMove == false)
        {
            if (pieceScript.isWhite == true)
            {
                if (kingSide)
                {
                    positions.Add(gm.tiles[7]);
                }

                if(queenSide)
                {
                    positions.Add(gm.tiles[0]);
                }
            }
            else if(pieceScript.isWhite == false)
            {
                if (kingSide)
                {
                    positions.Add(gm.tiles[63]);
                }

                if (queenSide)
                {
                    positions.Add(gm.tiles[56]);
                }
            }
        }
    }
}
