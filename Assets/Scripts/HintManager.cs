using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField]
    private bool giveVagueHintFirst = true;
    [SerializeField]
    private GameObject vagueHintUI;
    [SerializeField]
    private GameObject explicitHintUI;
    [SerializeField]
    private float visibilityTime = 8.0f;
    private Dictionary<GameObject, HintPiece> triangleHintPairs = new Dictionary<GameObject, HintPiece>();
    private PuzzleScript puzzleScript;
    private bool hintActive = false;

    void Start()
    {
        if (vagueHintUI) vagueHintUI.SetActive(false);
        if (explicitHintUI) explicitHintUI.SetActive(false);

        GameObject puzzleObject = GameObject.FindWithTag("puzzle");
        if (puzzleObject) puzzleScript = puzzleObject.GetComponent<PuzzleScript>();

        GameObject[] triangles = GameObject.FindGameObjectsWithTag("triangle");
        foreach (GameObject triangle in triangles)
        {
            HintPiece hintPiece = triangle.GetComponentInChildren<HintPiece>();
            triangleHintPairs.Add(triangle, hintPiece);
            hintPiece.Detatch();
        }
    }

    public void RequestHint()
    {
        if (hintActive) return;
        PlayerStatistics.HintUsed();
        if (giveVagueHintFirst) GiveVagueHint();
        else
        {
            GameObject unsolvedPiece = puzzleScript.GetUnsolvedPiece();
            if (unsolvedPiece)
            {
                HintPiece unsolvedHintPiece = null;
                triangleHintPairs.TryGetValue(unsolvedPiece, out unsolvedHintPiece);
                if (unsolvedHintPiece)
                {
                    unsolvedHintPiece.MakeHintVisible(visibilityTime);
                    StartCoroutine(ToggleExplicitHintUI());
                }
            }
        }
    }

    private void GiveVagueHint()
    {
        giveVagueHintFirst = false;
        StartCoroutine(ToggleVagueHintUI());
    }

    private IEnumerator ToggleVagueHintUI()
    {
        hintActive = true;
        if (vagueHintUI) vagueHintUI.SetActive(true);
        yield return new WaitForSeconds(visibilityTime);
        if (vagueHintUI) vagueHintUI.SetActive(false);
        hintActive = false;
    }

    private IEnumerator ToggleExplicitHintUI()
    {
        hintActive = true;
        if (explicitHintUI) explicitHintUI.SetActive(true);
        yield return new WaitForSeconds(visibilityTime);
        if (explicitHintUI) explicitHintUI.SetActive(false);
        hintActive = false;
    }
}
