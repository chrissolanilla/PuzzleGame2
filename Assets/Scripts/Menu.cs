using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject directionsPopUp;
    public GameObject defaultTriangles;
    private GameObject[] oldTriangles;
    private bool directionsActive;

    private void Start()
    {
        if(directionsPopUp) directionsPopUp.SetActive(false);   
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
}
