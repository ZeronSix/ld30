using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Color StartColor;
    public Color EndColor;
    public float FadeTime;

    private GUITexture _fader;

    void Awake()
    {
        _fader = GetComponent<GUITexture>();
        _fader.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        StartCoroutine(FadeStart());
    }

    public void EndScene(string text)
    {
        TacticalSceneGUI.Get().WinText.text = text;
        TacticalSceneGUI.Get().Enabled = false;
        StartCoroutine(FadeEnd());
    }

    private IEnumerator FadeStart()
    {
        var elapsedTime = 0.0f;

        while (elapsedTime < FadeTime)
        {
            _fader.color = Color.Lerp(StartColor, EndColor, elapsedTime / FadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        TacticalSceneGUI.Get().Enabled = true;
    }

    private IEnumerator FadeEnd()
    {
        var elapsedTime = 0.0f;

        while (elapsedTime < FadeTime) {
            _fader.color = Color.Lerp(EndColor, StartColor, elapsedTime / FadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}