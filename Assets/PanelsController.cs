using UnityEngine;

public class PanelsController : MonoBehaviour
{
    protected GameObject openPanel;

    public virtual void OpenPanel(GameObject panel)
    {
        if(openPanel != null)
            openPanel.SetActive(false);

        panel.SetActive(true);
        openPanel = panel;
    }

    public virtual void CloseOpenPanel()
    {
        openPanel?.SetActive(false);
        openPanel = null;
    }
}
