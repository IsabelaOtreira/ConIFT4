using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCorCS : MonoBehaviour
{
    struct Cube
    {
        public Vector3 position;
        public Color color;
        public float velo1;
        public float velo2;        
        public float massa;
        public float deltem;
        public Vector3 posicaonovo;
    }

    public int quant;
    public int sizetotal;
    public float Massamin;
    public float Massamax;
    public float tempant;
    public float tempnovo;
    public bool comecoCPU = false;
    public bool comecoGPU = false;
    Vector3 Posicaoinic;
    Cube[] data; 
    Color[] cubeColors; 
    public Material material;
    public GameObject cubeObj;
    public GameObject[] cubeTodos;
    public ComputeShader computeShader;
    public ComputeShader computeShader2;
    ComputeBuffer computeBuffer;
    ComputeBuffer computeBuffer2;
   

    public InputButton inputButton;
    // Start is called before the first frame update
    void Start()
    {
        Posicaoinic = Vector3.up * 100;
    }
    public void SimCPU()
    {
        if (comecoCPU == false)
        {
            quant = int.Parse(InputButton.inputquant);
            Massamax = float.Parse(InputButton.inputmassamax);
            Massamin = float.Parse(InputButton.inputmassamin);
            cubeTodos = new GameObject[quant];
            data = new Cube[quant];

            Color startColor;
            cubeColors = new Color[quant];

            for (int i = 0; i < quant; i++)
            {
                startColor = Random.ColorHSV();
                float offsetX = -quant / 2 + i;
                cubeTodos[i] = Instantiate(cubeObj, new Vector3(offsetX * 1.5f, Posicaoinic.y, Posicaoinic.z - 5), Quaternion.identity);
                cubeTodos[i].GetComponent<MeshRenderer>().material = new Material(material);
                cubeTodos[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                cubeColors[i] = cubeTodos[i].GetComponent<MeshRenderer>().material.GetColor("_Color");
            }

            comecoCPU = true;
        }
    }
 
    public void SimGPU()
    {

            if (comecoGPU == false)
            {
                quant = int.Parse(InputButton.inputquant);
                Massamax = float.Parse(InputButton.inputmassamax);
                Massamin = float.Parse(InputButton.inputmassamin);
                cubeTodos = new GameObject[quant];
                data = new Cube[quant];
                cubeColors = new Color[quant];

            for (int i = 0; i < quant; i++)
            {
                float offsetX = -quant / 2 + i;
                cubeTodos[i] = Instantiate(cubeObj, new Vector3(offsetX * 1.5f, Posicaoinic.y, Posicaoinic.z), Quaternion.identity);
                cubeTodos[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
                cubeColors[i] = cubeTodos[i].GetComponent<MeshRenderer>().material.GetColor("_Color");
                data[i].position = cubeTodos[i].transform.position;
                data[i].color = cubeTodos[i].GetComponent<MeshRenderer>().material.color;
                data[i].velo1 = 0;
                data[i].velo2 = 0;
                data[i].massa = Random.Range(Massamin, Massamax);
                data[i].deltem = 0;
                data[i].posicaonovo = cubeTodos[i].transform.position;

            }
                int vector3Size = sizeof(float) * 6;
                int variables = sizeof(float) * 4; 
                int colorSize = sizeof(float) * 4;
                sizetotal = colorSize + vector3Size + variables;

                comecoGPU = true;
            }
        
        }
    private void FixedUpdate()
    {
        if (comecoCPU == true)
        {
            tempnovo += 1 * Time.fixedDeltaTime;
          
            float tempvar = tempnovo - tempant;
            tempant = tempnovo;


            for (int i = 0; i < cubeTodos.Length; i++)
            {

                if (cubeTodos[i].transform.position.y > -10)
                {

                    data[i].velo2 += 9.8f * tempvar;

                    cubeTodos[i].transform.position -= new Vector3(0, (data[i].velo1 + data[i].velo2) * tempvar / 2, 0);
                }
               
            }
           
        }

        if (comecoGPU == true)
        {
            tempnovo += 1 * Time.fixedDeltaTime;
            for (int i = 0; i < cubeTodos.Length; i++)
            {
                data[i].deltem = tempnovo - tempant;
            }
            computeBuffer = new ComputeBuffer(data.Length, sizetotal);
            computeBuffer.SetData(data);
            computeShader2.SetBuffer(0, "cubes", computeBuffer);
            computeShader2.Dispatch(0, data.Length / 20, 1, 1);
            computeBuffer.GetData(data);
            computeBuffer.Dispose();

            for (int i = 0; i < cubeTodos.Length; i++)
            {
                if (cubeTodos[i].transform.position.y > -10)
                {
                    cubeTodos[i].transform.position = new Vector3(data[i].posicaonovo.x, data[i].posicaonovo.y, data[i].posicaonovo.z);
                }

            }
            tempant = tempnovo;
        }
    }

    public void MudacorCPU()
    {     
        comecoCPU = false;
        for (int i = 0; i < cubeTodos.Length; i++) {

            for (int k = 0; k < cubeTodos.Length; k++)
            {
                cubeTodos[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
            }
        }
        inputButton.Timerpara();
    }
    public void MudacorGPU()
    {
        comecoGPU = false;
        computeBuffer2 = new ComputeBuffer(data.Length, sizetotal);
        computeBuffer2.SetData(data);
        computeShader.SetBuffer(0, "cubes", computeBuffer2);
        computeShader.Dispatch(0, data.Length / 32, 1, 1);
        computeBuffer2.GetData(data);

        for (int i = 0; i < cubeTodos.Length; i++)
        {
            cubeTodos[i].GetComponent<MeshRenderer>().material.SetColor("_Color", data[i].color);
        }
        computeBuffer2.Dispose();
        inputButton.Timerpara();
    }

    public void Final(GameObject game)
    {
        if (comecoCPU == true)
        {
            MudacorCPU();
            
        }
        if (comecoGPU == true)
        {
            MudacorGPU();
            
        }
    }
}