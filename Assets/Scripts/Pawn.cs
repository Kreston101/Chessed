using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private int[] directions = { 8, -8, 0, 0, 7, 9, -9, -7 };

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
        int i = currentTileIndex;

        if (pieceScript.isWhite)
        {
            Debug.Log("white");
            if (pieceScript.takenFirstMove)
            {
                if (gm.tiles[currentTileIndex + directions[0]].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[currentTileIndex + directions[0]]);
                }
                if (gm.tiles[currentTileIndex + directions[4]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[4]].transform.GetChild(0).GetComponent<Pieces>().isWhite == false)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[4]]);
                    }
                }
                if (gm.tiles[currentTileIndex + directions[5]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[5]].transform.GetChild(0).GetComponent<Pieces>().isWhite == false)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[5]]);
                    }
                }
            }
            else
            {
                int firstTile = currentTileIndex + directions[0];
                int secondTile = firstTile + directions[0];
                if (gm.tiles[firstTile].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[firstTile]);
                }
                if (gm.tiles[secondTile].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[secondTile]);
                }
                if (gm.tiles[currentTileIndex + directions[4]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[4]].transform.GetChild(0).GetComponent<Pieces>().isWhite == true)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[4]]);
                    }
                }
                if (gm.tiles[currentTileIndex + directions[5]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[5]].transform.GetChild(0).GetComponent<Pieces>().isWhite == true)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[5]]);
                    }
                }
            }
        }
        else
        {
            Debug.Log("not white");
            if (pieceScript.takenFirstMove)
            {
                if (gm.tiles[currentTileIndex + directions[1]].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[currentTileIndex + directions[1]]);
                }
                if (gm.tiles[currentTileIndex + directions[6]].transform.childCount > 0)
                {
                    if(gm.tiles[currentTileIndex + directions[6]].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[6]]);
                    }
                }
                if (gm.tiles[currentTileIndex + directions[7]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[7]].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[7]]);
                    }
                }
            }
            else
            {
                int firstTile = currentTileIndex + directions[1];
                int secondTile = firstTile + directions[1];
                if (gm.tiles[firstTile].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[firstTile]);
                }
                if (gm.tiles[secondTile].transform.childCount == 0)
                {
                    positions.Add(gm.tiles[secondTile]);
                }
                if (gm.tiles[currentTileIndex + directions[6]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[6]].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[6]]);
                    }
                }
                if (gm.tiles[currentTileIndex + directions[7]].transform.childCount > 0)
                {
                    if (gm.tiles[currentTileIndex + directions[7]].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                    {
                        positions.Add(gm.tiles[currentTileIndex + directions[7]]);
                    }
                }
            }
        }
    }
}
