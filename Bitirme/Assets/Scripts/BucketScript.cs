using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket<T>
{
    private List<T> list;
    private List<T> init;
    
    public Bucket(List<T> init)
    {
        this.init = init;
        list = new List<T>(init);
    }

    public List<T> Add(T variable)
    {
        list.Add(variable);
        return list;
    }

    public T Pick()
    {
        if (list.Count == 0)
        {
            list = new List<T>(this.init);
        }
        int rand = Random.Range(0, list.Count);
        T value = list[rand];
        list.RemoveAt(rand);
        return value;
    }

    public List<T> Clear()
    {
        List<T> temp = new List<T>(list);
        list.Clear();
        return temp;
    }
}
