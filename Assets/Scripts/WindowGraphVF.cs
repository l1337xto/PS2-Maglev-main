using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;
public class WindowGraphVF : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashX;
    private RectTransform dashY;
    private RectTransform labX;
    private RectTransform labY;
    //UIManager.loadList or UIManager.instance.loadList
    //UIManager.currentList

    private void Awake() {
    	graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    	labelTemplateX=graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
    	labelTemplateY=graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
    	dashX=graphContainer.Find("dashX").GetComponent<RectTransform>();
    	dashY=graphContainer.Find("dashY").GetComponent<RectTransform>();
    	labX=graphContainer.Find("xLabels").GetComponent<RectTransform>();
    	labY=graphContainer.Find("yLabels").GetComponent<RectTransform>();
    	List<int> valueList = new List<int>(); //{5,60,56,52,48,44,40,30,31,34,36,37,41,45,48,49,50,52,53,57,60,65,66,67};
    	//for(int i=0;i<5;i++){
    	//	valueList.Add(UnityEngine.Random.Range(-100,100));
    		    //UIManager.currentList.Add(UnityEngine.Random.Range(-100f,100f));
    		   // UIManager.loadList.Add(UnityEngine.Random.Range(0,100f));
    	//}

    	ShowGraph(UIManager.velocityList, -1,(int _i) => UIManager.frequencyList[_i].ToString().Substring(0,5), (float _f) => _f.ToString().Substring(0,5)); // custom labels
    }

    private GameObject CreateCircle(Vector2 anchoredPosition){
    	GameObject gameObject = new GameObject("circle", typeof(Image));
    	gameObject.transform.SetParent(graphContainer,false);
    	gameObject.GetComponent<Image>().sprite = circleSprite;
    	RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
    	rectTransform.anchoredPosition=anchoredPosition;
    	rectTransform.sizeDelta = new Vector2(11,11);
    	rectTransform.anchorMin = new Vector2(0,0);
    	rectTransform.anchorMax = new Vector2(0,0);
    	return gameObject;
    }

    private void ShowGraph(List<float> valueList, int maxVisibleValueAmount=-1,Func<int,string> getAxisXLabel=null, Func<float,string> getAxisYLabel=null) {
    	if(getAxisXLabel==null){
    		getAxisXLabel = delegate (int _i){ return _i.ToString().Substring(0,5);};
    	}
    	if(getAxisYLabel==null){
    		getAxisYLabel = delegate (float _f){ return _f.ToString().Substring(0,5);};
    	}
    	
    	float yMax=valueList[0]; // range detection
    	float yMin = valueList[0];
    	if(maxVisibleValueAmount<=0){
    		maxVisibleValueAmount=valueList.Count;
    	}

    	for(int i=Mathf.Max(0,UIManager.velocityList.Count-maxVisibleValueAmount);i<valueList.Count;i++){
    		float values = valueList[i];
    		if(values>yMax){
    			yMax=values;
    		}
    		if(values < yMin){
    			yMin = values;
    		}
    	}

    	float yDiff=yMax-yMin;
    	if(yDiff<=0){
    		yDiff=5f;
    	}

    	yMax=yMax+((yDiff)*.2f);
    	yMin=yMin-((yDiff)*.2f);

    	float graphWidth = graphContainer.sizeDelta.x;
    	float graphHeight = graphContainer.sizeDelta.y;
    	
    	float xSize = graphWidth/(maxVisibleValueAmount+1);
    	
    	GameObject lastCircleGameObject = null;

    	int xIndex=0;
    	
    	for(int i=Mathf.Max(UIManager.frequencyList.Count-maxVisibleValueAmount,0);i<UIManager.frequencyList.Count;i++){
    		float xPos = xSize+xIndex*xSize;
    		float yPos = ((valueList[i]-yMin) / (yMax-yMin))*graphHeight;
    		GameObject circleGameObject = CreateCircle(new Vector2(xPos,yPos));
    		if(lastCircleGameObject!=null){
    			CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
    		}
    		lastCircleGameObject = circleGameObject;
// Value to display on X axis    		
    		RectTransform labelX = Instantiate(labelTemplateX);
    		labelX.SetParent(graphContainer);
    		labelX.gameObject.SetActive(true);
    		labelX.anchoredPosition = new Vector2(xPos,-7f);
    		labelX.GetComponent<Text>().text = getAxisXLabel(i);

    		RectTransform ldashX = Instantiate(dashY);
    		ldashX.SetParent(graphContainer);
    		ldashX.gameObject.SetActive(true);
    		ldashX.anchoredPosition = new Vector2(xPos,-7f);
// Axis Labels
/*
 * to change the axis labels change the value here
 * */

    		RectTransform notationX = Instantiate(labX);
    		notationX.SetParent(graphContainer);
    		notationX.gameObject.SetActive(true);
    		notationX.anchoredPosition = new Vector2(20, -220);

    		RectTransform notationY = Instantiate(labY);
    		notationY.SetParent(graphContainer);
    		notationY.gameObject.SetActive(true);
 /*Y label */notationY.anchoredPosition = new Vector2(-215, 15); 
    		
    		xIndex++;	
    	}
    	int separatorCount = 10;
    		
    		for(int i=0;i<=separatorCount;i++){
    			RectTransform labelY = Instantiate(labelTemplateY);
    			labelY.SetParent(graphContainer);
    			labelY.gameObject.SetActive(true);
    			float normalizedValue = i*1f/separatorCount;
    			labelY.anchoredPosition = new Vector2(-7f,normalizedValue*graphHeight);
    			labelY.GetComponent<Text>().text = getAxisYLabel(yMin+normalizedValue*(yMax-yMin));

    			RectTransform ldashY = Instantiate(dashX);
	    		ldashY.SetParent(graphContainer);
	    		ldashY.gameObject.SetActive(true);
	    		ldashY.anchoredPosition = new Vector2(-4f,normalizedValue*graphHeight);
    		}
    }

    private void CreateDotConnection(Vector2 aPos, Vector2 bPos){
    	GameObject gameObject = new GameObject("dotConnection",typeof(Image));
    	gameObject.transform.SetParent(graphContainer,false);
    	gameObject.GetComponent<Image>().color = new Color(1,1,1,.5f);
    	RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
    	Vector2 dir = (bPos-aPos).normalized;
    	float distance = Vector2.Distance(aPos,bPos);
    	rectTransform.sizeDelta = new Vector2(distance,1f);
    	rectTransform.anchorMin = new Vector2(0,0);
    	rectTransform.anchorMax = new Vector2(0,0);
    	rectTransform.anchoredPosition=aPos+ dir* distance * .5f;
    	rectTransform.localEulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
