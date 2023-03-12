using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public GameManager gm;
    public GameObject linkedTile;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //function here
    //when spawned, take the associated tile info and drop it here
    //move the tile piece, set parents, destroy un-needed objs, end the turn
    private void OnMouseDown()
    {
        Debug.Log(linkedTile);
        if(linkedTile.transform.childCount == 0)
        {
            GameObject pieceToMove = gm.selectedPiece;
            pieceToMove.transform.parent = linkedTile.transform;
            pieceToMove.transform.position = linkedTile.transform.position;
            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;
            gm.PieceMoved();
            Debug.Log("moved");
        }
        else if (linkedTile.transform.GetChild(0).GetComponent<Pieces>().isPiece == Pieces.PieceType.rook)
        {
            GameObject pieceToMove = gm.selectedPiece;
            GameObject rookToMove = linkedTile.transform.GetChild(0).gameObject;
            
            if(pieceToMove.GetComponent<Pieces>().isWhite != rookToMove.GetComponent<Pieces>().isWhite)
            {
                gm.pieces.Remove(linkedTile.transform.GetChild(0).gameObject);
                Destroy(linkedTile.transform.GetChild(0).gameObject);
                pieceToMove = gm.selectedPiece;
                pieceToMove.transform.parent = linkedTile.transform;
                pieceToMove.transform.position = linkedTile.transform.position;
                pieceToMove.GetComponent<Pieces>().takenFirstMove = true;
                gm.PieceMoved();
                Debug.Log("captured");
            }

            for(int i = 0; i <= 63; i++)
            {
                if (rookToMove.transform.IsChildOf(gm.tiles[i].transform))
                {
                    switch (i)
                    {
                        case 0:
                            pieceToMove.transform.parent = gm.tiles[i + 2].transform;
                            pieceToMove.transform.position = gm.tiles[i + 2].transform.position;
                            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;

                            rookToMove.transform.parent = gm.tiles[i + 3].transform;
                            rookToMove.transform.position = gm.tiles[i + 3].transform.position;
                            rookToMove.GetComponent<Pieces>().takenFirstMove = true;
                            break;
                        case 7:
                            pieceToMove.transform.parent = gm.tiles[i - 1].transform;
                            pieceToMove.transform.position = gm.tiles[i - 1].transform.position;
                            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;

                            rookToMove.transform.parent = gm.tiles[i - 2].transform;
                            rookToMove.transform.position = gm.tiles[i - 2].transform.position;
                            rookToMove.GetComponent<Pieces>().takenFirstMove = true;
                            break;
                        case 56:
                            pieceToMove.transform.parent = gm.tiles[i + 2].transform;
                            pieceToMove.transform.position = gm.tiles[i + 2].transform.position;
                            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;

                            rookToMove.transform.parent = gm.tiles[i + 3].transform;
                            rookToMove.transform.position = gm.tiles[i + 3].transform.position;
                            rookToMove.GetComponent<Pieces>().takenFirstMove = true;
                            break;
                        case 63:
                            pieceToMove.transform.parent = gm.tiles[i - 1].transform;
                            pieceToMove.transform.position = gm.tiles[i - 1].transform.position;
                            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;

                            rookToMove.transform.parent = gm.tiles[i - 2].transform;
                            rookToMove.transform.position = gm.tiles[i - 2].transform.position;
                            rookToMove.GetComponent<Pieces>().takenFirstMove = true;
                            break;
                    }
                }
            }
            gm.PieceMoved();
            Debug.Log("moved");
        }
        else
        {
            gm.pieces.Remove(linkedTile.transform.GetChild(0).gameObject);
            Destroy(linkedTile.transform.GetChild(0).gameObject);
            GameObject pieceToMove = gm.selectedPiece;
            pieceToMove.transform.parent = linkedTile.transform;
            pieceToMove.transform.position = linkedTile.transform.position;
            pieceToMove.GetComponent<Pieces>().takenFirstMove = true;
            gm.PieceMoved();
            Debug.Log("captured");
        }
    }
}
