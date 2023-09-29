using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfoBarDisplayer : MonoBehaviour
{
    [SerializeField] Entity toDisplay;
    [SerializeField] TMP_Text Name;
    [SerializeField] Slider healthBar;
    [SerializeField] Gradient healthGradient;
    [SerializeField] Image fillImage;

    private void OnEnable()
    {
        Name.text = toDisplay.Name;
        toDisplay.Health.onClampChanged.AddListener(RefreshClamp);
        toDisplay.Health.onValueChanged.AddListener(RefreshValue);

        RefreshClamp(toDisplay.Health.Clamp);
        RefreshValue(toDisplay.Health.Value);
    }

    private void OnDisable()
    {
        toDisplay.Health.onClampChanged.RemoveListener(RefreshClamp);
        toDisplay.Health.onValueChanged.RemoveListener(RefreshValue);
    }

    void RefreshValue(float value)
    {
        healthBar.value = value;
        fillImage.color = healthGradient.Evaluate(value/ healthBar.maxValue);
    }

    void RefreshClamp(Vector2 clamp)
    {
        float maxValue = clamp.y;
        healthBar.maxValue = maxValue;
    }
}
