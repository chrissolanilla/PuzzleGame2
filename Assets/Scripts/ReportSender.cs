using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ReportSender : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI emailInput;
	[SerializeField] TextMeshProUGUI feedbackText;
	[SerializeField] Button sendButton;
	// public TMP_InputField emailInput;

	public void SendReportFromInput()
	{
		string email = emailInput.text.Trim().Replace("\u200B", "");
		SendReport(email);
	}

    public void SendReport(string email)
    {
        StartCoroutine(SendReportEmail(email));
    }

	void ShowFeedback(string message, Color color) {
		feedbackText.text = message;
        feedbackText.color = color;
		feedbackText.gameObject.SetActive(true);
	}

    IEnumerator SendReportEmail(string playerEmail)
    {
        string report = ReportFormatter.GetFormattedReport();

		//disable button
		sendButton.interactable = false;
		//send request
        WWWForm form = new WWWForm();
        form.AddField("email", playerEmail);
        form.AddField("report", report);

        UnityWebRequest www = UnityWebRequest.Post("http://139.177.202.193:3000", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
			ShowFeedback("Error, failed to send email: " + www.error, Color.red);
            Debug.LogError("Failed to send email: " + www.error);
        }
        else
        {
			ShowFeedback("Thanks! Check your email for a sender of chrissolanilla@gmail.com. Try checking spam if you do not see it. ", Color.black);
            Debug.Log("Email sent successfully!");
        }
    }
}

