using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plano : MonoBehaviour
{
    public RandCorCS randCorCS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, 1, collision.gameObject.transform.position.z);
        randCorCS.Final(collision.gameObject);
    }
}
