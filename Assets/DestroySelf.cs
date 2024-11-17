using System.Collections;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float destroyTime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        StartCoroutine(DestroySelfCoroutine());
    }

    // Update is called once per frame
    IEnumerator DestroySelfCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
