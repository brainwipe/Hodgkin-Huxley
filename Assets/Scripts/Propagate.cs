using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagate : MonoBehaviour
{
    NeuronStructure neuronStructure;

    void Awake()
    {
        neuronStructure = GetComponent<NeuronStructure>();
    }

    public void OnPropagate(float electricalPotential)
    {
        // This is a simple form - should really use something like Dijkstra's algorithm here
        var compartments = new Queue<Compartment>();
        compartments.Enqueue(neuronStructure.CellBody.GetComponent<Compartment>());
        
        while(compartments.Count > 0)
        {
            var compartment = compartments.Dequeue();
            electricalPotential = compartment.CalculatePotential(electricalPotential);
            
            foreach(var next in compartment.Next)
            {
                compartments.Enqueue(next);
            }
        }
    }
}
