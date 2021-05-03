using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCorCS : MonoBehaviour
{

    struct Cube
    {
        public Vector3 posicao;
        public Color cor;
    }

    public ComputeShader computeShader;
    public GameObject modelPref;
    public int numboxes;
    public int interacaos = 100;
    Cube[] data;
    GameObject[] gameObjects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (data == null)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Create"))
            {
                int.TryParse (InputButton.userinput, out numboxes);
                Debug.Log("numbox " + numboxes);
                createCubes();
            }
        }

        if (data != null)
        {
            if (GUI.Button(new Rect(110, 0, 100, 50), "Random CPU"))
            {
                for (int k = 0; k < interacaos; k++)
                {
                    for (int i = 0; i < numboxes * numboxes; i++)
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
                int totalSize = sizeof(float) * 3 + sizeof(float) * 4;

                ComputeBuffer computeBuffer = new ComputeBuffer(data.Length, totalSize);
                computeBuffer.SetData(data);

                computeShader.SetBuffer(0, "cubes", computeBuffer);
                computeShader.SetInt("interactions", interacaos);
                computeShader.Dispatch(0, data.Length / 10, 1, 1);

                computeBuffer.GetData(data);

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].GetComponent<MeshRenderer>().material.SetColor("_Color", data[i].cor);
                }

                computeBuffer.Dispose();
            }
        }
    }

    void createCubes()
    {
        data = new Cube[numboxes];
        gameObjects = new GameObject[numboxes];

        for (int i = 0; i < numboxes; i++)
        {
            float offsetX = (-numboxes / 2 + i);

            for (int k = 0; k < numboxes; k++)
            {
                float offsetY = (-numboxes / 2 + k);

                Color _colorInic = Random.ColorHSV();

                GameObject go = GameObject.Instantiate(modelPref, new Vector3(offsetX * 0.6f, 0, offsetY * 0.6f), Quaternion.identity);
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", _colorInic);
                gameObjects[i * numboxes + k] = go;

                data[i * numboxes + k] = new Cube();
                data[i * numboxes + k].posicao = go.transform.position;
                data[i * numboxes + k].cor = _colorInic;
            }
        }
    }

}