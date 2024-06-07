using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 30f;

    [SerializeField] AudioSource boostAudio;
    [SerializeField] AudioSource bumpAudio;
    [SerializeField] AudioSource backgroundAudio;

    void Start()
    {
        backgroundAudio.Play();
    }

    private void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bump" && other.gameObject.tag != "Package")
        {
            // play bump audio
            bumpAudio.Play();

            // set movespeed to slowspeed
            moveSpeed = slowSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            // play boost audio
            boostAudio.Play();

            Destroy(other.gameObject);

            // set movespeed to boost
            moveSpeed = boostSpeed;
        }
    }
}
