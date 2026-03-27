using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    public int correctIndex;
    public int startIndex;
    public bool isCorrect = false;
    public bool isSelected = false;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
        PuzzleManager.Instance.SelectPiece(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PuzzleManager.Instance.ReleasePiece();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        PuzzleManager.Instance.SelectPiece(this);

        originalParent = transform.parent;
        startIndex = transform.GetSiblingIndex();

        PuzzleManager.Instance.grid.enabled = false;

        transform.SetParent(PuzzleManager.Instance.dragLayer);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        PuzzleManager.Instance.ReleasePiece();       

        PuzzlePiece targetPiece = null;
        foreach (GameObject result in eventData.hovered)
        {
            PuzzlePiece piece = result.gameObject.GetComponentInParent<PuzzlePiece>();

            if (piece != null && piece != this)
            {
                targetPiece = piece;
                break;
            }
        }

        transform.SetParent(originalParent);

        if (targetPiece != null && targetPiece != this)
        {
            int targetIndex = targetPiece.transform.GetSiblingIndex();

            transform.SetSiblingIndex(targetIndex);
            targetPiece.transform.SetSiblingIndex(startIndex);
            CheckIfCorrect();
            targetPiece.CheckIfCorrect();
        }
        else
        {
            transform.SetSiblingIndex(startIndex);
        }

        PuzzleManager.Instance.grid.enabled = true;

        LayoutRebuilder.ForceRebuildLayoutImmediate(PuzzleManager.Instance.grid.GetComponent<RectTransform>());
    }

    public void SetHighlight(bool active)
    {
        image.color = active ? Color.gray : Color.white;
    }

    void CheckIfCorrect()
    {
        if (!isCorrect && transform.GetSiblingIndex() == correctIndex)
        {
            isCorrect = true;
            PuzzleManager.Instance.RegisterCorrectPiece();
        }
    }

}
