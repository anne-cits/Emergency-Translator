using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PatientInput : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField patientInputField;

    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown languageDropdown;

    [Header("Buttons")]
    [SerializeField] private Button translateButton;
    [SerializeField] private Button summaryButton;
    [SerializeField] private Button clearButton;

    [Header("Output")]
    [SerializeField] private Transform patientOutputContainer;
    [SerializeField] private GameObject patientOutputPrefab;

    private List<string> patientOutputs = new List<string>();

    private void Start()
    {
        translateButton.onClick.AddListener(ProcessPatientInput);
        summaryButton.onClick.AddListener(GetPatientSummary);
        clearButton.onClick.AddListener(ClearPatientInput);
    }

    void ProcessPatientInput()
    {
        string inputText = patientInputField.text;

        if (string.IsNullOrWhiteSpace(inputText))
            return;

        // Force language selection
        if (languageDropdown.value == 0)
        {
            Debug.LogWarning("Please select a language first.");
            return;
        }

        StartCoroutine(TranslateAndDisplay(inputText));

        patientInputField.text = "";
    }

    IEnumerator TranslateAndDisplay(string input)
    {
        string sourceLang = GetLanguageCode(languageDropdown.value);
        string targetLang = "en";

        string url = $"https://api.mymemory.translated.net/get?q={UnityWebRequest.EscapeURL(input)}&langpair={sourceLang}|{targetLang}";

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Translation failed: " + request.error);

            // fallback: show original input if API fails
            CreatePatientBubble(input);
            yield break;
        }

        string json = request.downloadHandler.text;

        TranslationResponseMyMemory response = JsonUtility.FromJson<TranslationResponseMyMemory>(json);

        string translatedText = response.responseData.translatedText;

        patientOutputs.Add(translatedText);

        // ONLY show translated English
        CreatePatientBubble(translatedText);
    }

    string GetLanguageCode(int index)
    {
        switch (index)
        {
            case 1: return "es";     // Spanish
            case 2: return "zh-CN";  // Mandarin (Simplified Chinese)
            case 3: return "zh-TW";  // Cantonese (Traditional Chinese)
            case 4: return "tl";     // Tagalog
            case 5: return "vi";     // Vietnamese
            case 6: return "ar";     // Arabic
            case 7: return "fr";     // French
            case 8: return "ko";     // Korean
            case 9: return "pt";     // Portuguese
            default: return "en";
        }
    }

    string GetLanguageLabel(int index)
    {
        switch (index)
        {
            case 1: return "Spanish";
            case 2: return "Mandarin";
            case 3: return "Cantonese";
            case 4: return "Tagalog";
            case 5: return "Vietnamese";
            case 6: return "Arabic";
            case 7: return "French";
            case 8: return "Korean";
            case 9: return "Portuguese";
            default: return "Unknown";
        }
    }

    public void CreatePatientBubble(string text)
    {
        GameObject bubble = Instantiate(patientOutputPrefab, patientOutputContainer);

        TMP_Text bubbleText = bubble.GetComponentInChildren<TMP_Text>();
        bubbleText.text = text;

        Canvas.ForceUpdateCanvases();
    }

    void ClearPatientInput()
    {
        patientOutputs.Clear();

        foreach (Transform child in patientOutputContainer)
        {
            Destroy(child.gameObject);
        }

        patientInputField.text = "";
    }

    void GetPatientSummary()
    {
        string summary = string.Join("\n", patientOutputs);
        Debug.Log(summary);

        ClearPatientInput();
    }
}

[System.Serializable]
public class TranslationResponseMyMemory
{
    public ResponseData responseData;
}

[System.Serializable]
public class ResponseData
{
    public string translatedText;
}