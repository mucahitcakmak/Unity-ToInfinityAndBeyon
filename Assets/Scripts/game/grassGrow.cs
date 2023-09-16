using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassGrow : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") && coll.gameObject.CompareTag("Player")){
            Animation anim = GetComponent<Animation>();
            anim.Play(transform.name);        
        }
    }
}
