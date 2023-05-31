using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI loadingText;
    [SerializeField]
    private Slider loadingSlider;

    private void OnEnable()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level1");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingText.SetText($"LOADING SCENES: {asyncOperation.progress * 100}%");
            if(asyncOperation.progress >= 0.9f)
            {
                loadingText.SetText("Press the space bar to continue");
                loadingSlider.value = 1f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if(UIManager.HasInstance && GameManager.HasInstance)
                    {
                        UIManager.Instance.ActiveFadePanel(true);
                        UIManager.Instance.ActiveLoadingPanel(false);
                        UIManager.Instance.FadePanel.Fade(1,() =>
                        {
                            asyncOperation.allowSceneActivation = true;
                            UIManager.Instance.ActiveGamePanel(true);
                            GameManager.Instance.StartGame();
                            UIManager.Instance.ActiveFadePanel(false);
                        });
                    }
                }
            }
            yield return null;
        }
    }
}
