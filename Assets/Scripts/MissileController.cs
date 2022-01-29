using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Rigidbody body;
    private float timer;
    public float timeUntilRedirect = 2f;
    public Transform playerToTarget;
    public Vector3 tempTarget;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        tempTarget = playerToTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        body.velocity = Vector3.MoveTowards(transform.position, tempTarget, 1f);
        if (timer <= 0)
        {
            timer = timeUntilRedirect;
            tempTarget = playerToTarget.position;
            Debug.Log(tempTarget);
        }
    }
}
