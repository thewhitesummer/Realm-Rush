﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public Waypoint waypointPosition;

    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float attackRange = 30f;

    Transform targetEnemy;
    
    void Update() {
        SetTargetEnemy();
        if(targetEnemy) {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else {
            Shoot(false);
        }
    }

    private void SetTargetEnemy() {
        var activeEnemies = FindObjectsOfType<EnemyDamage>();
        if(activeEnemies.Length == 0) { return; }
        Transform closestEnemy = activeEnemies[0].transform;
        foreach(EnemyDamage testEnemy in activeEnemies) {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB) {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);
        if(distToA < distToB) {
            return transformA;
        }
        return transformB;
    }

    private void FireAtEnemy() {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, transform.position);
        if(distanceToEnemy <= attackRange) {
            Shoot(true);
        }
        else {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive) {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

    public void ChangeTowerWaypoint(Waypoint newWaypoint) {
        transform.position = newWaypoint.transform.position;
        waypointPosition = newWaypoint;
    }
}
