using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextura : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;


    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 32);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetFloat("resulution", renderTexture.width);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.width / 8, 1);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
