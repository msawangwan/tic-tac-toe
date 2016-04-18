﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
    private GameObject boardObject;

    private Board board; // cache reference to self from parent gameObject
    private BoardManager manager;
    private BoardInitialiser boardUtility;

    public Vector2[] TileCoordinates { get; set; }
    public Dictionary<Vector2, Tile> TileTable { get; set; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Vector2 BoardCenterPoint { get; private set; }

    public bool isBoardActive = false;

    // use as constructor
    public void CreateBoard( GameObject boardObj, Board boardScriptReference, BoardManager boardManager , int boardWidth, int boardHeight ) {
        manager = boardManager;
        boardObject = boardObj;
        board = boardScriptReference;

        Width = boardWidth;
        Height = boardHeight;

        boardUtility = new BoardInitialiser ( Width, Height );

        TileCoordinates = boardUtility.GetGridCoordinates ( );
        BoardCenterPoint = boardUtility.GameboardCenterPoint;

        TileTable = boardUtility.DrawBoard ( gameObject, TileCoordinates );

        isBoardActive = boardObject.activeInHierarchy;
    }
}