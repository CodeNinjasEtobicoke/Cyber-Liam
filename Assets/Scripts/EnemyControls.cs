﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField]
    private bool isFollowingTarget;

    public float speed = 2f;
    public float attackingDistance = 0.6f;

    private Animator animatorEnemy;
    private Rigidbody rigidbodyEnemy;
    private Transform target;

    

    private float currentAttackingTime = 0f;
    private float maxAttackingTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animatorEnemy = GetComponent<Animator>();
        rigidbodyEnemy = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);

        isFollowingTarget = Vector3.Distance(transform.position, target.position) >= attackingDistance;

        if (isFollowingTarget)
        {
            rigidbodyEnemy.velocity = transform.forward * speed;
        }
        else
        {
            Attack();
        }

        animatorEnemy.SetBool("Walk", isFollowingTarget);
    }
    

    void Attack()
    {
        rigidbodyEnemy.velocity = Vector3.zero;

        currentAttackingTime += Time.deltaTime;
        
        if (currentAttackingTime > maxAttackingTime)
        {
            currentAttackingTime = 0f;
            int rand = Random.Range(1, 5);
            animatorEnemy.SetTrigger("Attack"+ rand);
        }
    }
}
