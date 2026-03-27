using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGenerator : MonoBehaviour
{
    public Texture2D sourceImage;
    public int columns = 3;
    public int rows = 3;
    public GameObject piecePrefab;
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePuzzle();
        ShufflePieces();

        PuzzleManager.Instance.initializePuzzle(rows * columns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePuzzle()
    {
        float pieceWidth = sourceImage.width / columns;
        float pieceHeight = sourceImage.height / columns;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Rect rect = new Rect(
                                        x * pieceWidth,
                                        (rows - 1 - y) * pieceHeight,
                                        pieceWidth,
                                        pieceHeight
                                    );

                Sprite pieceSprite = Sprite.Create(sourceImage, 
                                                    rect, 
                                                    new Vector2(0.5f, 0.5f)
                                                  );

                GameObject piece = Instantiate(piecePrefab, parent);
                piece.GetComponent<Image>().sprite = pieceSprite;
                piece.GetComponent<PuzzlePiece>().correctIndex = y * columns + x;
                piece.name = $"Piece_{x}_{y}";
            }
        }
    }

    void ShufflePieces()
    {
        List<Transform> pieces = new List<Transform>();

        foreach (Transform child in parent)
        {
            pieces.Add(child);
        }

        //Metodo Fisher-Yates para permutar elementos de uma lista
        for(int i = pieces.Count - 1; i > 0; i--)
{
            int j = Random.Range(0, i + 1);

            var temp = pieces[i];
            pieces[i] = pieces[j];
            pieces[j] = temp;
        }

        // Certifica que nenhuma peça caiu no lugar correto da grid
        for (int i = 0; i < pieces.Count; i++)
        {
            var piece = pieces[i].GetComponent<PuzzlePiece>();

            if (piece.correctIndex == i)
            {                
                int swapIndex = (i + 1) % pieces.Count;

                var temp = pieces[i];
                pieces[i] = pieces[swapIndex];
                pieces[swapIndex] = temp;
            }
        }

        //Embaralha na GRID
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].SetSiblingIndex(i);
        }
    }
}
