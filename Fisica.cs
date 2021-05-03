using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisica : MonoBehaviour
{
    struct Cube
    {
        public Vector3 posicao;
        public Color cor;

    };

    public GameObject modelPref;
    public ComputeShader computeShader;
    GameObject[] gameObjects;
    public int conta = 50;
    Cube[] data;


    
    
    public float fallSpeed = 8.0f;
    

    void Update()
    {

        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }

    void createCubes()
    {
        data = new Cube[conta * conta];
        gameObjects = new GameObject[conta * conta];

        for (int i = 0; i < conta; i++)
        {
            float offsetX = (-conta / 2 + i);

            for (int k = 0; k < conta; k++)
            {
                float offsetY = (-conta / 2 + k);

                Color _colorInic = Random.ColorHSV();

                GameObject go = GameObject.Instantiate(modelPref, new Vector3(offsetX * 0.6f, 0, offsetY * 0.6f), Quaternion.identity);
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", _colorInic);
                gameObjects[i * conta + k] = go;

                data[i * conta + k] = new Cube();
                data[i * conta + k].posicao = go.transform.position;
                data[i * conta + k].cor = _colorInic;
            }
        }
    }
}