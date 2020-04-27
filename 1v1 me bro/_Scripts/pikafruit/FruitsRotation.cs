using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsRotation : MonoBehaviour
{
    public Sprite[] fruitsSprites;

    public float distance = 3f;
    [Range(1f, 20f)] public float moveSpeed;
    [Range(0f, 2f)] public float baseSpeed;
    [Range(1f, 20f)] public float disappearSpeed;

    private GameObject[] fruitArray;
    

    private void Start()
    {
        fruitArray = new GameObject[transform.childCount];
        shuffle(fruitsSprites);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<SpriteRenderer>().sprite = fruitsSprites[i];
            fruitArray[i] = child;
        }

        foreach (GameObject fruit in fruitArray)
        {
            fruit.AddComponent<FruitBehavior>();
        }
    }

    // Knuth shuffle algorithm
    private void shuffle(UnityEngine.Object[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            UnityEngine.Object tmp = array[i];
            int r = UnityEngine.Random.Range(i, array.Length);
            array[i] = array[r];
            array[r] = tmp;
        }
    }

    public void MakeAllFruitsMove()
    {
        foreach (GameObject fruit in fruitArray)
        {
            StartCoroutine(MakeOneFruitMove(fruit));
        }
    }

    private IEnumerator MakeOneFruitMove(GameObject fruit)
    {
        Vector3 goal = fruit.transform.position;
        goal.y = goal.y - distance;
        while (fruit.transform.position != goal)
        {
            fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, goal,
                                                            (Math.Abs((goal - fruit.transform.position).sqrMagnitude) * moveSpeed + baseSpeed) * Time.deltaTime);
            yield return null;
        }
        fruit.GetComponent<FruitBehavior>().CheckPosition();
        StartCoroutine(MakeOneFruitMove(fruit));
    }

    private IEnumerator MakeOneFruitDisappear(GameObject fruit)
    {
        Vector3 goal = new Vector3(0, 0, fruit.transform.localScale.z);
        while (fruit.transform.localScale != goal)
        {
            Vector3 newScale = Vector3.MoveTowards(fruit.transform.localScale, goal, disappearSpeed * Time.deltaTime);
            fruit.transform.localScale = newScale;
            yield return null;
        }
    }

    public void End()
    {
        StopAllCoroutines();
        FruitBehavior.ending = true;
        foreach (GameObject fruit in fruitArray)
        {
            StartCoroutine(MakeOneFruitDisappear(fruit));
        }
    }

}
