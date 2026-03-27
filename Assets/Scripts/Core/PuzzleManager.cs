using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    private PuzzlePiece selectedPiece;
    private Vector3 offset;
    private int correctPieces = 0;

    public static PuzzleManager Instance;
    public Transform dragLayer;
    public GridLayoutGroup grid;
    public int totalPieces;

    private void Awake()
    {       
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initializePuzzle(int total)
    {
        totalPieces = total;
        correctPieces = 0;
    }

    public void SelectPiece(PuzzlePiece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.SetHighlight(false);
        }

        selectedPiece = piece;
        selectedPiece.SetHighlight(true);        
    }

    public void ReleasePiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.SetHighlight(false);
            selectedPiece = null;
        }
    }

    public void RegisterCorrectPiece()
    {
        correctPieces++;
        Debug.Log("peças corretas: " + correctPieces);

        if (correctPieces >= totalPieces)
        {
            Victory();
        }
    }
    void Victory()
    {
        Debug.Log("Puzzle completo!");
    }
}
