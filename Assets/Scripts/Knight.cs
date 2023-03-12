using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private int[] directions = { 6, 15, 17, 10, -6, -15, -17, -10 };

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
        for(int i = 0; i <= directions.Length - 1; i++)
        {
            int newIndex = currentTileIndex + directions[i];
            if (i <= 3)
            {
                if (newIndex <= 63)
                {
                    if (gm.tiles[newIndex].transform.childCount == 0)
                    {
                        Debug.Log(gm.tiles[newIndex].transform.childCount);
                        positions.Add(gm.tiles[newIndex]);
                    }
                    else
                    {
                        if (gm.tiles[newIndex].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            positions.Add(gm.tiles[newIndex]);
                        }
                    }
                }
            }
            else if (i > 3)
            {
                if (newIndex >= 0)
                {
                    if (gm.tiles[newIndex].transform.childCount == 0)
                    {
                        Debug.Log(gm.tiles[newIndex].transform.childCount);
                        positions.Add(gm.tiles[newIndex]);
                    }
                    else
                    {
                        if (gm.tiles[newIndex].transform.GetChild(0).GetComponent<Pieces>().isWhite != pieceScript.isWhite)
                        {
                            Debug.Log("this is a kill tile");
                            positions.Add(gm.tiles[newIndex]);
                        }
                    }
                }
            }
        }
    }
}