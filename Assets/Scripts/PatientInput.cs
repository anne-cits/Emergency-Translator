using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientInput : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField patientInputField; //left side input field

    [Header("Buttons")]
    [SerializeField] private Button translateButton; //translate patent's input
    [SerializeField] private Button summaryButton; //summary button for patient
    [SerializeField] private Button clearButton; //clear list & all player's input

    [Header("Output")]
    [SerializeField] private Transform patientOutputContainer;
    [SerializeField] private GameObject patientOutputPrefab;

    [Header("Patient Output Storage")]
    public List<string> patientOutputs = new List<string>();

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

        string translatedText = TranslateInput(inputText); //Placeholder translator logic

        patientOutputs.Add(translatedText); //Store for summary

        CreatePatientBubble(translatedText); //Create UI bubble

        patientInputField.text = ""; //Clear input
    }

    string TranslateInput(string input) //Actually Translates the Words
    {
        return "Patient says: " + input; // Placeholder translator logic
    }

    void CreatePatientBubble(string text)
    {
        GameObject bubble = Instantiate(patientOutputPrefab, patientOutputContainer);

        TMP_Text bubbleText = bubble.GetComponentInChildren<TMP_Text>();
        bubbleText.text = text;
    }

    void ClearPatientInput() //clears all input when the clear button is pressed
    {
        patientOutputs.Clear(); //clear the list


        foreach (Transform child in patientOutputContainer) // Destroy all bubble UI children
        {
            Destroy(child.gameObject);
        }

        patientInputField.text = ""; //clear text input
    }

    public void GetPatientSummary()
    {
        string.Join("\n", patientOutputs);
        ClearPatientInput();
    }

}
