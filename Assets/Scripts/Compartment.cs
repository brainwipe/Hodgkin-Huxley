using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compartment : MonoBehaviour
{
    public List<Compartment> Next = new List<Compartment>();
    public NeuronStructure neuronStructure;
    public float ElectricalPotential = 0.4f;
    Material material;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        neuronStructure = GetComponentInParent<NeuronStructure>();
    }

    void Update()
    {
        material.color = neuronStructure.Gradient.Evaluate(ElectricalPotential);
    }

    public float CalculatePotential(float electricalPotentialOfPrevious)
    {
        // TODO the HH algorithm here
        ElectricalPotential = electricalPotentialOfPrevious * (1 - neuronStructure.GradientFalloff);
        return ElectricalPotential;
    }
}
