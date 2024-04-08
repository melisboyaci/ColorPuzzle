using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ChangeMaterial : MonoBehaviour
{
    public List<GameObject> collidedObjects = new List<GameObject>();
    public Material material;

    private float scaleAmount = 0.75f; 
    private float tweenDuration = 0.4f; 

    private Vector3 originalScale;


    private void Start()
    {

       
    }

    private void Update()
    {
        
        if (collidedObjects.Count > 0)
        {
            for (int i = 0; i < collidedObjects.Count; i++)
            {
                collidedObjects[i].gameObject.GetComponent<Renderer>().material = GameManager.Instance.startMaterial;
                originalScale = collidedObjects[i].gameObject.transform.localScale;
                Sequence sequence = DOTween.Sequence();

                sequence.Append(collidedObjects[i].gameObject.transform.DOScale(originalScale * scaleAmount, tweenDuration)); 
                sequence.Append(collidedObjects[i].gameObject.transform.DOScale(originalScale, tweenDuration)); 
            }
            Destroy(gameObject);

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("SelectionCube"))
        {
            GameObject collidedObject = other.gameObject;
            collidedObjects.Add(collidedObject);
           
        }
        
    }

   


}
