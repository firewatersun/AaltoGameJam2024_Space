using System.Collections;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameObject[] planets;

    public GameObject player;

    public GameObject planetLabelsTrigger;

    public GameObject planetDestroyTrigger;
    public GameObject turnIntoCubeTrigger;
    public GameObject addBowTieTrigger;
    

    public GameObject resetPlanetsTrigger;
    public GameObject waitTheresOneMore;
    public GameObject finalPlanet;
    public bool canDestroyPlanets;

    public int planetsDestroyed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (planetsDestroyed == 6)
        {
            foreach (GameObject planet in planets)
            {
                if (planet.name != "Planet (4) AM I GOOD ENOUGH")
                {
                    planet.GetComponent<Gravity>().ExplodePlanet();
                }
 
            }
            StartCoroutine(WaitTheresOneMore());
        }
    }

    public void TurnOnLabels()
    {

            foreach (GameObject planet in planets)
            {
                StartCoroutine("TurnOnLabelsCoroutine", planet);
            }
    }

    IEnumerator TurnOnLabelsCoroutine(GameObject planet)
    {
        float randomWait = Random.Range(0.2f, 4f);
        yield return new WaitForSeconds(randomWait);
        planet.GetComponent<Gravity>().labels = true;
    }
    
    public void TurnOnSpecialLabels()
    {

        foreach (GameObject planet in planets)
        {
            planet.GetComponent<Gravity>().specialLabels = true;
        }
    }
    

    public void StartGravity()
    {
        player.GetComponent<Gravity>().affectedByGravity = true;
        foreach (GameObject planet in planets)
        {
            StartCoroutine("StartGravityTimer", planet);
        }
    }

    IEnumerator StartGravityTimer(GameObject planet)
    {
        float randomWait = Random.Range(0.2f, 2.5f);
        yield return new WaitForSeconds(randomWait);
        planet.GetComponent<Gravity>().affectedByGravity = true;
 
    }
    
    public void EnableDestroyPlanets()
    {
        foreach (GameObject planet in planets)
        {
            planet.GetComponent<Gravity>().ActivateDestroyLabels();
            planet.GetComponent<Gravity>().destroyLabels = true;
        }
    }

    public void ResetPlanets()
    {
        planetsDestroyed = 0;
        player.GetComponent<Gravity>().affectedByGravity = false;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.transform.position = player.GetComponent<Gravity>().startPosition;
        foreach (GameObject planet in planets)
        {
            planet.GetComponent<Gravity>().affectedByGravity = false;
            planet.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            planet.GetComponent<Gravity>().destroyLabels = false;
            planet.GetComponent<Gravity>().labels = false;
            planet.transform.position = planet.GetComponent<Gravity>().startPosition;
        }
    }

    IEnumerator WaitTheresOneMore()
    {
        planetsDestroyed = 0;
        Vector3 direction = player.transform.position - finalPlanet.transform.position;
        finalPlanet.GetComponent<Rigidbody>().AddForce(8f*direction.normalized*GravityValues.gravValue, ForceMode.Impulse);
        yield return new WaitForSeconds(6f);
        waitTheresOneMore.SetActive(true);
    }

    public void ExplodeFinalPlanet()
    {
        finalPlanet.GetComponent<Gravity>().ExplodePlanet();
        planetsDestroyed = 0;
    }

    public void MakePlanetsVisible()
    {
        foreach (GameObject planet in planets)
        {
            StartCoroutine("MakePlanetsVisibleCoroutine", planet);
        }
        
    }

    IEnumerator MakePlanetsVisibleCoroutine(GameObject planet)
    {
        float randomWait = Random.Range(0.2f, 7f);
        yield return new WaitForSeconds(randomWait);
        planet.SetActive(true);
    }

}
