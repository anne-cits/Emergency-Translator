using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BodyPartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private SelectedPartButton selectedPartButton;

    [Header ("Inspector Settings")]
    public string hoveredTextString = "";
    private string selectedTextString = "";
    private TMP_Text hoveredBubbleText;
    [SerializeField] private GameObject bodyPartVisual;

    [Header ("Body Part Bubble")]
    private GameObject bodyPartPrefab;
    private RectTransform bodyTransform;

    [Header("Body Part Selected Bubble")]
    private GameObject bodyPartSelectedPrefab;
    private RectTransform bodyPartSelectedTransform;

    private void Start()
    {
        //Body Prefab
        bodyTransform = GetComponent<RectTransform>();
        bodyPartPrefab = Resources.Load<GameObject>("BodyPart_Prefab");

        //Selected Body Prefab
        bodyPartSelectedTransform = GameObject.Find("SelectedBodyParts_Viewport").GetComponent<RectTransform>();
        bodyPartSelectedPrefab = Resources.Load<GameObject>("BodyPart_Selected_Prefab");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CreateHoverBodyPartBubble();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearHoverBodyPartBubble();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selectedTextString = hoveredTextString;

        BodyPartManager.instance.SelectBodyPart(bodyPartVisual);
    }

    public void CreateHoverBodyPartBubble()
    {
        Vector3 spawnPosition = bodyTransform.position + new Vector3(0f, 48f, 0f);

        GameObject bubble = Instantiate(bodyPartPrefab, spawnPosition, Quaternion.identity, bodyTransform);

        hoveredBubbleText = bubble.GetComponentInChildren<TMP_Text>();
        hoveredBubbleText.text = hoveredTextString;
    }

    public void ClearHoverBodyPartBubble()
    {
        foreach (Transform child in bodyTransform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateSelectedBodyPartBubble()
    {
        GameObject bubble = Instantiate(bodyPartSelectedPrefab, bodyPartSelectedTransform);

        TMP_Text bubbleText = bubble.GetComponentInChildren<TMP_Text>();
        bubbleText.text = selectedTextString + ": " + selectedPartButton.branchTextString;
    }

    public void ClearSelectedBodyPartBubble()
    {
        foreach (Transform child in bodyPartSelectedTransform)
        {
            Destroy(child.gameObject);
        }
    }

}
