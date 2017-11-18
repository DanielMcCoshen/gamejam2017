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
    private attackDelegate currentAttack;
    private attackDelegate[] attacks;
    private Animator anim;
    private bool isAttacking;
 //////////////////////////////////////////////////////////////////////////////
 //                              MAIN METHODS                                //
 //////////////////////////////////////////////////////////////////////////////
    void Start () {
        anim=GetComponent<Animator>();
        attacks=new attackDelegate[1];
        attacks[0]=basicAttack;
        currentAttack = attacks[0];

        inputFunc=leftStick;
	}
	
	// Update is called once per frame
	void Update () {
        inputType input =  inputFunc();
        if (input.attack && !isAttacking){
            StartCoroutine(currentAttack());
        }
        else if (input.block){
            StartCoroutine(block());
        }
        
        Vector3 direction=new Vector3(input.X , 0, input.Y);
        if (direction != Vector3.zero) {
            Quaternion rotation=Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation=rotation;
        }
        

    }

    void changeAttack(int attacktype){
        anim.SetTrigger("Transform");
        currentAttack=attacks[attacktype];
    }

 //////////////////////////////////////////////////////////////////////////////
 //                            ATTACK TYPE METHODS                           //
 //////////////////////////////////////////////////////////////////////////////
    private IEnumerator block(){
        yield return 0;
    }
    private IEnumerator basicAttack() {
        anim.SetTrigger("Attack");
        isAttacking=true;

        yield return new WaitForSeconds(0.5f);
        isAttacking=false;
    }
    //////////////////////////////////////////////////////////////////////////////
    //                             INPUT TYPE METHODS                           //
    //////////////////////////////////////////////////////////////////////////////
    private inputType leftStick() {
        inputType toRet = new inputType();
        toRet.X=Input.GetAxis("C1-H");
        toRet.Y=Input.GetAxis("C1-V");
        toRet.attack=Input.GetAxis("C1-A") > 0.5;
        toRet.block=false;
        return toRet;
    }

}

