using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject directionsPopUp;
    public GameObject winnerPopUp;
    public GameObject UIButtons;
    public GameObject defaultTriangles;
    private GameObject[] oldTriangles;
    private bool directionsActive = false;
    private bool solutionView = false;
	public GameObject textPrefab;
	public GameObject sliderPrefab;

	// public GameObject imagePrefab;
	[DllImport("__Internal")]
	private static extern void ImageUploaderCaptureClick();

	IEnumerator LoadTexture (string url)
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
		{
			yield return uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Failed to load image: " + uwr.error);
				yield break;
			}

			Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
			Debug.Log("Loaded image size: " + texture.width + "x" + texture.height);

			// TODO: Instantiate image prefab, assign texture to it here
			GameObject prefab = Resources.Load<GameObject>("imagePrefab");
			if (prefab == null) {
				Debug.LogError("imagePrefab not found in Resources folder!");
				yield break;
			}

			GameObject imageObj = Instantiate(prefab, GameObject.Find("Canvas").transform);
			imageObj.tag = "whiteboard";
			imageObj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
			imageObj.transform.SetAsFirstSibling();
			UnityEngine.UI.Image img = imageObj.GetComponent<UnityEngine.UI.Image>();
			img.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			// Instantiate the opacity slider as a child
			if (sliderPrefab != null)
			{
				GameObject sliderObj = Instantiate(sliderPrefab, imageObj.transform);
				sliderObj.transform.localPosition = new Vector3(0, -60, 0); // Adjust position relative to the image
				RectTransform sliderRT = sliderObj.GetComponent<RectTransform>();
				sliderRT.anchorMin = new Vector2(0.5f, 0);  // bottom center
				sliderRT.anchorMax = new Vector2(0.5f, 0);
				sliderRT.pivot = new Vector2(0.5f, 1);
				sliderRT.anchoredPosition = new Vector2(0, -2); // push it just under the image
				sliderRT.sizeDelta = new Vector2(100, 20);

				// Hook the slider to control the image's opacity
				OpacitySlider opacitySlider = sliderObj.GetComponentInChildren<OpacitySlider>();
				if (opacitySlider != null)
				{
					opacitySlider.targetImage = img;
				}
				else
				{
					Debug.LogWarning("OpacitySlider script not found on slider prefab.");
				}
			}
			else
			{
				Debug.LogWarning("sliderPrefab not assigned in Menu.cs");
			}

			//set rect transform to img size
			RectTransform rt = imageObj.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(texture.width, texture.height);

			GameObject resizeHandlePrefab = Resources.Load<GameObject>("resizeHandle");
			if(resizeHandlePrefab == null)
			{
				Debug.LogError("resizeHandle not found in Resources folder!");
				yield break;
			}
			GameObject handleObj = Instantiate(resizeHandlePrefab, imageObj.transform);
			//pos it bottom right
			RectTransform handleRT = handleObj.GetComponent<RectTransform>();
			handleRT.anchorMin = new Vector2(1, 0); // Bottom-right
			handleRT.anchorMax = new Vector2(1, 0);
			handleRT.pivot = new Vector2(1, 0);
			handleRT.anchoredPosition = new Vector2(15,-15); // Sticks to the corner OUTSIDE
			handleRT.sizeDelta = new Vector2(30, 30); // Optional: size of resize handle
			//set the target for resizing
			ResizeHandle rh = handleObj.GetComponent<ResizeHandle>();
			if(rh != null)
			{
				rh.target = imageObj.GetComponent<RectTransform>();
			}
		}
	}

	void FileSelected( string url )
	{
		StartCoroutine(LoadTexture(url));
	}


    private void Start()
    {

        if(directionsPopUp)
		{
			TMPro.TMP_Text directionsText = directionsPopUp.GetComponentInChildren<TMPro.TMP_Text>();
			directionsText.text = "Use ALL the triangles in the level to fill the red shape. When you correctly solve the puzzle, the shape will turn blue!\nTo move a triangle, click and drag it. To rotate or flip a triangle, use the buttons that appear when you click on it.\n\nIf you get stuck, use the hint button to get a hint, clicking the hint button twice each time will show you where a piece should be.";
			directionsText.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 13);
			directionsPopUp.SetActive(false);
		}
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

	public void addText(){
		GameObject newTextBox = Instantiate(textPrefab, GameObject.Find("Canvas").transform);
		newTextBox.tag = "whiteboard";
		//center
		newTextBox.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		//wrap text
		var inputField = newTextBox.GetComponentInChildren<TMPro.TMP_InputField>();
		var textComponent = inputField.textComponent;
		if(textComponent != null) {
			textComponent.enableWordWrapping = true;
			textComponent.overflowMode = TMPro.TextOverflowModes.Overflow;
			textComponent.rectTransform.sizeDelta = new Vector2(300f, 100f);
		}
		else{
			print("its null");
		}
	}

	public void addImage()
	{
        //somehow create an image that they can move around?
		#if UNITY_WEBGL && !UNITY_EDITOR
			ImageUploaderCaptureClick();
		#else
			Debug.Log("Image Uploader not supported on this platform");
		#endif
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
            UIButtons.SetActive(false);
            directionsActive = true;
        }
        else if (directionsActive == true)
        {
            directionsPopUp.SetActive(false);
            UIButtons.SetActive(true);
            directionsActive = false;
        }
    }

	public void DirectionsPopup2(){
        if (!directionsPopUp) return;

		TMPro.TMP_Text directionsText = directionsPopUp.GetComponentInChildren<TMPro.TMP_Text>();
		directionsText.text = "Have fun here!\n\nYou can get more triangles by dragging one out from the cluster of triangles in the bottom right and drag to move a triangle. Use buttons that appear to flip or rotate it.\n\nAdd text or images to the whiteboard using the buttons.\nYou can resize the images from the bottom right corner and adjust the opacity to see triangles underneath.";
		directionsText.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 13);
		directionsPopUp.SetActive(false);

        if (directionsActive == false)
        {
            directionsPopUp.SetActive(true);
            UIButtons.SetActive(false);
            directionsActive = true;
        }
        else if (directionsActive == true)
        {
            directionsPopUp.SetActive(false);
            UIButtons.SetActive(true);
            directionsActive = false;
        }
	}

    public void WinGameUI()
    {
        if (winnerPopUp && winnerPopUp.activeSelf || solutionView) return;
        if (directionsPopUp)
        {
            directionsPopUp.SetActive(false);
            directionsActive = false;
        }
        if (UIButtons) UIButtons.SetActive(false);
        if (winnerPopUp) winnerPopUp.SetActive(true);
    }

    public void ViewSolution()
    {
        solutionView = true;
        if (winnerPopUp && winnerPopUp.activeSelf) winnerPopUp.SetActive(false);
        if (UIButtons)
        {
            UIButtons.SetActive(true);
            UnityEngine.UI.Button hintButton = GameObject.Find("Hint Button").GetComponent<UnityEngine.UI.Button>();
            if (hintButton) hintButton.interactable = false;
            UnityEngine.UI.Button directionsButton = GameObject.Find("DirectionsButton").GetComponent<UnityEngine.UI.Button>();
            if (directionsButton) directionsButton.interactable = false;
        }

        DragDrop[] dragDrops = Resources.FindObjectsOfTypeAll<DragDrop>();
        foreach(DragDrop dragDrop in dragDrops)
        {
            dragDrop.SetToViewOnly();
        }
        Selectable[] selectables = Resources.FindObjectsOfTypeAll<Selectable>();
        foreach (Selectable selectable in selectables)
        {
            selectable.SetToViewOnly();
        }
    }
}
