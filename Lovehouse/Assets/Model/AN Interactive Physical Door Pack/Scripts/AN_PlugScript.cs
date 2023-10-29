﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_PlugScript : MonoBehaviour
{
    [Tooltip("Feature for one using only")]
    public bool OneTime = false;
    [Tooltip("Plug follow this local EmptyObject")]
    public Transform HeroHandsPosition;
    [Tooltip("SocketObject with collider(shpere, box etc.) (is trigger = true)")]
    public Collider Socket; // need Trigger
    public AN_DoorScript DoorObject;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    public GameObject inttext;

    public bool follow = false, isConnected = false, followFlag = false, youCan = true, interactable = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (youCan) Interaction();

        // frozen if it is connected to PowerOut
        if (isConnected)
        {

            AudioManager.PlayMusic2();
            gameObject.transform.position = Socket.transform.position;
            gameObject.transform.rotation = Socket.transform.rotation;
            DoorObject.isOpened = true;



        }
        else
        {
            DoorObject.isOpened = false;
        }

        if (HeroHandsPosition == null) Debug.LogError("HeroHandsPosition is null. Please, set it in inspector.");

    }

    void Interaction()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.R) && !follow)
        {
            isConnected = false; // unfrozen
            follow = true;
            followFlag = false;
        }

        if (follow)
        {
            rb.drag = 10f;
            rb.angularDrag = 10f;
            if (followFlag)
            {
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                if (distance > 3f || Input.GetKeyDown(KeyCode.E))
                {
                    follow = false;
                }
            }

            followFlag = true;

            // rb.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f);
            // second variant of following
            StartCoroutine(MoveToPosition(HeroHandsPosition.position, 0.1f));
        }
        else
        {
            rb.drag = 0f;
            rb.angularDrag = .5f;
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 3f && angleView < 35f) return true;
        else return false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inttext.SetActive(false);
            interactable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MainCamera"))
        {
            inttext.SetActive(true);
            interactable = true;
        }

        if (other == Socket)
        {
            isConnected = true;
            follow = false;
            DoorObject.rbDoor.AddRelativeTorque(new Vector3(0, 0, -200f));
            AudioManager.PlayEnemyRoar();
        }
        if (OneTime) youCan = false;
    }
}
