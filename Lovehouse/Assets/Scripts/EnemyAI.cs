using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, detectionDistance, catchDistance, minChaseTime, maxChaseTime, minSearchTime, maxSearchTime, jumpscareTime;
    public bool walking, chasing, finalchase;
    public Transform player;
    public Transform currentDest;
    public Transform dest;
    public Transform rayCastOffset;
    public string deathScene;
    public float aiDistance;
    public GameObject deathCamera;

    public GameObject holyKey;

    Vector3 rayCastoffset = new(0, -1f, 0);
    public bool isFinish = false;

    void Start()
    {
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        aiDistance = Vector3.Distance(player.position, transform.position);
    }
    void Update()
    {
        aiDistance = Vector3.Distance(player.position, transform.position);
        Vector3 forwardDirection = (player.transform.position - transform.position).normalized;

        
        // Check for obstacles within the vision range.

        // To ensure that the raycast at the body of Player

        RaycastHit hit;


        if (holyKey.gameObject.GetComponent<AN_PlugScript>().isConnected == true)
        {
            finalchase = true;
            
            if(finalchase == true && isFinish == false)
            {
                Debug.Log("Final chase!");
                dest = player.transform;
                ai.destination = dest.position;

                // To full speed
                ai.speed = chaseSpeed;

                // Reset all triggers
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");

                aiAnim.SetTrigger("sprint");

                if (aiDistance <= catchDistance)
                {
                    Debug.Log("Player is caught!");
                    player.gameObject.SetActive(false);
                    holyKey.SetActive(false);

                    deathCamera.SetActive(true);

                    aiAnim.ResetTrigger("walk");
                    aiAnim.ResetTrigger("idle");
                    aiAnim.ResetTrigger("sprint");
                    aiAnim.SetTrigger("kill");
                

                    StartCoroutine(deathRoutine());
                    finalchase = false;
                    isFinish = true;
                }
            }
            return;
        }

        
        Debug.DrawRay(rayCastOffset.position + rayCastoffset, forwardDirection * detectionDistance, Color.green);
        if (Physics.Raycast(rayCastOffset.position + rayCastoffset, forwardDirection, out hit, detectionDistance))
        {
            // Check if the hit object is within the field of view angle.
            if (hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                chasing = true;

                // Stop idling and give chase
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");

            }
            
        }

        // Final Chase after player plug in
        

        if (chasing == true)
        {
            dest = player.transform;
            ai.destination = dest.position;

            // To full speed
            ai.speed = chaseSpeed;

            // Reset all triggers
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");

            aiAnim.SetTrigger("sprint");


            AudioManager.PlayEnemyRun();

            // If player get out of catch distance, stop chasing
            // Else if player is in catch distance, kill player
            if (aiDistance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                holyKey.SetActive(false);

                deathCamera.SetActive(true);

                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("sprint");
                aiAnim.SetTrigger("kill");
            

                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }

        

        if (walking == true)
        {
            dest = currentDest.transform;
            ai.destination = dest.position;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");

            AudioManager.PlayEnemyWalk();
            
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;

                AudioManager.PlayEnemyIdle();
            }
        }
    }
    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseRoutine");
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    IEnumerator chaseRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minChaseTime, maxChaseTime));
        stopChase();
    }
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(1);
        // Timing for bite animation
        AudioManager.PlayEnemyBite();

        // disable running agent
        ai.isStopped = true;
        
        // Waiting for bite animation to finish
        yield return new WaitForSeconds(3);
        Debug.Log("Dooing Roar");
        // Play roar audio
        AudioManager.PlayEnemyRoar();
        
        // Waiting for roar audio to finish
        yield return new WaitForSeconds(7);
        aiAnim.SetTrigger("idle");

        

        // // Load death scene
        SceneManager.LoadScene(deathScene);

    }

    IEnumerator justRoar()
    {
        AudioManager.PlayEnemyRoar();
        yield return new WaitForSeconds(1);
    }
}
