using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{
    public Button button;
    public Button button2;
    public InputField inputField;
    public InputField inputField2;
    public InputField inputField3;
    public Text Tempo;
    public float tempcome;
    public float tempnovo;
    public float tempant;
    public bool tempc = false;
    public static string inputquant;
    public static string inputmassamin;
    public static string inputmassamax;
    public RandCorCS randCorCS;
    
    // Start is called before the first frame update
    void Start()
    {
        //button.onClick.AddListener(InputButtonHandler);
       // button2.onClick.AddListener(InputButtonHandler2);
       // RandCorCS randCorCS = gameObject.AddComponent<RandCorCS>() as RandCorCS;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempc == true)
        {
            tempnovo = Time.realtimeSinceStartup - tempcome;
            string segundos = (tempnovo % 60).ToString("f1");
            Tempo.text = segundos + "  segundos";
        }
       

    }
    public void InputButtonHandler()
    {
        inputquant = inputField.text;
        inputmassamin = inputField2.text;
        inputmassamax = inputField3.text;
        tempc = true;
        Timergo();
        //RandCorCS randCorCS = gameObject.AddComponent<RandCorCS>() as RandCorCS;
        randCorCS.SimCPU();
        //randCorCS.btncpupressed = true;
       

    }
    public void InputButtonHandler2()
    {
        inputquant = inputField.text;
        inputmassamin = inputField2.text;
        inputmassamax = inputField3.text;
        tempc = true;
        Timergo();
        //RandCorCS randCorCS = gameObject.AddComponent<RandCorCS>() as RandCorCS;
        randCorCS.SimGPU();
        //randCorCS.btngpupressed = true;
    

    }
    public void Timergo()
    {
        tempcome = Time.realtimeSinceStartup;
    }

    public void Timerpara()
    {
        tempc = false;
    }
}
