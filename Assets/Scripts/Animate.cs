using UnityEngine;

public class Animate : MonoBehaviour {
    private Animator anim;
    public boolean Terrified;


    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (Terrified == true);
            
        }
    }
}