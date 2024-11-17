using System.Collections;
using UnityEngine;

public class PauseEnabler : MonoBehaviour
{
    public GameObject thingToEnableOnClick;
    public float enableTime;
    public GameObject thingToDisableOnClick;
    //public float disableTime;
    public GameObject thingToEnableOnStart;
    //public float enableTime;
    public GameObject thingToDisableOnStart;
    //public float disableTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnEnable()
    {
        StartCoroutine(EnableOnStart());
        StartCoroutine(DisableOnStart());
    }
    
    public void BeginEnableDisable()
    {
        StartCoroutine(EnableOnClick());
        StartCoroutine(DisableOnClick());
    }

    // Update is called once per frame
    IEnumerator EnableOnClick()
    {
        yield return new WaitForSeconds(enableTime);
        if (thingToEnableOnClick!=null)
        {
            thingToEnableOnClick.SetActive(true);
        }

        
    }
    IEnumerator DisableOnClick()
    {
        yield return new WaitForSeconds(enableTime);
        if (thingToDisableOnClick != null)
        {
            thingToDisableOnClick.SetActive(false);
        }
        
    }
    IEnumerator EnableOnStart()
    {
        yield return new WaitForSeconds(enableTime);
        if (thingToEnableOnStart != null)
        {
            thingToEnableOnStart.SetActive(true);
        }
        
    }
    IEnumerator DisableOnStart()
    {
        yield return new WaitForSeconds(enableTime);
        if (thingToDisableOnStart != null)
        {
            thingToDisableOnStart.SetActive(false);
        }

        
    }
}
