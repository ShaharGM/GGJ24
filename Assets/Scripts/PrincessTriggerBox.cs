using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTriggerBox : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            EventManager.onTalk.Invoke();
        }
    }   
}
