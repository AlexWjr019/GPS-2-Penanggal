using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerTarget)
        {
            enemy.SetStrikingDistanceBool(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == PlayerTarget)
        {
            enemy.SetStrikingDistanceBool(false);
        }
    }
}
