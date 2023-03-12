using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int turnNum = 1;
    public bool turnEnded = false;
    public Color colored, tileColored;
    public List<GameObject> tiles;
    public List<GameObject> pieces;
    public GameObject tilePrefab;
    public GameObject gameBoard;
    public GameObject kingFab, queenFab, bishopFab, knightFab, rookFab, pawnFab;
    public GameObject markerFab, movementFab;

    public List<GameObject> movementMarkers;

    public Button rookBut, knightBut, bishopBut, queenBut;

    public GameObject selectedPiece;

    public GameObject whiteKing, blackKing;

    [SerializeField]
    //private GameObject selectedTile;
    private GameObject marker;
    private bool selectionMade;
    // Start is called before the first frame update
    void Start()
    {
        SetBoard();
        gameBoard.transform.position = new Vector3(-4.5f, -4.5f, 0);
        SetPieces();
        StartCoroutine(TurnCounter());
        rookBut.gameObject.SetActive(false);
        knightBut.gameObject.SetActive(false);
        bishopBut.gameObject.SetActive(false);
        queenBut.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnEnded == true)
        {
            selectedPiece = null;
            Destroy(marker);
            Debug.Log("ran counter");
            StartCoroutine(TurnCounter());
            CanCastle();
        }
    }

    //set board up
    void SetBoard()
    {
        GameObject tileClone;
        for(int y = 1; y <= 8; y++)
        {
            for(int x = 1; x <= 8; x++)
            {
                tileClone = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tileClone.transform.SetParent(gameBoard.transform);
                tiles.Add(tileClone);
                if ((x+y) % 2 == 0)
                {
                    tileClone.GetComponent<SpriteRenderer>().color = tileColored;
                }
            }
        }
    }

    //is a mess in there btw
    private void SetPieces()
    {
        GameObject holder;

        //place pawns
        int tileIndex = 8;
        for (int i = 0; i < 8; i++)
        {
            holder = Instantiate(pawnFab, tiles[tileIndex].transform);
            holder.GetComponent<Pieces>().isWhite = true;
            pieces.Add(holder);
            tileIndex++;
        }
        tileIndex = 48;
        for (int i = 0; i < 8; i++)
        {
            holder = Instantiate(pawnFab, tiles[tileIndex].transform);
            holder.GetComponent<Pieces>().isWhite = false;
            holder.GetComponent<SpriteRenderer>().color = colored;
            pieces.Add(holder);
            tileIndex++;
        }

        //place rooks
        holder = Instantiate(rookFab, tiles[0].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(rookFab, tiles[7].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(rookFab, tiles[56].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);
        holder = Instantiate(rookFab, tiles[63].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);

        //place knights
        holder = Instantiate(knightFab, tiles[1].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(knightFab, tiles[6].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(knightFab, tiles[57].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);
        holder = Instantiate(knightFab, tiles[62].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);

        //place bishops
        holder = Instantiate(bishopFab, tiles[2].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(bishopFab, tiles[5].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(bishopFab, tiles[58].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);
        holder = Instantiate(bishopFab, tiles[61].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);

        //place kings and queens
        holder = Instantiate(queenFab, tiles[3].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        pieces.Add(holder);
        holder = Instantiate(kingFab, tiles[4].transform);
        holder.GetComponent<Pieces>().isWhite = true;
        whiteKing = holder;
        pieces.Add(holder);

        holder = Instantiate(queenFab, tiles[59].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        pieces.Add(holder);
        holder = Instantiate(kingFab, tiles[60].transform);
        holder.GetComponent<Pieces>().isWhite = false;
        holder.GetComponent<SpriteRenderer>().color = colored;
        blackKing = holder;
        pieces.Add(holder);
    }

    //white plays on odd turn [turnNum mod 2 != 0]
    //black plays on even turn [turnNum mod 2 = 0]
    private void TurnToPlay()
    {
        if (turnNum % 2 == 0)
        {
            foreach (GameObject piece in pieces)
            {
                if (piece.GetComponent<Pieces>().isWhite == true)
                {
                    piece.GetComponent<Pieces>().canMove = false;
                }
                else
                {
                    piece.GetComponent<Pieces>().canMove = true;
                }
            }
            Debug.Log("black to play");
        }
        else
        {
            foreach (GameObject piece in pieces)
            {
                if (piece.GetComponent<Pieces>().isWhite == true)
                {
                    piece.GetComponent<Pieces>().canMove = true;
                }
                else
                {
                    piece.GetComponent<Pieces>().canMove = false;
                }
            }
            Debug.Log("white to play");
        }
    }

    //run ONCE
    //increment turn count last
    private IEnumerator TurnCounter()
    {
        turnEnded = false;
        TurnToPlay();
        Debug.Log(turnNum);
        //yield return new WaitUntil(() => turnEnded == true);
        turnNum += 1;
        Debug.Log("turn++");
        yield return new WaitUntil(() => turnEnded == true);
    }

    //call this
    public void PieceSelected(GameObject piece)
    {
        if(marker != null)
        {
            Destroy(marker);
        }
        if (piece.GetComponent<Pieces>().canMove)
        {
            selectedPiece = piece;
            marker = Instantiate(markerFab, selectedPiece.transform.position, Quaternion.identity);
        }
    }

    public void MoveLocation(List<GameObject> positionList)
    {
        if (movementMarkers != null)
        {
            foreach (GameObject marker in movementMarkers)
            {
                Destroy(marker);
            }
            movementMarkers.Clear();
        }
        GameObject tempMarker;
        foreach(GameObject position in positionList)
        {
            if(position.transform.childCount == 0)
            {
                tempMarker = Instantiate(movementFab, position.transform.position, Quaternion.identity);
                tempMarker.GetComponent<MoveTile>().linkedTile = position;
                movementMarkers.Add(tempMarker);
            }
            else if (selectedPiece == whiteKing)
            {
                if (position.transform.GetChild(0).GetComponent<Pieces>().isPiece == Pieces.PieceType.rook)
                {
                    tempMarker = Instantiate(movementFab, position.transform.position, Quaternion.identity);
                    tempMarker.GetComponent<MoveTile>().linkedTile = position;
                    tempMarker.GetComponent<SpriteRenderer>().color = Color.blue;
                    movementMarkers.Add(tempMarker);
                }
            }
            else if (selectedPiece == blackKing)
            {
                if (position.transform.GetChild(0).GetComponent<Pieces>().isPiece == Pieces.PieceType.rook)
                {
                    tempMarker = Instantiate(movementFab, position.transform.position, Quaternion.identity);
                    tempMarker.GetComponent<MoveTile>().linkedTile = position;
                    tempMarker.GetComponent<SpriteRenderer>().color = Color.blue;
                    movementMarkers.Add(tempMarker);
                }
            }
            else
            {
                tempMarker = Instantiate(movementFab, position.transform.position, Quaternion.identity);
                tempMarker.GetComponent<MoveTile>().linkedTile = position;
                tempMarker.GetComponent<SpriteRenderer>().color = Color.red;
                movementMarkers.Add(tempMarker);
            }
        }
        Debug.Log("add");
    }

    public void PieceMoved()
    {
        foreach (GameObject marker in movementMarkers)
        {
            Destroy(marker);
        }
        movementMarkers.Clear();
        Destroy(marker);
        if(CheckPawn() == true)
        {
            StartCoroutine(PromotionSelection());
        }
        else
        {
            turnEnded = true;
        }
    }

    public bool CheckPawn()
    {
        int tileIndex = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (selectedPiece.transform.IsChildOf(tiles[i].transform))
            {
                tileIndex = i;
            }
        }
        if (selectedPiece.GetComponent<Pieces>().isPiece == Pieces.PieceType.pawn)
        {
            if (selectedPiece.GetComponent<Pieces>().isWhite)
            {
                if (tileIndex >= 56)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                if (tileIndex <= 7)
                {
                    return true;
                }
                else return false;
            }
        }
        else
        {
            return false;
        }
    }

    private IEnumerator PromotionSelection()
    {
        Debug.Log("coroutine STARTED");
        rookBut.gameObject.SetActive(true);
        knightBut.gameObject.SetActive(true);
        bishopBut.gameObject.SetActive(true);
        queenBut.gameObject.SetActive(true);
        selectionMade = false;
        yield return new WaitUntil(() => selectionMade == true);
        rookBut.gameObject.SetActive(false);
        knightBut.gameObject.SetActive(false);
        bishopBut.gameObject.SetActive(false);
        queenBut.gameObject.SetActive(false);
        turnEnded = true;
        Debug.Log("coroutine ENDED");
    }

    public void PromoteToRook()
    {
        GameObject holder;
        holder = Instantiate(rookFab, selectedPiece.transform.parent.transform);
        holder.GetComponent<Pieces>().isWhite = selectedPiece.GetComponent<Pieces>().isWhite;
        holder.GetComponent<SpriteRenderer>().color = selectedPiece.GetComponent<SpriteRenderer>().color;
        pieces.Add(holder);
        pieces.Remove(selectedPiece);
        Destroy(selectedPiece);
        selectionMade = true;
    }
    public void PromoteToKnight()
    {
        GameObject holder;
        holder = Instantiate(knightFab, selectedPiece.transform.parent.transform);
        holder.GetComponent<Pieces>().isWhite = selectedPiece.GetComponent<Pieces>().isWhite;
        holder.GetComponent<SpriteRenderer>().color = selectedPiece.GetComponent<SpriteRenderer>().color;
        pieces.Add(holder);
        pieces.Remove(selectedPiece);
        Destroy(selectedPiece);
        selectionMade = true;
    }
    public void PromoteToBishop()
    {
        GameObject holder;
        holder = Instantiate(bishopFab, selectedPiece.transform.parent.transform);
        holder.GetComponent<Pieces>().isWhite = selectedPiece.GetComponent<Pieces>().isWhite;
        holder.GetComponent<SpriteRenderer>().color = selectedPiece.GetComponent<SpriteRenderer>().color;
        pieces.Add(holder);
        pieces.Remove(selectedPiece);
        Destroy(selectedPiece);
        selectionMade = true;
    }
    public void PromoteToQueen()
    {
        GameObject holder;
        holder = Instantiate(queenFab, selectedPiece.transform.parent.transform);
        holder.GetComponent<Pieces>().isWhite = selectedPiece.GetComponent<Pieces>().isWhite;
        holder.GetComponent<SpriteRenderer>().color = selectedPiece.GetComponent<SpriteRenderer>().color;
        pieces.Add(holder);
        pieces.Remove(selectedPiece);
        Destroy(selectedPiece);
        selectionMade = true;
    }

    public void CanCastle()
    {
        Debug.Log("castler");
        if (whiteKing.GetComponent<Pieces>().canMove)
        {
            Debug.Log("white side");
            for (int i = 1; i <= 3; i++)
            {
                if(tiles[i].transform.childCount != 0)
                {
                    whiteKing.GetComponent<King>().queenSide = false;
                    break;
                }
                else
                {
                    whiteKing.GetComponent<King>().queenSide = true;
                }
            }
            for (int i = 5; i <= 6; i++)
            {
                if (tiles[i].transform.childCount != 0)
                {
                    whiteKing.GetComponent<King>().kingSide = false;
                    break;
                }
                else
                {
                    whiteKing.GetComponent<King>().kingSide = true;
                }
            }
        }
        else if (blackKing.GetComponent<Pieces>().canMove)
        {
            Debug.Log("black side");
            for (int i = 57; i <= 59; i++)
            {
                if (tiles[i].transform.childCount != 0)
                {
                    blackKing.GetComponent<King>().queenSide = false;
                    break;
                }
                else
                {
                    blackKing.GetComponent<King>().queenSide = true;
                }
            }
            for (int i = 61; i <= 62; i++)
            {
                if (tiles[i].transform.childCount != 0)
                {
                    blackKing.GetComponent<King>().kingSide = false;
                    break;
                }
                else
                {
                    blackKing.GetComponent<King>().kingSide = true;
                }
            }
        }
    }
}
