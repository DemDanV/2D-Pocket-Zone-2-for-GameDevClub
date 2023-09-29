using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DroppedItemAnimator : MonoBehaviour
{
    [SerializeField] float animationSpeed = 100.0f; // Скорость анимации
    [SerializeField] float maxYOffset = 0.2f;     // Максимальное смещение по Y
    [SerializeField] float smoothTime = 0.2f;     // Время плавной остановки

    [SerializeField] Transform shadow;
    [SerializeField] Transform icon;


    private Vector3 initialPosition;
    private Vector3 initialScale;


    private void Start()
    {
        initialPosition = icon.transform.position;
        initialScale = shadow.localScale;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (true)
        {
            float yPos = Mathf.Sin(Time.time * Mathf.Deg2Rad * animationSpeed);

            icon.position = initialPosition + Vector3.up * yPos * maxYOffset;
            shadow.localScale = initialScale - Vector3.one * yPos * maxYOffset;

            yield return null;
        }
    }
}