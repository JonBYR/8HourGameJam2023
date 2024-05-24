using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InputCodeGuesser : MonoBehaviour
{
    List<string> inputCodes = new List<string>();
    InputPasswordGenerator gen;
    private PlayerController cont;
    // Start is called before the first frame update
    void Start()
    {
        inputCodes.Clear();
        gen = GameObject.Find("PassCode").GetComponent<InputPasswordGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputCodes.Add("A");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            inputCodes.Add("B");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            inputCodes.Add("C");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputCodes.Add("D");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            inputCodes.Add("E");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            inputCodes.Add("F");
        }
        if (inputCodes.Count == 4)
        {
            if(gen.passCodeGuesser(inputCodes) == true) this.gameObject.SetActive(false);
            else inputCodes.Clear();
        }
        
            
    }
}
