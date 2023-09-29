using UnityEngine;

public class EntityRelationshipManager : MonoBehaviour
{
    [SerializeField] private EntityRelationships entityRelationshipsData;
    public EntityRelationships CurrentGlobalRelationships => entityRelationshipsData;

    public static EntityRelationshipManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            Debug.LogWarning("There's more than one EntityRelationshipManager!");
            return;
        }
        singleton = this;
    }
}
