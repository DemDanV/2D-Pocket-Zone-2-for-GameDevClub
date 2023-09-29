using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 1);
    }
}
