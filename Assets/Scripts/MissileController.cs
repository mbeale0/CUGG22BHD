using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private float timer;
    public float timeUntilRedirect = 2f;
    public Transform playerToTarget;
    public Vector3 tempDirection;

    [SerializeField] private AudioClip boomSFX;

    // Start is called before the first frame update
    void Start()
    {
        tempDirection = playerToTarget.position - transform.position;
        GetComponent<AudioSource>().Play();
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

            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOne") || other.CompareTag("PlayerTwo"))
        {
            other.GetComponent<AudioSource>().PlayOneShot(boomSFX);
            other.GetComponent<Controls>().OnHit(6.0f);
            Destroy(gameObject);
        }
    }
}
