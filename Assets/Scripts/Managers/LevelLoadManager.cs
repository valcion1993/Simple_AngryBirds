using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager instance;

    [Header("Bar settings")]
    [SerializeField] private Slider loadingBar;

    [Header("Canvas group fade")]
    [SerializeField] private float fadeSpeed;
    private CanvasGroup canvasGroup;

    //Unity
	void Awake()
	{
        canvasGroup = GetComponent<CanvasGroup>();

        if (!instance)
		{
            instance = this;

            DontDestroyOnLoad(gameObject);

            ResetLoadValues();
        }
		else
		{
            Destroy(gameObject);
		}
    }

    //Publics
	public void LoadLevel(string levelName)
    {
        StartCoroutine(FadeEffect(false));
        StartCoroutine(LoadSceneAsync(levelName));
    }

    //Privates
    void ResetLoadValues()
    {
        loadingBar.value = 0;
    }

    void DissableLoadPanel() 
    {
        StartCoroutine(FadeEffect(true));

        Invoke(nameof(ResetLoadValues), 0.5f);
    }

    //IEnumerators
    IEnumerator FadeEffect(bool negative) 
    {
        yield return new WaitForSeconds(fadeSpeed);

		if (negative)
		{
			if (canvasGroup.alpha > 0)
			{
                canvasGroup.alpha -= 0.05f;

                StartCoroutine(FadeEffect(negative));
            }
		}
		else
		{
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.05f;

                StartCoroutine(FadeEffect(negative));
            }
        }
    }
    IEnumerator LoadSceneAsync(string levelName)
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while (!op.isDone)
        {
            float progress = op.progress;

            loadingBar.value = Mathf.Round(progress * 100f);
            yield return null;
        }

        if (op.isDone)
        {
            loadingBar.value = 100;
            DissableLoadPanel();
        }
    }
}