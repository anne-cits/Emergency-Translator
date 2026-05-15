using UnityEngine;

public class UpdateCanvas : MonoBehaviour
{
    [ContextMenu("Execute Action")]
    void MyAction()
    {
        Debug.Log("Executed from context menu");
        Canvas.ForceUpdateCanvases();
    }
}
