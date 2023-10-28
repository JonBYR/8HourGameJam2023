using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PassWordGenerator : MonoBehaviour
{
    private string codeWord;
    //public TMP_InputField guessInput;
    public TMP_Text guessText;
    private PlayerController cont;
    // Start is called before the first frame update
    void Awake()
    {
        List<string> potentialWords = new List<string>() { "code", "pass", "left", "yuzu", "pear", "heat", "beat", "dead", "send", "aims", "game", "tame", "seal" };
        int position = Random.Range(0, potentialWords.Count-1);
        codeWord = potentialWords[position];
        cont = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(codeWord);
    }

    // Update is called once per frame
    
    public void checkGuess(TMP_InputField guess)
    {
        
        if (guess.text == codeWord)
        {
            guessText.text = "Code Word guessed! GOD MODE UNLOCKED";
            cont.godMode = true;
        }
        else   
        {
            List<int> correctPositions = new List<int>();
            List<char> correctLetters = new List<char>();
            for (int i = 0; i < guess.text.Length; i++)
            {
                char c = guess.text[i];
                if ((char)guess.text[i] == (char)codeWord[i]) correctPositions.Add(i + 1);
                else if ((codeWord.Contains(c))) correctLetters.Add(c);
            }
            if (correctPositions.Count == 0 && correctLetters.Count == 0) 
            {
                guessText.text = "No letters guessed correctly";
            }
            else
            {
                int[] correctPos = correctPositions.ToArray();
                char[] correctLet = correctLetters.ToArray();
                guessText.text = "Correct Positions: " + string.Join(", ", correctPos) + " Correct Letters in wrong position: " + string.Join(", ", correctLet);
            }
        }
    }
}
