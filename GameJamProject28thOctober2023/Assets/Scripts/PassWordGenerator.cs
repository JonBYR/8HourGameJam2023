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
    public AudioSource cursedAudio;
    public AudioSource whimsicalAudio;
    // Start is called before the first frame update
    void Awake()
    {
        List<string> potentialWords = new List<string>() { "code", "pass", "left", "yuzu", "pear", "heat", "beat", "dead", "send", "aims", "game", "tame", "seal" }; //potential words
        int position = Random.Range(0, potentialWords.Count-1);
        codeWord = potentialWords[position];
        cont = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(codeWord);
    }

    // Update is called once per frame
    
    public void checkGuess(TMP_InputField guess)
    {
        
        if (guess.text == codeWord) //checks if user guessed the password
        {
            guessText.text = "Code Word guessed! GOD MODE UNLOCKED";
            cont.godMode = true; //activates God Mode in player
            cursedAudio.Pause();
            whimsicalAudio.Play();
        }
        else   
        {
            List<int> correctPositions = new List<int>(); //checks if any letters are in the correct position
            List<char> correctLetters = new List<char>(); //checks if any letters are correct
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
