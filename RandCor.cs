using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCor : MonoBehaviour
{
    struct Cube {
        public Vector3 position;
        public Color color;

    }

    public ComputeShader computeShader;
    public int iteractions = 50;
    public int conta = 100;
    GameObject[] gameObjects;
    Cube[] data;
    public GameObject modelpref;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (data == null)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Create"))
            {
                createCube();
            }
        }
        if (data != null)
        {
            if (GUI.Button(new Rect(110, 0, 100, 50), "Random CPU"))
            {
                for (int k = 0; k < iteractions; k++)
                {
                    for (int i = 0; i < gameObjects.Length; i++)
                    {
                        gameObjects[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
                    }

                }
            }

        }

        if (data != null)
        {
            if (GUI.Button(new Rect(220, 0, 100, 50), "Random GPU"))
            {
                int size = 4 * sizeof(float) + 3 * sizeof(float);

                ComputeBuffer computeBuffer = new ComputeBuffer(data.Length, size);
                computeBuffer.SetData(data);

                computeShader.SetBuffer(0, "cubes", computeBuffer);
                computeShader.SetInt("iteraction", iteractions);

                computeShader.Dispatch(0, data.Length / 10, 1, 1);

                computeBuffer.GetData(data);

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].GetComponent<MeshRenderer>().material.SetColor("_Color", data[i].color);
                }

                computeBuffer.Dispose();


            }
        }
    }
    private void createCube()
    {
        data = new Cube[conta * conta];
        gameObjects = new GameObject[conta * conta];

        for (int i = 0; i < conta; i++)
        {
            float offsetX = (-conta / 2 + i);
            
            for (int j = 0; j < conta; j++)
            {
                float offsetY = (-conta / 2 + j);
                Color _color = Random.ColorHSV();

                GameObject go = GameObject.Instantiate(modelpref, new Vector3(offsetX * 0.7f, 0, offsetY * 0.7f), Quaternion.identity);
                go.GetComponent<MeshRenderer>().material.SetColor("Color", _color);

                gameObjects[i * conta + j] = go;
                
                data[i * conta + j] = new Cube();
                data[i * conta + j].position = go.transform.position;
                data[i * conta + j].color = _color;

            }

        }
    }

}
