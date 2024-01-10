using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParent : MonoBehaviour
{
    public float health;
    public int stage;
    private void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void damage(float amount) {
        health -= amount;
    }
}
