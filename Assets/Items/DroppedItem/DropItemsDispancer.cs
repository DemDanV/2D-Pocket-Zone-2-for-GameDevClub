using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsDispancer : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] float maxDropDistance = 3;
    private void Start()
    {
        StartCoroutine(ThrowItems());
    }

    IEnumerator ThrowItems()
    {
        short counter = 0;
        while (counter < 20)
        {
            counter++;

            Item toInit = items[Random.Range(0, items.Count)];

            int amount = 999;
            if (toInit.Stackable == false)
                amount = 5;

            InventoryItem toIn = new InventoryItem(toInit, Random.Range(1, amount));

            ItemDropManager.singleton.Spawn(toIn, transform.position, maxDropDistance);

            yield return new WaitForSeconds(2);
        }
    }
}
