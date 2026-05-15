using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BodyPartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private string bodyString = "";

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
        Debug.Log("ENTER");
        CreateBodyPartBubble();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT");
        ClearBodyPartBubble();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("BUTTON HAS BEEN PRESSED");
        CreateSelectedBodyPartBubble();
    }

    public void CreateBodyPartBubble()
    {
        Vector3 spawnPosition = bodyTransform.position + new Vector3(0f, 48f, 0f);

        GameObject bubble = Instantiate(bodyPartPrefab, spawnPosition, Quaternion.identity, bodyTransform);

        TMP_Text bubbleText = bubble.GetComponentInChildren<TMP_Text>();
        bubbleText.text = bodyString;
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
        bubbleText.text = bodyString;
    }

    public void ClearSelectedBodyPartBubble()
    {
        foreach (Transform child in bodyPartSelectedTransform)
        {
            Destroy(child.gameObject);
        }
    }

}
