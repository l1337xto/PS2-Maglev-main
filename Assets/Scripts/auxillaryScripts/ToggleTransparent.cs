using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTransparent : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] material;
    public int x;
    Renderer rend;
    public UIManager uim;
    void Start()
    {
        uim = FindObjectOfType<UIManager>();
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = material[x];
    }
    public void NextMaterial()
    {
        if (x == 0)
        {
            x++;
        }
        else { x--; }
        uim.SwitchView();
    }
}
