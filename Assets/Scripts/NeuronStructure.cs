using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronStructure : MonoBehaviour
{
    static int axonLength = 6;
    static int axonTerminalsLength = 2;
    static Vector3 cellBodyScale = new Vector3(1f,1f,1f);
    static Vector3 myelinSheatScale = new Vector3(0.7f,0.7f,0.7f);
    static Vector3 axonTerminalScale = new Vector3(0.7f,2f,0.7f);
    static float standardCapsuleLength = 2;

    public Material CompartmentMaterial;
    public Gradient Gradient;
    [Range(0.1f, 0.3f)]
    public float GradientFalloff = 0.1f;

    public GameObject CellBody;
    GameObject[] axon = new GameObject[axonLength];
    GameObject[] axonTerminals = new GameObject[axonTerminalsLength];

    void Start()
    {
        BuildCellBody();
        BuildMyelinSheat();
        BuildBifurcation();   
    }

    void BuildCellBody()
    {
        CellBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        CellBody.name = "CellBody";
        CellBody.transform.localScale = cellBodyScale;
        CellBody.transform.parent = transform;
        CellBody.AddComponent<Compartment>();
    }

    void BuildMyelinSheat()
    {
        var myelinSheatPosition = new Vector3((CellBody.transform.localScale.x * 0.5f) + myelinSheatScale.x, 0, 0);
        var previous = CellBody.GetComponent<Compartment>();

        for(int i=0; i<axonLength; i++)
        {
            axon[i] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            axon[i].transform.parent = CellBody.transform;
            axon[i].transform.localRotation = Quaternion.Euler(0,0,90);
            axon[i].transform.localScale = myelinSheatScale;
            axon[i].transform.localPosition = myelinSheatPosition;
            myelinSheatPosition += new Vector3(standardCapsuleLength * myelinSheatScale.y, 0, 0);

            var compartment = axon[i].AddComponent<Compartment>();
            previous.Next.Add(compartment);
            previous = compartment;
        }
    }

    void BuildBifurcation()
    {
        var lastMyelinSheat = axon[axon.Length - 1];
       
        axonTerminals[0] = BuildAxonTerminal(lastMyelinSheat, 45, -1.3f);
        axonTerminals[1] = BuildAxonTerminal(lastMyelinSheat, -45, 1.3f);
    }

    GameObject BuildAxonTerminal(GameObject lastMyelinSheat, float angle, float zPosition)
    {
        var lastMyelinSheatComparment = lastMyelinSheat.GetComponent<Compartment>();

        var axonTerminal = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        axonTerminal.transform.parent = lastMyelinSheat.transform;
        axonTerminal.transform.localRotation = Quaternion.Euler(angle,0,0);
        axonTerminal.transform.localScale = axonTerminalScale;
        axonTerminal.transform.localPosition = new Vector3(0,-2,zPosition);
        var compartment = axonTerminal.AddComponent<Compartment>();
        lastMyelinSheatComparment.Next.Add(compartment);

        return axonTerminal;
    }
}