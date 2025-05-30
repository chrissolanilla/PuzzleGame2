using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleStarManager : MonoBehaviour
{
    public GameObject starPrefab; // Assign in Inspector
    public Sprite filledStar;
    public Sprite outlinedStar;

    private const int MAX_STARS = 3;

	void OnEnable()
	{
		RefreshStars();
	}

	public void RefreshStars()
	{
		foreach (Transform puzzleSlot in transform)
		{
			//remove any existing StarRow under each puzzle
			Transform oldRow = puzzleSlot.Find("StarRow");
			if (oldRow != null)
			{
				Destroy(oldRow.gameObject);
				Debug.Log($"[PuzzleStarManager] Removed old StarRow from {puzzleSlot.name}");
			}
		}

		foreach (Transform puzzleSlot in transform)
		{
			// string levelName = puzzleSlot.name.Replace("PuzzleSlot", "Level ").Replace("-", "-");
			string levelName = puzzleSlot.name.Replace("PuzzlesSlot", "Level ");



			int starRating = 0;
			if (PlayerStatistics.levels.ContainsKey(levelName))
			{
				starRating = PlayerStatistics.levels[levelName].starRating;
				Debug.Log($"[PuzzleStarManager] Found rating for {levelName}: {starRating} stars");
			}
			else
			{
				Debug.LogWarning($"[PuzzleStarManager] No level data found for {levelName}");
			}

			GameObject starRow = new GameObject("StarRow", typeof(RectTransform));
			starRow.transform.SetParent(puzzleSlot, false);

			//position stars 50 units *above* center
			RectTransform rect = starRow.GetComponent<RectTransform>();
			rect.anchorMin = new Vector2(0.5f, 0.5f);
			rect.anchorMax = new Vector2(0.5f, 0.5f);
			rect.pivot = new Vector2(0.5f, 0.5f);
			rect.anchoredPosition = new Vector2(0f, 71f); // move stars up

			HorizontalLayoutGroup layout = starRow.AddComponent<HorizontalLayoutGroup>();
			layout.childAlignment = TextAnchor.MiddleCenter;
			layout.spacing = 5;
			layout.childControlHeight = false;
			layout.childControlWidth = false;

			for (int i = 0; i < MAX_STARS; i++)
			{
				GameObject star = Instantiate(starPrefab, starRow.transform);
				Image starImg = star.GetComponent<Image>();
				starImg.sprite = i < starRating ? filledStar : outlinedStar;
				starImg.rectTransform.sizeDelta = new Vector2(32, 32);
			}
		}
	}


}

