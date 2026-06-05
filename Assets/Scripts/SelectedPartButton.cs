using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using JetBrains.Annotations;

public class SelectedPartButton : MonoBehaviour, IPointerDownHandler
{
    private BodyPartButton bodyPartButton;

    private Image branchObject;
    public TMP_Text branchBubbleText;
    public string branchTextString = "";

    public void Start()
    {
        branchObject = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TMP_Text branchBubbleText = branchObject.GetComponentInChildren<TMP_Text>();
        branchBubbleText.text = branchTextString;

        bodyPartButton.CreateSelectedBodyPartBubble();
    }
}
