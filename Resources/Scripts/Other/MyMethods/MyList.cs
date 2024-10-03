using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.Universal;


public class MyList<T>
{

    private List<T> _list;

    public List<T> Values => _list;

    public MyList(List<T> values = null) 
    {
        _list = values == null ? new List<T>() : values;
    }


  

    public void AddElement(T light)
    {
        if (!_list.Contains(light))
        {
            _list.Add(light);
        }
    }

    public void RemoveElement(T light)
    {
        if (_list.Contains(light))
        {
            _list.Remove(light);
        }
    }
}

