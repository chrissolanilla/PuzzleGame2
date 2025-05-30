using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private GameObject PuzzleSelectUI;

	public static bool OpenPuzzleSelectOnStart = false;

    private void Start()
    {
        if(PuzzleSelectUI) PuzzleSelectUI.SetActive(false);

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
}
