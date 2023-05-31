using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level2"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_VICTORY);
            }
            //Hien Ui chien thang
            if (UIManager.HasInstance)
            {
                Time.timeScale = 0f;
                UIManager.Instance.ActiveVictoryPanel(true);
            }

        }
        else
        {
            if (AudioManager.HasInstance && UIManager.HasInstance)
            {
                StartCoroutine(LoadScene());
                AudioManager.Instance.PlaySE(AUDIO.SE_FINISH);
                AudioManager.Instance.PlayBGM(AUDIO.BGM_BGM_04);
                UIManager.Instance.ActiveFadePanel(true);
                UIManager.Instance.FadePanel.Fade(1f, () =>
                {
                    UIManager.Instance.ActiveFadePanel(false);
                    UIManager.Instance.GamePanel.SetTimeRemain(240);
                });        
            }
            
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level2");
    }
}
