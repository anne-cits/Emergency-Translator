using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    public static BodyPartManager instance;

    public GameObject defaultBodyVisual;
    public GameObject[] allBodyVisuals;

    private void Awake()
    {
        instance = this;
    }

    public void SelectBodyPart(GameObject bodyPartVisual)
    {
        defaultBodyVisual.SetActive(false);

        foreach (GameObject visual in allBodyVisuals)
        {
            visual.SetActive(false);
        }

        bodyPartVisual.SetActive(true);
    }
}
