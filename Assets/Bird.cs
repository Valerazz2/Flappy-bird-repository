using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    private int SpikeCont = 4;
    public float jumpStrength = 1f, birdSpeed = 1f;
    private bool directionRight = true;
    private bool GameGoing;
    Vector3 DirectionV = Vector3.right;
    public GameObject Kluv1, Kluv2;
    public GameObject Spike;
    private GameObject[] SpikeBuffer;
    public ParticleSystem ParticleSystem;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Spike"))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
            SpawnSpike();
        }
    }

    private void ChangeDirection()
    {
        directionRight = !directionRight;
        if (DirectionV == Vector3.right)
        {
            DirectionV = Vector3.left;
        }
        else
        {
            DirectionV = Vector3.right;
        }
        Kluv1.SetActive(!Kluv1.activeSelf);
        Kluv2.SetActive(!Kluv2.activeSelf);
    }
    // Start is called before the first frame update
    void Start()
    {
        SpikeBuffer = new GameObject[SpikeCont];
    }

    private void SpawnSpike()
    {
        List<int> positionsY = new List<int>();
        foreach (var VARIABLE in SpikeBuffer)
        {
            Destroy(VARIABLE);
        }
        float PosX = 3.5f;
        if (!directionRight)
            PosX = -3.5f;
        for (int j = 0; j < SpikeCont; j++)
        {
            int PosY;
            var GO = Instantiate(Spike);
            while (true)
            {
                PosY = Random.Range(-3,4);
                if (!positionsY.Contains(PosY))
                { 
                    break;
                }
            }
            positionsY.Add(PosY);
            GO.transform.position = new Vector3(PosX,PosY, 0.1f);
            SpikeBuffer[j] = GO;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameGoing)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpStrength);
                ParticleSystem.Play();
            }
            else
            {
                GameGoing = true;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.9f;
            }
        }
        if (GameGoing)
        {
            gameObject.transform.Translate(DirectionV * birdSpeed);
        }
    }
}
