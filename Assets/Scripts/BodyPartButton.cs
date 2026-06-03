using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BodyPartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header ("Inspector Settings")]
    [SerializeField] private string bodyUnselectedTextString = "";
    private string bodySelectedTextString = "";
    private TMP_Text unselectedBubbleText;
    private TMP_Text selectedBubbleText;
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
        CreateBodyPartBubble();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearBodyPartBubble();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (unselectedBubbleText != null)
        {
            unselectedBubbleText.text = selectedBubbleText.text;
            selectedBubbleText.text = bodySelectedTextString;
            BodyPartManager.instance.SelectBodyPart(bodyPartVisual);
        }
        
    }

    public void CreateBodyPartBubble()
    {
        Vector3 spawnPosition = bodyTransform.position + new Vector3(0f, 48f, 0f);

        GameObject bubble = Instantiate(bodyPartPrefab, spawnPosition, Quaternion.identity, bodyTransform);

        unselectedBubbleText = bubble.GetComponentInChildren<TMP_Text>();
        unselectedBubbleText.text = bodyUnselectedTextString;
    }

    public void ClearBodyPartBubble()
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
        bubbleText.text = bodySelectedTextString + ": " + BodyPartManager.instance.selectedPartText;
    }

    public void ClearSelectedBodyPartBubble()
    {
        foreach (Transform child in bodyPartSelectedTransform)
        {
            Destroy(child.gameObject);
        }
    }

}
