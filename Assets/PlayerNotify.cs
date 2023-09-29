using TMPro;
using UnityEngine;

public class PlayerNotify : MonoBehaviour
{
    [SerializeField] TMP_Text t;
    public void SetText(string message, float deleteAfter)
    {
        t.text = message;
        Destroy(gameObject, deleteAfter);
    }
}
