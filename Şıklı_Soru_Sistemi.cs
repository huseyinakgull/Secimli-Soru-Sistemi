using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Şıklı_Soru_Sistemi : MonoBehaviour
{
    public TMP_Text soruMetni;
    public Button İlerleme_Butonu;
    public TMP_Text sonucMetni;
    public Button[] secimButonlari;
    private int puan;

    [Tooltip("Soruları ve cevapları burada ayarlayın.")]
    public Soru[] sorular;

    private int currentQuestionIndex = 0;
    private bool[] soruSonuclari;

    private void Start()
    {
        soruSonuclari = new bool[sorular.Length];
        sonucMetni.gameObject.SetActive(false);
        DisplayQuestion(currentQuestionIndex);
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        bool dogruMu = choiceIndex == sorular[currentQuestionIndex].dogruSecenekIndex;
        soruSonuclari[currentQuestionIndex] = dogruMu;
        currentQuestionIndex++;
        if (currentQuestionIndex < sorular.Length)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResults();
        }
    }

    private void DisplayQuestion(int questionIndex)
    {
        soruMetni.text = sorular[questionIndex].soruMetni;

        for (int i = 0; i < secimButonlari.Length; i++)
        {
            secimButonlari[i].GetComponentInChildren<TMP_Text>().text = sorular[questionIndex].secenekler[i];
        }

        sonucMetni.text = "";
    }

    private void ShowResults()
    {
        soruMetni.gameObject.SetActive(false);
        foreach (Button button in secimButonlari)
        {
            button.gameObject.SetActive(false);
        }
        sonucMetni.gameObject.SetActive(true);
        sonucMetni.text = "Sonuçlar:\n";
        for (int i = 0; i < sorular.Length; i++)
        {
            İlerleme_Butonu.gameObject.SetActive(true);
            if (soruSonuclari[i])
            {
                sonucMetni.text += (i + 1) + ". Soru: " + "Doğru\n";
            }
            else
            {
                sonucMetni.text += (i + 1) + ". Soru: " + "Yanlış. \n [" + sorular[i].secenekler[sorular[i].dogruSecenekIndex] + "]\n";
            }
        }
    }
}

[System.Serializable]
public class Soru
{
    [TextArea(3, 5)]
    public string soruMetni;
    public string[] secenekler;
    [Tooltip("Doğru seçeneğin indeksi (0'dan başlayarak)")]
    public int dogruSecenekIndex;
}
