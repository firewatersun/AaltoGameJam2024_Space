using UnityEngine;

public class ExplodeFinalPlanet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStateController>().ExplodeFinalPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
