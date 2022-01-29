using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private float timer;
    public float timeUntilRedirect = 2f;
    public Transform playerToTarget;
    public Vector3 tempDirection;

    // Start is called before the first frame update
    void Start()
    {
        tempDirection = playerToTarget.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempDirection, 0.03f);
        if (timer <= 0)
        {
            timer = timeUntilRedirect;
            tempDirection = playerToTarget.position - transform.position;
            Debug.Log(tempDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo"))
        {
            other.GetComponent<Renderer>().material.color = Color.black;
            Destroy(gameObject);
        }
    }
}
