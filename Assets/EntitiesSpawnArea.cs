using System.Collections;
using UnityEngine;

public class EntitiesSpawnArea : RectangleSpawnArea
{
    [SerializeField] Entity toSpawn;
    [SerializeField] int spawnAmount = 3;

    [SerializeField] float spawnCooldown = 1;

    private void Start()
    {
        StartCoroutine(ThrowItems());
    }

    IEnumerator ThrowItems()
    {
        short counter = 0;
        while (counter < spawnAmount)
        {
            counter++;

            Spawn(toSpawn, transform.position, transform.localScale);

            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 0));
    }
}
