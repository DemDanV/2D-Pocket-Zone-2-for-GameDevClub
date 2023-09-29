using System.Collections;
using UnityEngine;

public class SwayingBushes : MonoBehaviour
{
    [SerializeField] float maxRotationAngle = 20;
    [SerializeField] float swayDuration = 2;
    [SerializeField] int swayCycles = 2; // Количество циклов покачивания
    [SerializeField] AudioClip swaySound;

    Coroutine swaying;
    AudioSource audioSource;
    Quaternion initialRotation;
    int currentSwayCycle = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialRotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (swaying == null && currentSwayCycle < swayCycles)
        {
            swaying = StartCoroutine(Animate());
        }
    }

    IEnumerator Animate()
    {
        float estimatedTime = Time.time + swayDuration * swayCycles;
        float elapsedTime = 0;
        float mult = 1 / (swayDuration * swayCycles);

        if (audioSource != null && swaySound != null)
        {
            audioSource.PlayOneShot(swaySound);
        }

        while (currentSwayCycle < swayCycles)
        {
            float t = elapsedTime / swayDuration;
            float rotationAngle = Mathf.Sin(t * Mathf.PI * 2) * maxRotationAngle * (mult * (estimatedTime - Time.time));

            // Вращаем куст.
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, rotationAngle);

            elapsedTime += Time.deltaTime;

            // Если анимация завершилась, увеличиваем счетчик циклов.
            if (t >= 1f)
            {
                currentSwayCycle++;
                elapsedTime = 0;
            }

            yield return null;
        }

        // Сбрасываем в начальное положение и обнуляем счетчик циклов.
        transform.rotation = initialRotation;
        currentSwayCycle = 0;
        swaying = null;
    }
}
