using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabtoplace;
    public GameObject prefabtoplace2;
    public GameObject cube;
    public PlacementManager pcm;
    public UIManager uim;
    public bool isObjectPlaced = false;
    Animator ani;
    void Start()
    {
        pcm = FindObjectOfType<PlacementManager>();
        uim = FindObjectOfType<UIManager>();
    }
    //GameObject.Find("UIManager").GetComponent<UIManager>().isStartPressed
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isObjectPlaced && uim.check)
        {
            PlaceObject();
            uim.maglevScene.SetActive(true);
        }
    }
    private void PlaceObject()
    {
        Instantiate(prefabtoplace, pcm.transform.position, pcm.transform.rotation);
        //Instantiate(prefabtoplace2, pcm.transform.position, pcm.transform.rotation);
        ani = GameObject.Find("idle(Clone)").GetComponent<Animator>();
        ani.speed = 0;
        //cube.SetActive(false);
        isObjectPlaced = true;
        pcm.visual.SetActive(false);
    }
}
