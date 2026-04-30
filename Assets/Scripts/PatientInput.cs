using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientInput : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField patientInputField;

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

        string translatedText = TranslateInput(inputText);

        patientOutputs.Add(translatedText);

        CreatePatientBubble(translatedText);

        patientInputField.text = "";
    }

    string TranslateInput(string input)
    {
        return input; // replace later with real translation
    }

    void CreatePatientBubble(string text)
    {
        GameObject bubble = Instantiate(patientOutputPrefab, patientOutputContainer);

        TMP_Text bubbleText = bubble.GetComponentInChildren<TMP_Text>();
        bubbleText.text = text;

        // Force layout update (helps prevent weird delays)
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