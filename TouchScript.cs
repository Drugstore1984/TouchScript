using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchControl : MonoBehaviour
{
    public Transform place;
    
    
    public bool locked;
    public float deltaX,deltaY;
    #if UNITY_EDITOR||UNITY_WEBGL||UITY_FACEBOOK  
    float lastTimeClicked;
    float maxTime =0.2f;
    #endif
    float timer=0.0f;
    float scaleX;
    float scaleY;
  


    
    private void Awake()
    
    {
        Input.multiTouchEnabled = false;

    }
 
    void OnMouseDrag() 
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x-deltaX,mousePosition.y-deltaY);
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        


        if(Mathf.Abs(transform.position.x-place.position.x) <=0.5f &&
            Mathf.Abs(transform.position.y - place.position.y) <= 0.5f &&transform.rotation.z==place.rotation.z)
        {
            transform.position = new Vector2(place.position.x,place.position.y);
            locked = true;
           
        }
 
        if(locked)
        {
            transform.position = new Vector2(place.position.x,place.position.y);
            Destroy (GetComponent<Collider2D>());
            GetComponent<SpriteRenderer>().sortingLayerName = place.GetComponent<SpriteRenderer>().sortingLayerName;
            GetComponent<SpriteRenderer>().sortingOrder = place.GetComponent<SpriteRenderer>().sortingOrder;
        }
          
    } 
   
    private void OnMouseDown()
    {
        deltaX =Camera.main.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x;
        deltaY =Camera.main.ScreenToWorldPoint(Input.mousePosition).y-transform.position.y;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;

                    #if UNITY_IOS||UNITY_ANDROID
                    
                        for (var i = 0; i < Input.touchCount; ++i)
                        {
                            if (Input.GetTouch(i).phase == TouchPhase.Began) 
                        {
                        if(Input.GetTouch(i).tapCount == 2)
                        {
                        transform.Rotate(0,0,90);
                        }
                        }
                        }
                    #endif
                #if UNITY_EDITOR||UNITY_WEBGL||UITY_FACEBOOK     
                if(Input.GetMouseButton(0))
                {
                    float deltaTime = Time.time - lastTimeClicked;
                    if(deltaTime<maxTime)
                        {
                            transform.Rotate(0,0,90);
                        }
                    lastTimeClicked = Time.time;
                }
                #endif
              
            
        }
        private void OnMouseExit() 
        {
       GetComponent<SpriteRenderer>().sortingOrder = 0;    
        }
    }
