using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float groundOffset = 0.1f; 

    private Transform player;
    private Enemy enemy;
    private Rigidbody rb;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation; 
            rb.useGravity = true;
        }
    }

    void Update()
    {
        if (player == null || enemy == null || enemy.IsDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
           
            Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            Vector3 direction = (playerPosition - transform.position).normalized;

          
            transform.position += direction * moveSpeed * Time.deltaTime;

         
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                transform.position = new Vector3(transform.position.x,
                    hit.point.y + groundOffset,
                    transform.position.z);
            }

           
            transform.LookAt(playerPosition);
        }
    }
}
