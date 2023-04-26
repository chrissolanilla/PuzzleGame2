using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject directionsPopUp;
    public GameObject winnerPopUp;
    public GameObject UIButtons;
    public GameObject defaultTriangles;
    private GameObject[] oldTriangles;
    private bool directionsActive;

    private void Start()
    {
        if(directionsPopUp) directionsPopUp.SetActive(false);   
        if(winnerPopUp) winnerPopUp.SetActive(false);
        if(UIButtons) UIButtons.SetActive(true);
    }
    public void ClearBoard()
    {
         oldTriangles = GameObject.FindGameObjectsWithTag("whiteboard");

        for (var i = 0; i < oldTriangles.Length; i++)
        {
            Destroy(oldTriangles[i]);
        }
    }

    public void PopulateBoard()
    {
        ClearBoard();
        Instantiate(defaultTriangles, defaultTriangles.transform.position, defaultTriangles.transform.rotation);
    }

    public void DirectionsPopUp()
    {
        if (!directionsPopUp) return;
        if (directionsActive == false)
        {
            directionsPopUp.SetActive(true);
            directionsActive = true;
        }
        else if (directionsActive == true)
        {
            directionsPopUp.SetActive(false);
            directionsActive = false;
        }
    }

    public void WinGameUI()
    {
        if (winnerPopUp && winnerPopUp.activeSelf) return;
        if (directionsPopUp)
        {
            directionsPopUp.SetActive(false);
            directionsActive = false;
        }
        if (UIButtons) UIButtons.SetActive(false);
        if (winnerPopUp) winnerPopUp.SetActive(true);
    }
}
