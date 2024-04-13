using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyManager : MonoBehaviour {

    public static ButterflyManager instance;
    public GameObject butterflyPrefab;
    public int numButterflies = 20;
    public GameObject[] allButterflies;
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f);
    public Vector3 goalPos = Vector3.zero;

    [Header("Butterfly Settings")]
    public float minSpeed;
    public float maxSpeed;
    public float neighbourDistance;
    public float rotationSpeed;

    void Awake() {
        instance = this;
    }

    void Start() {
        allButterflies = new GameObject[numButterflies];
        for (int i = 0; i < numButterflies; ++i) {
            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));

            allButterflies[i] = Instantiate(butterflyPrefab, pos, Quaternion.identity);
            ButterflyFlock flockComponent = allButterflies[i].GetComponent<ButterflyFlock>();
            if (flockComponent != null) {
                flockComponent.manager = this;  // Ensure this line correctly initializes the manager reference in ButterflyFlock
            }
        }

        goalPos = this.transform.position;
    }

    void Update() {
        if (Random.Range(0, 100) < 10) {
            goalPos = this.transform.position + new Vector3(
                Random.Range(-swimLimits.x, swimLimits.x),
                Random.Range(-swimLimits.y, swimLimits.y),
                Random.Range(-swimLimits.z, swimLimits.z));
        }
    }
}
