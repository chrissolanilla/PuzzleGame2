using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ReportSender : MonoBehaviour
{
    public void SendReport(string email)
    {
        StartCoroutine(SendReportEmail(email));
    }

    IEnumerator SendReportEmail(string playerEmail)
    {
        string report = ReportFormatter.GetFormattedReport();

        WWWForm form = new WWWForm();
        form.AddField("email", playerEmail);
        form.AddField("report", report);

        UnityWebRequest www = UnityWebRequest.Post("localhost:3000", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to send email: " + www.error);
        }
        else
        {
            Debug.Log("Email sent successfully!");
        }
    }
}

