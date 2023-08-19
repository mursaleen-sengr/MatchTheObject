using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    destroyingobject destroyingobject;
     public GameObject des;
    private Animator destroyAnimation;
     public ParticleSystem playParticalSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        destroyingobject = des.GetComponent<destroyingobject>();
        destroyAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void destroyAnim()
    {
        if (destroyingobject.isAnimating==true)
        {
            startAnimation();

            Invoke("stopAnimation", 1f);
           
        }
    }
    public void startAnimation()
    {
        destroyAnimation.SetBool("isAnimating", true);
        //print("playedAnimation");
        playParticalSystem.Play();
           
    }
    public void stopAnimation()
    {
        destroyingobject.isAnimating = false;
        destroyAnimation.SetBool("isAnimating", false);
    }
}
