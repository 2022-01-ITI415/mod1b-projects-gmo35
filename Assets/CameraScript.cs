using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    static public GameObject POI;
    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }
    //public GameObject projectile;
    //private Vector3 offset;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        //if(POI == null) return;
        //Vector3 destination = POI.transform.position;
        
        Vector3 destination;
        if(POI == null)
        {
            destination = Vector3.zero;
        } else 
        {
            destination = POI.transform.position;
            if (POI.tag == "Projectile2"){
                if (POI.GetComponent<Rigidbody>().IsSleeping()){
                    POI = null;
                    return;
                }
            }
            destination.z = camZ;
        transform.position = destination;
        }
        //offset = transform.position - projectile.transform.position; 
    }

    // Update is called once per frame
    //void Update()
    //{
        //transform.position = projectile.transform.position + offset;
   // }
}
