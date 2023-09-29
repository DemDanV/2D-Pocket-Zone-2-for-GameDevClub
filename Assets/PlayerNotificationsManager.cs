using UnityEngine;

public class PlayerNotificationsManager : MonoBehaviour
{
    public static PlayerNotificationsManager singleton;
    [SerializeField] PlayerNotify playerNotifyPrefab;

    [SerializeField] Transform notificationsContentRef;


    private void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("There is more than one PlayerNotificationsManager");
        }
        singleton = this;
    }

    public void Notify(string message)
    {
        PlayerNotify pn = Instantiate(playerNotifyPrefab, notificationsContentRef);
        pn.SetText(message, 6);
    }

    public void SetState(string message, float deleteAfter)
    {
        PlayerNotify pn = Instantiate(playerNotifyPrefab, notificationsContentRef);
        pn.SetText(message, deleteAfter);
    }
}
