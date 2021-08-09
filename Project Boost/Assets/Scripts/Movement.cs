using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    AudioSource audioSource;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    void StartLeftRotation()
    {
        ApplyRotation(rotationThrust);
        leftThrusterParticles.Stop();
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
        rightThrusterParticles.Stop();
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over.
    }
}
 