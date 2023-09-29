using UnityEngine;

public class InGamePanelsController : PanelsController
{
    [SerializeField] GameObject ControlsUI;
    private void Start()
    {
        OpenPanel(ControlsUI);
    }

    public override void OpenPanel(GameObject panel)
    {
        if (panel != ControlsUI)
            InputManager.Add(InputManager.ActionMapNames.Player);

        base.OpenPanel(panel);
    }

    public override void CloseOpenPanel()
    {
        base.CloseOpenPanel();

        InputManager.Remove(InputManager.ActionMapNames.Player);
        OpenPanel(ControlsUI);
    }
}
