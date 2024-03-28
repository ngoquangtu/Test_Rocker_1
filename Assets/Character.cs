using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private CharacterController controller;
    [HideInInspector] public Material characterMaterial;
    public static Character Instance;
    public bool gameOver = false;
    public bool isMove = true;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        characterMaterial = GetComponent<Renderer>().material;
        Debug.Log(characterMaterial.color);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeCharacterColor();


    }
    private void Update()
    {
        if (isMove)
        {
            movement(); 
        }
      
        
    }

    private void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        controller.Move(dir * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {

            if(other.gameObject.GetComponent<Renderer>().material.color==characterMaterial.color)
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.score++;
                Debug.Log("Score:" + GameManager.Instance.score);
                ChangeCharacterColor();
                StartCoroutine(SpawnObjects());
            }
            else
            {
                gameOver = true;
                isMove = false;
            }
        }
        
    }
    private void ChangeCharacterColor()
    {
        characterMaterial = GetRandomMaterial();
        GetComponent<Renderer>().material = characterMaterial;
    }
    private Material GetRandomMaterial()
    {
        int randomIndex = Random.Range(0, ObjectPooling.Instance.ballMaterials.Length);
        Debug.Log("stt random " + randomIndex);
        switch (randomIndex)
        {
            case 0:
                return ObjectPooling.Instance.ballMaterials[0];
            case 1:
                return ObjectPooling.Instance.ballMaterials[1];
            case 2:
                return ObjectPooling.Instance.ballMaterials[2];
        }
        return ObjectPooling.Instance.ballMaterials[0];
    }
    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject obj = ObjectPooling.Instance.GetPooledObject();
            if (obj != null)
            {
            
                obj.SetActive(true);

            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
