using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class SelectedPartButton : MonoBehaviour, IPointerDownHandler
{
    public BodyPartButton bodyPartButton;

    public void Start()
    {
        bodyPartButton = FindFirstObjectByType<BodyPartButton>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();

        if (textComponent != null )
        {
            BodyPartManager.instance.selectedPartText = textComponent.text;
            bodyPartButton.CreateSelectedBodyPartBubble();
        }
    }
}
