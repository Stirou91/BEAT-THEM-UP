using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{


    [SerializeField] GameObject loadingscreen;
    [SerializeField] GameObject spaceText;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI progressText;




    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;

        loadingscreen.SetActive(true);

        float progress = 0;
        while (operation.progress<.9f)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress/2f;
            progressText.text = progress * 100f/2f + "%";

            yield return null;
        }

        float t = 0;
        float duration = 2f;

        while(t<duration)
        {
            t += Time.deltaTime;
            float progress2 = Mathf.Lerp(0, 50, t / duration);
            slider.value = .5f + progress2 /100f ;
            progressText.text = (50f + progress2).ToString("0.0")+ "%";

            yield return null;
        }

        spaceText.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        operation.allowSceneActivation = true;

    }

    //public void ChargerLeJeu()
    //{

    //    SceneManager.LoadScene(1);


    //}
    //public void ChargerLeMenu()
    //{

    //    SceneManager.LoadScene(0);


    //}
}
