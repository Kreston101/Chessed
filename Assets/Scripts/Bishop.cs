using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    private int[] directions = { 0, 0, 0, 0, 7, 9, -9, -7 };

    public GameManager gm;
    public Pieces pieceScript;
    [HideInInspector]
    public Vector3 posAtTurnStart;
    public List<GameObject> positions;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        pieceScript = GetComponent<Pieces>();
    }

    private void OnMouseDown()
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
        if (directions[0] != 0)
        {
            int i = currentTileIndex;
            while (i <= 63)
            {
                i += directions[0];
                if (i <= 63)
                {
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        Debug.Log(gm.tiles[i].transform.childCount);
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        //going down
        if (directions[1] != 0)
        {
            int i = currentTileIndex;
            while (i >= 0)
            {
                i += directions[1];
                if (i >= 0)
                {
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        //going left
        if (directions[2] != 0)
        {
            //0%8 = 0 leftmost || 7%8 = 7 rightmost
            int tilesFromLeft = currentTileIndex % 8;
            for (int i = 1; i <= tilesFromLeft; i++)
            {
                if (gm.tiles[currentTileIndex - i].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[currentTileIndex - i]);
                }
                else
                {
                    if (gm.tiles[currentTileIndex - i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        Debug.Log("this is a kill tile");
                        positions.Add(gm.tiles[currentTileIndex - i]);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        //going right
        if (directions[3] != 0)
        {
            //0%8 = 0 leftmost || 7%8 = 7 rightmost
            int tilesFromRight = 7 - currentTileIndex % 8;
            for (int i = 1; i <= tilesFromRight; i++)
            {
                if (gm.tiles[currentTileIndex + i].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[currentTileIndex + i]);
                }
                else
                {
                    if (gm.tiles[currentTileIndex + i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        Debug.Log("this is a kill tile");
                        positions.Add(gm.tiles[currentTileIndex + i]);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        //up left
        if (directions[4] != 0)
        {
            int tilesFromLeft = currentTileIndex % 8; //index 59, 3 tiles
            int tileLayer = (currentTileIndex - tilesFromLeft) / 8; //eg layer 7 top
            int i = currentTileIndex;
            if (tileLayer != 7)
            {
                while (i % 8 != 0)
                {
                    if (i >= 56)
                    {
                        break;
                    }
                    i += directions[4];
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        //up right
        if (directions[5] != 0)
        {
            int tilesFromLeft = currentTileIndex % 8; //index 59, 3 tiles
            int tileLayer = (currentTileIndex - tilesFromLeft) / 8; //eg layer 7 top
            int i = currentTileIndex;
            if (tileLayer != 7)
            {
                while (i % 8 != 7)
                {
                    if (i >= 56)
                    {
                        break;
                    }
                    i += directions[5];
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            Debug.Log(gm.tiles[i].transform);
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        //down left
        if (directions[6] != 0)
        {
            int tilesFromLeft = currentTileIndex % 8; //index 59, 3 tiles
            int tileLayer = (currentTileIndex - tilesFromLeft) / 8; //eg layer 7 top
            int i = currentTileIndex;
            if (tileLayer != 0)
            {
                while (i % 8 != 0)
                {
                    if (i <= 7)
                    {
                        break;
                    }
                    i += directions[6];
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            Debug.Log(gm.tiles[i].transform);
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        //down right
        if (directions[7] != 0)
        {
            int tilesFromLeft = currentTileIndex % 8; //index 59, 3 tiles
            int tileLayer = (currentTileIndex - tilesFromLeft) / 8; //eg layer 7 top
            int i = currentTileIndex;
            if (tileLayer != 0)
            {
                while (i % 8 != 7)
                {
                    if (i <= 7)
                    {
                        break;
                    }
                    i += directions[7];
                    if (gm.tiles[i].transform.childCount == 0)
                    {
                        positions.Add(gm.tiles[i]);
                    }
                    else
                    {
                        if (gm.tiles[i].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            Debug.Log(gm.tiles[i].transform);
                            positions.Add(gm.tiles[i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}