using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int health = 5;
    int particleHits = 0;

    void OnEnable(){
        particleHits = 0;
    }
    private void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit(){
        particleHits++;
        if(particleHits >= health){
            gameObject.SetActive(false);
        }
    }
}
