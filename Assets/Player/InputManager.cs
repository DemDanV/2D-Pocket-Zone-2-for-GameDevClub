using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static readonly IDictionary<ActionMapNames, int> mapStates = new Dictionary<ActionMapNames, int>();

    private static PlayerInput controls;
    public static PlayerInput Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new PlayerInput();
        }
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();
    private void OnDestroy() => controls = null;

    public static void Add(ActionMapNames mapName)
    {
        mapStates.TryGetValue(mapName, out int value);
        mapStates[mapName] = value + 1;

        UpdateMapState(mapName);
    }

    public static void Remove(ActionMapNames mapName)
    {
        mapStates.TryGetValue(mapName, out int value);
        mapStates[mapName] = Mathf.Max(value - 1, 0);

        UpdateMapState(mapName);
    }

    private static void UpdateMapState(ActionMapNames mapName)
    {
        int value = mapStates[mapName];

        if (value > 0)
        {
            Controls.asset.FindActionMap(mapName.ToString()).Disable();

            return;
        }

        Controls.asset.FindActionMap(mapName.ToString()).Enable();
    }

    public enum ActionMapNames
    {
        Player,
        UI
    }
}
