using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public GameManager gm;
    public bool canMove;
    public bool takenFirstMove;
    public bool isWhite;

    public enum PieceType
    {
        pawn,
        rook,
        knight,
        bishop,
        queen,
        king
    }
    [SerializeField]
    public PieceType isPiece;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!canMove)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}