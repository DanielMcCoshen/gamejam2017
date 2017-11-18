using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
////////////////////////////////////////////////////////////////////////////////
//                                   TYPES                                    //
////////////////////////////////////////////////////////////////////////////////
    struct inputType{
        public float X;
        public float Y;
        public bool attack;
        public bool block;
    }
   
    delegate inputType inputDelegate();
    delegate IEnumerator attackDelegate();

 //////////////////////////////////////////////////////////////////////////////
 //                            INSTANCE VARIABLES                            //
 //////////////////////////////////////////////////////////////////////////////
    private inputDelegate inputFunc;
    private attackDelegate attack;

 //////////////////////////////////////////////////////////////////////////////
 //                              MAIN METHODS                                //
 //////////////////////////////////////////////////////////////////////////////
    void Start () {
        attack = basicAttack;
	}
	
	// Update is called once per frame
	void Update () {
        inputType input =  inputFunc();
        if (input.attack) {
            StartCoroutine(attack());
        }
        else if (input.block) {
            StartCoroutine(block());
        }
	}


 //////////////////////////////////////////////////////////////////////////////
 //                            ATTACK TYPE METHODS                           //
 //////////////////////////////////////////////////////////////////////////////
    private IEnumerator block(){
        yield return 0;
    }
    private IEnumerator basicAttack() {
        yield return 0;
    }
}
