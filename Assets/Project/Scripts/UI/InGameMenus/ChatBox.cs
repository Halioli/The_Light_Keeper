using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChatBox : MonoBehaviour
{
    private static float FADE_IN_TIME = 0.3f;
    private static float FADE_OUT_TIME = 1.0f;
    private static float WAIT_TIME = 2.0f;
    private static float LETTER_DELAY = 0.05f;

    private CanvasGroup chatCanvasGroup;
    private bool chatOpen;
    private bool allTextShown;
    private string fullMssgText;
    private string currentMssgText = "";
    private string[] chatMessages = {
        "Ores can be mined",
        "These creatures look hostile",
        "If we bring it something it might activate"
    };

    public TextMeshProUGUI mssgText;
    public GameObject duckFace;

    void Start()
    {
        chatCanvasGroup = GetComponent<CanvasGroup>();
        chatOpen = false;
        allTextShown = false;
    }

    private void OnEnable()
    {
        TutorialMessages.OnNewMessage += ShowChatBox;
    }

    private void OnDisable()
    {
        TutorialMessages.OnNewMessage -= ShowChatBox;
    }

    private void ShowChatBox(int mssgID)
    {
        if (!chatOpen)
        {
            // Set canvas group to 1
            StartCoroutine("CanvasFadeIn", chatCanvasGroup);
        }

        // Display text
        fullMssgText = chatMessages[mssgID];
        StartCoroutine("ShowText");

        // Set canvas group to 0
        StartCoroutine("CanvasFadeOut", chatCanvasGroup);
    }

    private void ResetValues()
    {
        chatOpen = false;
        allTextShown = false;
    }

    IEnumerator CanvasFadeIn(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(0f, 0f);
        Vector2 endVector = new Vector2(1f, 1f);

        for (float t = 0f; t < FADE_IN_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_IN_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        chatOpen = true;
    }

    IEnumerator CanvasFadeOut(CanvasGroup canvasGroup)
    {
        Vector2 startVector = new Vector2(1f, 1f);
        Vector2 endVector = new Vector2(0f, 0f);

        while (!allTextShown)
        {
            yield return null;
        }

        yield return new WaitForSeconds(WAIT_TIME);

        for (float t = 0f; t < FADE_OUT_TIME; t += Time.deltaTime)
        {
            float normalizedTime = t / FADE_OUT_TIME;

            canvasGroup.alpha = Vector2.Lerp(startVector, endVector, normalizedTime).x;
            yield return null;
        }
        canvasGroup.alpha = endVector.x;

        ResetValues();
    }

    IEnumerator ShowText()
    {
        Vector3 punchVec = new Vector3(1.0f, 1.0f, 1.0f);

        for (int i = 0; i < fullMssgText.Length + 1; i++)
        {
            currentMssgText = fullMssgText.Substring(0, i);
            mssgText.text = currentMssgText;

            duckFace.transform.DOShakeScale(LETTER_DELAY, 0.2f, 10, 50);
            
            yield return new WaitForSeconds(LETTER_DELAY);
        }
        allTextShown = true;
    }
}