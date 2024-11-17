using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Gravity : MonoBehaviour
{
    public bool affectedByGravity;
    public Vector3 startPosition;
    public float gravityRadiusMultiplier;
    public float gravityValue;
    public float mass;
    float gravityRadius;
    float innerGravityRadius;
    float outerGravityRadius;
    private float minGravForce;
    float maxGravForce;
    public float personalGravityMultiplier;
    public float personalGravityRadiusMultiplier;
    public bool labels;
    public GameObject label;
    public bool specialLabels;
    public GameObject specialLabel;
    public bool destroyLabels;
    public GameObject destroyLabel;
    public GameObject explodeParticle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        destroyLabels = false;
        affectedByGravity = false;
        startPosition = gameObject.transform.position;
        gravityValue = GravityValues.gravValue*personalGravityMultiplier;
        gravityRadiusMultiplier = GravityValues.gravRadiusMultiplier*personalGravityRadiusMultiplier;
        minGravForce = GravityValues.minGravForce;
        maxGravForce = GravityValues.maxGravForce;
        mass = gameObject.transform.lossyScale.x;
        gravityRadius = gameObject.transform.lossyScale.x*gravityRadiusMultiplier;
       innerGravityRadius = gameObject.transform.lossyScale.x*1.2f;
       outerGravityRadius = gameObject.transform.lossyScale.x*6.1f;
       gameObject.GetComponent<Rigidbody>().mass = mass;
       if (label!= null)
       {
           label.SetActive(false);
       }

       if (specialLabel!= null)
       {
           specialLabel.SetActive(false);
       }
       if (destroyLabel!= null)
       {
           destroyLabel.SetActive(false);
       }
       
    }

    // Update is called once per frame
    void Update()
    {
        mass = gameObject.transform.lossyScale.x;

        if (affectedByGravity)  
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, gravityRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<Gravity>().affectedByGravity)
                {
                                        if ((hitCollider.transform.position - gameObject.transform.position).magnitude > outerGravityRadius)
                                        {
                                            float distanceSquared = (hitCollider.transform.position - gameObject.transform.position).magnitude * (hitCollider.transform.position - gameObject.transform.position).magnitude;
                                            float force = (gravityValue * mass * hitCollider.GetComponent<Gravity>().mass )/distanceSquared;
                                            float forceClamped = Mathf.Clamp(force, minGravForce, maxGravForce);
                                            Vector3 gravityDirection = (hitCollider.transform.position - gameObject.transform.position).normalized;
                                            hitCollider.gameObject.GetComponent<Rigidbody>().AddForce(-1.6f*gravityDirection*forceClamped);
                    
                                        }
                    if ((hitCollider.transform.position - gameObject.transform.position).magnitude > innerGravityRadius && (hitCollider.transform.position - gameObject.transform.position).magnitude <= outerGravityRadius )
                    {
                        float distanceSquared = (hitCollider.transform.position - gameObject.transform.position).magnitude * (hitCollider.transform.position - gameObject.transform.position).magnitude;
                        float force = (gravityValue * mass * hitCollider.GetComponent<Gravity>().mass )/distanceSquared;
                        float forceClamped = Mathf.Clamp(force, minGravForce, maxGravForce);
                        Vector3 gravityDirection = (hitCollider.transform.position - gameObject.transform.position).normalized;
                        hitCollider.gameObject.GetComponent<Rigidbody>().AddForce(-1*gravityDirection*forceClamped);

                    }
                
                    if ((hitCollider.transform.position - gameObject.transform.position).magnitude <= innerGravityRadius)
                    {
                        float distanceSquared = (hitCollider.transform.position - gameObject.transform.position).magnitude * (hitCollider.transform.position - gameObject.transform.position).magnitude;
                        float force = (gravityValue * mass * hitCollider.GetComponent<Gravity>().mass )/distanceSquared;
                        float forceClamped = Mathf.Clamp(force, 2*minGravForce, 2*maxGravForce);
                        Vector3 gravityDirection = (hitCollider.transform.position - gameObject.transform.position).normalized;
                        hitCollider.gameObject.GetComponent<Rigidbody>().AddForce(1*gravityDirection*forceClamped);

                    }
                }
               

            }
        }
        
        if (labels && label != null)
        {
            label.SetActive(true);
        }
        if (specialLabels && specialLabel != null)
        {
            specialLabel.SetActive(true);
        }
        
    }

    public void EnableGravity()
    {
    gameObject.GetComponent<SphereCollider>().radius = gravityRadius;
    affectedByGravity = true;
    }

    public void ActivateDestroyLabels()
    {
        if (destroyLabel!= null)
        {
            destroyLabel.SetActive(true);
        }

        
    }

    public void ExplodePlanet()
    {
        GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameStateController>().planetsDestroyed += 1;
        if (label != null)
        {
            label.SetActive(false);
        }

        if (specialLabel != null)
        {
            specialLabel.SetActive(false);
        }

        if (destroyLabel != null)
        {
            destroyLabel.SetActive(false);
        }

        Transform currentTransform = gameObject.transform;
        GameObject.Instantiate(explodeParticle, currentTransform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
