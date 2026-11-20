
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SkipOpeningButton : MonoBehaviour
{
    public Button skipButton;
    public string targetScene;
    public float delayBeforeShow = 10f;

    private void Start()
    {
        skipButton.gameObject.SetActive(false);

        skipButton.onClick.AddListener(SkipOpening);

        StartCoroutine(ShowSkipButton());
    }

    private IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        skipButton.gameObject.SetActive(true);
    }

    public void SkipOpening()
    {
        SceneManager.LoadScene(targetScene);
    }
}
