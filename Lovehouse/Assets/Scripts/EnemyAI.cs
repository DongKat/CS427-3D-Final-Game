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
    public bool walking, chasing;
    public Transform player;
    public Transform currentDest;
    public Transform dest;
    public Transform rayCastOffset;
    public string deathScene;
    public float aiDistance;
    public GameObject hideText, stopHideText;

    void Start()
    {
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
    }
    void Update()
    {
        Vector3 forwardDirection = (player.transform.position - transform.position).normalized;
        // Check for obstacles within the vision range.

        // To ensure that the raycast at the body of Player
        Vector3 rayCastoffset = new Vector3(0, -1f, 0);

        RaycastHit hit;
        Debug.DrawRay(rayCastOffset.position + rayCastoffset, forwardDirection * detectionDistance, Color.green);
        if (Physics.Raycast(rayCastOffset.position + rayCastoffset, forwardDirection, out hit, detectionDistance))
        {
            // Check if the hit object is within the field of view angle.
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                walking = false;
                chasing = true;

                // Stop idling and give chase
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
            }

        }


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

            // If player get out of catch distance, stop chasing
            // Else if player is in catch distance, kill player
            // if (aiDistance <= catchDistance)
            // {
            //     // Remove player from scene
            //     player.gameObject.SetActive(false);

            //     aiAnim.ResetTrigger("walk");
            //     aiAnim.ResetTrigger("idle");
            //     aiAnim.ResetTrigger("sprint");

            //     // hideText.SetActive(false);
            //     // stopHideText.SetActive(false);

            //     // Change to bite animation
            //     aiAnim.SetTrigger("jumpscare");

            //     StartCoroutine(deathRoutine());
            //     chasing = false;

            //     // This end game
            // }
        }

        if (walking == true)
        {
            dest = currentDest.transform;
            ai.destination = dest.position;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
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
        yield return new WaitForSeconds(jumpscareTime);

        // Tells GameManager that player is dead

        // SceneManager.LoadScene(deathScene);
    }
}
