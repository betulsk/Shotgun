using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunS : MonoBehaviour
{
    public Transform spawnPoint;
    public float spreadAngle;
    [SerializeField] private int pelletCount;
    public float range = 100f;
    public float pelletFireVel = 100f;
    public int totalBullet=0;


    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject pellet;
    List<Quaternion> pellets;

    public Camera fpsCam;
    UIManager instance;

    #region SINGLETON PATTERN
    public static GunS _instance;
    public static GunS Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GunS>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("");
                    _instance = container.AddComponent<GunS>();
                }
            }

            return _instance;
        }
    }
    #endregion
    private void Awake()
    {

        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }
    private void Start()
    {
        instance = UIManager.Instance; 
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            totalBullet += pellets.Count;
            Debug.Log(totalBullet);
            instance.UpdateBullet(totalBullet);
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        for (int i = 0; i < pelletCount; i++)
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                pellets[i] = Random.rotation;

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                
                GameObject pelletHandler = Instantiate(pellet, spawnPoint.transform.position, Quaternion.LookRotation(hit.normal));
                pelletHandler.transform.Rotate(Vector3.left * 90);
                Rigidbody tempRig;
                tempRig = pelletHandler.GetComponent<Rigidbody>();

                tempRig.AddForce(transform.forward * pelletFireVel);
                
                Destroy(pelletHandler, 3.0f);
                Destroy(impactGO, 2f);
            }

        }
        
    }
}
