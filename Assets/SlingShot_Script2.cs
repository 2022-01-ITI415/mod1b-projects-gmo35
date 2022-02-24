using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot_Script2 : MonoBehaviour
{
    static private SlingShot_Script2 S;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile2;
    public bool aimingMode;

    private Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS{
        get{
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    void Awake(){
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown(){
        aimingMode = true;
        projectile2 = Instantiate(prefabProjectile) as GameObject;
        projectile2.transform.position = launchPos;
        projectile2.GetComponent<Rigidbody>().isKinematic = true;

        projectileRigidbody = projectile2.GetComponent<Rigidbody>();
    }

    void Update(){
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D-launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude){
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile2.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)){
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            CameraScript.POI = projectile2;
            projectile2 = null;
            //ShotsFired.ShotFired();
            //ProjectileLine.S.poi = projectile2;
        }
    }
}
