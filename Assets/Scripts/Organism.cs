using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Organism : MonoBehaviour
{
    public Color color = Color.green;
    public float health = 1f;

    public float birthday;
    public float age;
    public float maxAge = 100f;
    public Vector2 maxAgeRange = new Vector2(-0.1f, 0.1f);

    public Vector2 colorRange = new Vector2(-0.1f, 0.1f);
    public float healthLostWhenHungry = 0.1f;
    public float healthGainedWhenFeeding = 0.1f;
    public float maxDistanceToFeed = 1f;
    public float neighborDistance = 1f;
    public int maxNeighbors = 100;
    public float healthToReproduce = 1.25f;
    public LayerMask layerMask;
    public float nextMeal = 0f;

    public float colorHealthMin = 0.5f;
    public float colorHealthScale = 0.5f;

    public int numCells = 1;
    public float maxNumCells = 1f;
    public Vector2 cellGrowthRange = new Vector2(0f, 0.1f);
    public float healthToGrow = 1.2f;

    public int generation = 0;

    public delegate void OnSplit(Organism original, Organism child);
    public delegate void OnDie(Organism organism);
    public static OnSplit onSplit = delegate { };
    public static OnDie onDie = delegate { };

    void Start()
    {
        birthday = Time.time;
        UpdateColor();
    }

    private Color RandomizeColor(Color oldColor)
    {
        Color.RGBToHSV(oldColor, out float h, out float s, out float v);
        h = h + Random.Range(colorRange.x, colorRange.y);
        h = (h + 1f) % 1f; // keep within [0,1)
        return Color.HSVToRGB(h, s, v);
    }

    private void UpdateColor()
    {
        float mult = (health * colorHealthScale) + colorHealthMin;
        Color newColor = color * mult;
        newColor.a = 1f;

        foreach (SpriteRenderer s in GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = newColor;
        }
    }

    void Update()
    {
        if (Time.time > nextMeal)
        {
            if (AnyFoodNearby())
            {
                health += healthGainedWhenFeeding;
            }
            else if (TooManyNeighborsNearby())
            {
                health -= healthLostWhenHungry;
            }
            else
            {
                health += healthGainedWhenFeeding * 0.1f;
            }

            if (health < 0 || (Time.time - birthday) > maxAge)
            {
                Die();
            }
            else if (health > healthToGrow && numCells < Mathf.FloorToInt(maxNumCells))
            {
                Grow();
            }
            else if (health > healthToReproduce)
            {
                Split();
            }

            UpdateColor();
            nextMeal = Time.time + Random.Range(0.9f, 1.1f);
        }

        age = Time.time - birthday;
    }

    private void Grow()
    {
        // Copy an existing cell
        GameObject[] cells = GetComponentsInChildren<SpriteRenderer>().Select(s => s.gameObject).ToArray();
        int index = Random.Range(0, cells.Length);
        GameObject original = cells[index];

        float angle = Random.Range(0f, 360f);
        Vector3 dir = Quaternion.Euler(0, 0, angle) * new Vector2(0, 0.15f);

        GameObject newCell = Instantiate(original, original.transform.parent, false);
        newCell.transform.localPosition = original.transform.localPosition + dir;

        health = 1f;
        numCells++;
    }

    private void Split()
    {
        health = 1f;

        GameObject otherGO = Instantiate(gameObject);
        Organism other = otherGO.GetComponent<Organism>();
        other.generation++;
        other.maxAge += maxAge * Random.Range(maxAgeRange.x, maxAgeRange.y);
        other.maxNumCells += maxNumCells * Random.Range(cellGrowthRange.x, cellGrowthRange.y);
        other.color = RandomizeColor(color);

        onSplit(this, other);
    }

    private void Die()
    {
        Destroy(gameObject);
        onDie(this);
    }

    private bool AnyFoodNearby()
    {
        foreach (var p in FindObjectsOfType<ParticleSystem>())
        {
            Vector3 diff = p.transform.position - transform.position;
            if (diff.magnitude < maxDistanceToFeed)
            {
                return true;
            }
        }

        return false;
    }

    private bool TooManyNeighborsNearby()
    {
        Collider2D[] otherColliders = Physics2D.OverlapCircleAll(transform.position, neighborDistance, layerMask);

        int numNeighbors = otherColliders.Select(o => o.transform.root.gameObject).Distinct(new GameObjectComparer()).Count();

        return numNeighbors > maxNeighbors;
    }

    private class GameObjectComparer : IEqualityComparer<GameObject>
    {
        public bool Equals(GameObject x, GameObject y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(GameObject obj)
        {
            return obj.GetInstanceID();
        }
    }
}
