using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenuPanelController : MonoBehaviour
{
    [SerializeField] Button save;
    [SerializeField] Button load;

    private IEnumerator Start()
    {
        while (true)
        {
            load.interactable = SaveSystem.CanLoad;
            yield return null;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        StopAllCoroutines();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
