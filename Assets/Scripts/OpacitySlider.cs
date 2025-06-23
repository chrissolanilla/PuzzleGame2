using UnityEngine;
using UnityEngine.UI;

public class OpacitySlider : MonoBehaviour
{
	public Image targetImage;

	void Start()
	{
		GetComponent<Slider>().onValueChanged.AddListener(SetOpacity);
	}

	void SetOpacity(float value)
	{
		if (targetImage != null)
		{
			Color c = targetImage.color;
			c.a = value/100f;
			targetImage.color = c;
		}
	}
}

