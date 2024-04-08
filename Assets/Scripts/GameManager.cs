using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject cubePrefab;
    public GameObject previousCube; 


    public Material startMaterial = null;
    public Material transparentMaterial;

    private Vector3 startPoint; 
    private Vector3 endPoint;
    RaycastHit hit1;
    RaycastHit hit2;
    public LineRenderer lineRenderer1, lineRenderer;
    bool isPuzzleSolved = false;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {

        
        if (!isPuzzleSolved)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray1, out hit1))
                {

                    startPoint = hit1.point;
                    startPoint.x = 3f;
                    lineRenderer.SetPosition(0, startPoint);
                }
            }

            if (Input.GetMouseButton(0)) 
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit2))
                {
                    endPoint = hit2.point;
                    endPoint.x = 3f;
                    Vector3 squareEdge1, squareEdge2;
                    squareEdge1.y = startPoint.y;
                    squareEdge1.z = endPoint.z;
                    squareEdge1.x = 3f;
                    squareEdge2.z = startPoint.z;
                    squareEdge2.y = endPoint.y;
                    squareEdge2.x = 3f;

                    lineRenderer.SetPosition(1, squareEdge1);
                    lineRenderer.SetPosition(2, endPoint);
                    lineRenderer.SetPosition(3, squareEdge2);
                    lineRenderer.SetPosition(4, startPoint);
                    if (previousCube != null)
                    {
                        Destroy(previousCube);
                    }

                    float diagonalLength = Vector3.Distance(startPoint, endPoint);
                   
                    Vector3 midPoint = (startPoint + endPoint) / 2f;

                    
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = midPoint;
                    //cube.transform.localScale = new Vector3(0.1f, diagonalLength, diagonalLength);
                    cube.transform.localScale = new Vector3(0.5f, Mathf.Abs(endPoint.y - startPoint.y), Mathf.Abs(endPoint.z - startPoint.z));

                    cube.GetComponent<Renderer>().material = transparentMaterial;

                    previousCube = cube; 
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out hit2))
                {
                    endPoint = hit2.point;

                }

                startMaterial = hit1.collider.gameObject.GetComponent<Renderer>().material;

                float diagonalLength = Vector3.Distance(startPoint, endPoint);

                Vector3 midPoint = new Vector3(1.3f, (startPoint.y + endPoint.y) / 2f, (startPoint.z + endPoint.z) / 2f);

                GameObject cube = Instantiate(cubePrefab, midPoint, Quaternion.identity);

                cube.transform.localScale = new Vector3(0.5f, Mathf.Abs(endPoint.y - startPoint.y), Mathf.Abs(endPoint.z - startPoint.z));
               
            }
        }

        if (LevelManager.Instance.IsPuzzledSolved())
        {
            Destroy(previousCube);
            isPuzzleSolved = true;
            LevelManager.Instance.panel.SetActive(true);
                
        }
    }
   


}

   


    


