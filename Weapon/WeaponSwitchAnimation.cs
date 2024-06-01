using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchAnimation : MonoBehaviour
{

    public Animator animator;

    private void OnEnable()
    {
        //Inventory.scroll += PlaySwitchAnimation;
    }

    private void OnDisable()
    {
        //Inventory.scroll -= PlaySwitchAnimation;
    }

    public void PlaySwitchAnimation()
    {
        animator.SetBool("Switching", true);
        StartCoroutine(animationWait());
        animator.SetBool("Switching", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator animationWait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
