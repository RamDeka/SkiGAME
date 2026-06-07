using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private int nextLevel;
   
    void Start()
    {
        gameOver.SetActive(false);
        overlay.gameObject.SetActive(true);
        StartCoroutine("FadeoutOverlay");
    }

    private IEnumerator FadeinOverlay()
    {
        while (overlay.alpha < 1.0f)
        {
            overlay.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRaceUI;
    }
    private void OnDisable()
    {
        FinishGate.FinishRace -= FinishRaceUI;
    }
    private void FinishRaceUI()
    {
        gameOver.SetActive(true);
    }

    public void Retry()
    {
        StartCoroutine("RetryCoroutine");
    }
    
    private IEnumerator RetryCoroutine()
    {
        yield return StartCoroutine("FadeinOverlay");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Quit()
    {
        StartCoroutine("QuitCoroutine");
    }
    private IEnumerator QuitCoroutine()
    {
        yield return StartCoroutine("FadeinOverlay");
        Application.Quit();
    }

    public void NextLevel()
    {
        StartCoroutine("NextCoroutine");
    }
    private IEnumerator NextCoroutine()
    {
        yield return StartCoroutine("FadeinOverlay");
        SceneManager.LoadScene(nextLevel);
    }
    
    private IEnumerator FadeoutOverlay()
    {
        while (overlay.alpha > 0.0f)
        {
            overlay.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    void Update()
    {
        
    }
}