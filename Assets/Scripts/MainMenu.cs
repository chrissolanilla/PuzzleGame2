using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private GameObject PuzzleSelectUI;
    [SerializeField] private GameObject PrivacyPolicyUI;
	public GameObject starPopup;
	public static bool OpenPuzzleSelectOnStart = false;

	public void starClick()
	{
		if(starPopup) starPopup.SetActive(true);
	}

	public void starDismiss()
	{
		if(starPopup) starPopup.SetActive(false);
	}


    private void Start()
    {
				if(starPopup){
								starPopup.SetActive(false);
										}

        if(PuzzleSelectUI) PuzzleSelectUI.SetActive(false);

        if(PrivacyPolicyUI) PrivacyPolicyUI.SetActive(false);

		if( OpenPuzzleSelectOnStart )
		{
			OpenPuzzleSelectOnStart = false;
			PuzzleSelectMenu();
		}
    }

    public void PuzzleSelectMenu()
    {
        if (PuzzleSelectUI.activeSelf) PuzzleSelectUI.SetActive(false);
        else PuzzleSelectUI.SetActive(true);
    }

    public void PrivacyPolicy()
    {
        if(PrivacyPolicyUI.activeSelf) PrivacyPolicyUI.SetActive(true);
        else PrivacyPolicyUI.SetActive(true);
    }

    public void ClosePrivacyPolicy()
    {
        if(PrivacyPolicyUI)
            PrivacyPolicyUI.SetActive(false);
    }
}
