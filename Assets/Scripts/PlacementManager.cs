using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ARRaycastManager rayManager;
    public GameObject visual;
    public SpawnManager spm;
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        spm = FindObjectOfType<SpawnManager>();
        visual = this.transform.GetChild(0).gameObject;
        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (!spm.isObjectPlaced)
        
            if (hits.Count > 0)
            {

                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                


                if (!visual.activeInHierarchy)
                    visual.SetActive(true);
            }
       // }
    }
      
    }

