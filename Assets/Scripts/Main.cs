using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject prefab;
    public Camera theCamera;

    public int nextId = 0;
    public int count = 0;
    public List<int> maxNumCellsCounter = new List<int>();
    public int maxGeneration = 0;

    void OnEnable()
    {
        Organism.onSplit += OnOrganismSplit;
        Organism.onDie += OnOrganismDie;
    }

    void OnDisable()
    {
        Organism.onSplit -= OnOrganismSplit;
        Organism.onDie -= OnOrganismDie;
    }

    void Update()
    {
        if (InputHelper.Spawn)
        {
            Vector3 pos = theCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Spawn(pos);
        }
    }

    public void Spawn(Vector3 pos)
    {
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        go.name = nextId.ToString();
        Organism organism = go.GetComponent<Organism>();

        count++;
        nextId++;

        int maxNumCells = Mathf.FloorToInt(organism.maxNumCells);
        if (maxNumCellsCounter.Count < maxNumCells)
            maxNumCellsCounter.Add(0);
        maxNumCellsCounter[maxNumCells - 1] += 1;

        maxGeneration = Mathf.Max(maxGeneration, organism.generation);
    }

    private void OnOrganismSplit(Organism original, Organism child)
    {
        child.name = child.name.Replace("(Clone)", "") + "_" + nextId.ToString();

        count++;
        nextId++;

        int maxNumCells = Mathf.FloorToInt(child.maxNumCells);
        if (maxNumCellsCounter.Count < maxNumCells)
            maxNumCellsCounter.Add(0);
        maxNumCellsCounter[maxNumCells - 1] += 1;

        maxGeneration = Mathf.Max(maxGeneration, child.generation);
    }

    private void OnOrganismDie(Organism organism)
    {
        count--;

        int maxNumCells = Mathf.FloorToInt(organism.maxNumCells);
        maxNumCellsCounter[maxNumCells - 1] -= 1;
    }
}
