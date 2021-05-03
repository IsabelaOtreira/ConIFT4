using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{
    public Button button;
    public InputField inputField;
    public static string userinput;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(InputButtonHandler);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputButtonHandler()
    {
        userinput = inputField.text;
        Debug.Log("Input " + inputField.text  +  userinput);

    }
}
