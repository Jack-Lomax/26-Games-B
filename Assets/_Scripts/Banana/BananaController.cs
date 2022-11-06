using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class BananaController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float recoilAmount;
    [SerializeField] private float torqueAmount;
    [SerializeField] private float minShotInterval;
    [SerializeField] private Transform shotDirection;

    [SerializeField] private float explosionForce;

    int directionSign = 1;
    float timeOfLastShot;

    [Header("Graphics")]
    [SerializeField] private ShakePreset shotShake;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shotParticles;

    [SerializeField] private ShakePreset collisionShake;
    [SerializeField] private GameObject hitParticles;

    [SerializeField] private AudioSource coinSFX;
    [SerializeField] private AudioSource explosionSFX;
    [SerializeField] private AudioSource shotSFX;
    [SerializeField] private AudioSource hitSFX;


    float timeStarted;

    private float score;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;


    void Start()
    {
        timeStarted = Time.time;

        rb = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(0,0,Random.Range(210, 270));

        timeOfLastShot = Time.time;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time - timeOfLastShot > minShotInterval)
        {
            rb.rotation = Quaternion.Euler(0,0, transform.eulerAngles.z);    
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 dir = new Vector3(shotDirection.up.x, shotDirection.up.y, 0).normalized;

            dir -= Vector3.up * .3f;
                

            rb.AddForce(-dir * recoilAmount, ForceMode.VelocityChange);

            //float dot = Vector3.Dot(shotDirection.up, Vector3.right);
            Vector3 direction = Vector3.forward * torqueAmount * directionSign;
            directionSign *= -1;

            rb.AddTorque(direction, ForceMode.VelocityChange);

            Instantiate(bullet, shotDirection.position, Quaternion.FromToRotation(bullet.transform.forward, shotDirection.up));
            Instantiate(shotParticles, shotDirection.position, Random.rotation);

            shotSFX.Play();

            timeOfLastShot = Time.time;

            Shaker.ShakeAll(shotShake);
        }

        if(transform.position.y < -5)
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("WALL"))
        {
            Instantiate(hitParticles, transform.position, Random.rotation);
            Shaker.ShakeAll(collisionShake);
            hitSFX.Play();
        }
        else if(col.gameObject.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.Detonate();
            Vector3 dir = (transform.position - bomb.transform.position).normalized;
            rb.AddForce(dir * explosionForce, ForceMode.VelocityChange);
            explosionSFX.Play();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            coin.Remove();
            score += 10;
            coinSFX.Play();
        }
    }

    public float GetScore()
    {
        return (Time.time - timeStarted) + score;
    }
    
    public void UpdateScore(float additive) => score += additive;



}
