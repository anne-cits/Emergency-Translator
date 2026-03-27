using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanguageDropdown : MonoBehaviour
{
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var dropdownField = uiDocument.rootVisualElement.Q<DropdownField>("MyGameDropdown");

        // Create a new list of options
        List<string> newOptions = new List<string> { "Option A", "Option B", "Option C", "Option D" };

        // Assign the new options to the dropdown
        dropdownField.choices = newOptions;

        // Set a default value (e.g., "Option B" which is at index 1)
        dropdownField.index = 1;
    }
}
