using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public float lifeTime = 100;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        lifeTime--;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo"))
        {
            other.GetComponent<Renderer>().material.color = Color.black;
        }
        Destroy(gameObject);
    }
}
