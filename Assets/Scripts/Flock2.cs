using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyFlock : MonoBehaviour {

    public ButterflyManager manager;  // Add this line
    float speed;
    bool turning = false;

    void Start() {
        // This checks if a manager is assigned, if not, it tries to find one in the scene
        if (manager == null) {
            manager = FindObjectOfType<ButterflyManager>();
        }

        if (manager != null) {
            speed = Random.Range(manager.minSpeed, manager.maxSpeed);
        } else {
            Debug.LogWarning("ButterflyManager not found. Please assign a ButterflyManager to the flock.");
        }
    }

    void Update() {
        if (manager == null) return;

        Bounds b = new Bounds(manager.transform.position, manager.swimLimits * 2.0f);

        if (!b.Contains(transform.position)) {
            turning = true;
        } else {
            turning = false;
        }

        if (turning) {
            Vector3 direction = manager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime);
        } else {
            if (Random.Range(0, 100) < 10) {
                speed = Random.Range(manager.minSpeed, manager.maxSpeed);
            }

            if (Random.Range(0, 100) < 10) {
                ApplyButterflyRules();
            }
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void ApplyButterflyRules() {
        if (manager == null) return;

        GameObject[] gos = manager.allButterflies;
        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.01f;
        float mDistance;
        int groupSize = 0;

        foreach (GameObject go in gos) {
            if (go != this.gameObject) {
                mDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (mDistance <= manager.neighbourDistance) {
                    vCentre += go.transform.position;
                    groupSize++;

                    if (mDistance < 1.0f) {
                        vAvoid += (this.transform.position - go.transform.position);
                    }

                    ButterflyFlock anotherFlock = go.GetComponent<ButterflyFlock>();
                    gSpeed += anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {
            vCentre = vCentre / groupSize + (manager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            if (speed > manager.maxSpeed) {
                speed = manager.maxSpeed;
            }

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if (direction != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
