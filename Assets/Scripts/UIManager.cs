using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;
    [SerializeField] private GameObject entry;
    [SerializeField] private GameObject expselect;
    [SerializeField] private GameObject maglevselect;
    [SerializeField] private GameObject aboutmaglev;
    [SerializeField] private GameObject view;
    [SerializeField] private GameObject viewXray;
    [SerializeField] private GameObject infoImage;
    [SerializeField] public GameObject maglevScene;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI loadText;
    [SerializeField] private GameObject textHolder;
    [SerializeField] private GameObject graphCL;
    [SerializeField] private GameObject graphVF;
    [SerializeField] private GameObject view1;
    [SerializeField] private GameObject viewXray1;
    [SerializeField] private GameObject Descriptionholder;
    public GameObject cube;
    public GameObject sphere;
    public bool isStartPressed = false;
    private float current = 0.00000f;
    private float load = 0.0000f;
    private float velocity = 00.00000f;
    private float frequency= 00.00000f;
    public SpawnManager spm;
    public static List<float> currentList = new List<float>();
    public static List<float> loadList = new List<float>();
    public static List<float> velocityList = new List<float>();
    public static List<float> frequencyList = new List<float>();
    Animator ani;
    public Material[] material;
    public int x = 0;
    Renderer rend;
    public bool check = false;
    public Slider velocitySlider;
    public Slider loadSlider;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Awake()
    {
        MakeInstance();
    }
    void Start() {
        spm = FindObjectOfType<SpawnManager>();
    }
    void Update() {
        rend.sharedMaterial = material[x];
    }
    public void Login() {
        entry.SetActive(false);
        expselect.SetActive(true);
    }
    public void Logout()
    {
        expselect.SetActive(false);
        entry.SetActive(true);
    }
    public void Maglev() {
        expselect.SetActive(false);
        maglevselect.SetActive(true);
    }

    public void AboutMaglev() {
        maglevselect.SetActive(false);
        aboutmaglev.SetActive(true);
    }

    public void BackMag() {
        if (maglevselect.activeSelf)
        {
            maglevselect.SetActive(false);
            expselect.SetActive(true);
        }
        else if (aboutmaglev.activeSelf)
        {
            aboutmaglev.SetActive(false);
            maglevselect.SetActive(true);
        }
        else {
            maglevScene.SetActive(false);
            maglevselect.SetActive(true);
        }
    }

    public void SwitchView() {
        //cube = GameObject.Find("cube(Clone)");
        //sphere = GameObject.Find("sphere(Clone)");
       // if (cube.activeSelf)
       // {
       //     cube.SetActive(false);
       //     sphere.SetActive(true);
        //}
       // else {
       //     cube.SetActive(true);
       //     sphere.SetActive(false);
        //}

        if (view.activeSelf)
        {
            view.SetActive(false);
            viewXray.SetActive(true);
        }
        else {
            viewXray.SetActive(false);
            view.SetActive(true);
        }
    }

    public void info() {
        if (infoImage.activeSelf)
        {
            infoImage.SetActive(false);
            Descriptionholder.SetActive(true);
        }
        else {
            Descriptionholder.SetActive(false);
            infoImage.SetActive(true);
            
        }
    }

    public void MaglevExp() {
        maglevselect.SetActive(false);
        if (spm.isObjectPlaced)
        {
            maglevScene.SetActive(true);
        }
        check = true;
    }
    public void velocityUpdate(float value) {
        velocityText.text = Mathf.RoundToInt(value) + " km/h";
        velocity = value;
        frequency = (velocity * 5f) / (4.8f * 0.1f * 18f);
        ani = GameObject.Find("idle(Clone)").GetComponent<Animator>();
        ani.speed = value/100;
    }

    public void TakeReading() {
        currentList.Add(current);
        loadList.Add(load);
        velocityList.Add(velocity);
        frequencyList.Add(frequency);
    }
    public void loadUpdate(float value)
    {
        loadText.text = Mathf.RoundToInt(value) + " kg";
        load = value;
        current = (float)Math.Sqrt((double)((load + 1200f) * 9.81f * 4f * 0.01f * 0.01f) / (360f * 360f * 0.038f * 0.0000012566f));
    }
    public void startButton() {
        isStartPressed = true;
        //textHolder.SetActive(true);
        loadSlider.value = 100f;
        velocitySlider.value = 60f;
    }
    public void NextMaterial()
    {
        if (x == 0)
        {
            x++;
        }
        else { x--; }
    }
    public void MagneticField() {
        if (!graphCL.activeSelf)
        {
            
            graphCL.SetActive(true);
        }
        else graphCL.SetActive(false);
    }
    public void ElectricLoad()
    {
        if (!graphVF.activeSelf)
        {
            
            graphVF.SetActive(true);
        }
        else graphVF.SetActive(false);
    }
    public void SwitchView1()
    {
        rend = GameObject.FindWithTag("mag").GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
        if (view1.activeSelf)
        {
            view1.SetActive(false);
            viewXray1.SetActive(true);
        }
        else
        {
            viewXray1.SetActive(false);
            view1.SetActive(true);
        }
        NextMaterial();
    }
}