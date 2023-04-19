using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPiece : MonoBehaviour
{
    [SerializeField]
    private string triangleType = "";
    private SpriteRenderer renderer;
    private bool currentlyOverlapped = false;
    GameObject overlappingObject = null;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        if (renderer) renderer.enabled = false;
    }

    public void Detatch()
    {
        gameObject.transform.parent = null;
    }

    public void MakeHintVisible(float visibilityTime)
    {
        if (renderer && renderer.enabled) return;

        if (currentlyOverlapped)
        {
            HintPiece synonymousHint = FindSynonymousHint();
            if (synonymousHint) synonymousHint.MakeHintVisible(visibilityTime);
            else StartCoroutine(MakeHintInvisible(visibilityTime));
        }
        else StartCoroutine(MakeHintInvisible(visibilityTime));
    }

    private IEnumerator MakeHintInvisible(float visibilityTime)
    {
        if (renderer) renderer.enabled = true;
        yield return new WaitForSeconds(visibilityTime);
        if (renderer) renderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PuzzleCheck puzzleComp = collision.gameObject.GetComponent<PuzzleCheck>();
        if (puzzleComp && puzzleComp.TriangleType() == triangleType)
        {
            currentlyOverlapped = true;
            overlappingObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(overlappingObject == collision.gameObject)
        {
            currentlyOverlapped = false;
            overlappingObject = null;
        }
    }

    public bool IsCurrentlyOverlapped()
    {
        return currentlyOverlapped;
    }

    public string TriangleType()
    {
        return triangleType;
    }

    private HintPiece FindSynonymousHint()
    {
        GameObject[] triangles = GameObject.FindGameObjectsWithTag("hint");
        foreach (GameObject triangle in triangles)
        {
            HintPiece hintPiece = triangle.GetComponentInChildren<HintPiece>();
            if (hintPiece && !hintPiece.IsCurrentlyOverlapped() && triangleType == hintPiece.TriangleType()) return hintPiece;
        }
        return null;
    }
}
