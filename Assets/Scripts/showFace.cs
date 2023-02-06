using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class showFace : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {
       NonRenderFace();

   }

   // Update is called once per frame
   void Update()
   {
      
       StartCoroutine(waiter());
       //Debug.Log("hej");

   }

   void RenderFace()
   {
       //gameObject.active = true;
       GameObject.Find ("Person1").transform.localScale = new Vector3(1, 1, 1);
   }

   void NonRenderFace()
   {
       //gameObject.active = false;                                Makes object inactive and cant find it then
       //GetComponent("MeshRenderer").enabled = false;             MeshRenderer not a type..?  
      
       GameObject.Find ("Solomon").transform.localScale = new Vector3(0, 0, 0);
   }
  
   IEnumerator waiter()
   {
       //Wait for 4 seconds
       yield return new WaitForSeconds(4);
       RenderFace();
   }
}
