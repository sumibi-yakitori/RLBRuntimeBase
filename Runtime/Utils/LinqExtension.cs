using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace RetroLevel {
  public static class LinqExtension {
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, System.Action<T> action) {
      foreach(T item in source) action(item);
      return source;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, System.Action<T, int> action) {
      int i = 0;
      foreach(T item in source) action(item, i++);
      return source;
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
      return source.OrderBy(_ => System.Guid.NewGuid());
    }

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> source) {
      while (true) {
        foreach (var item in source) {
          yield return item;
        }
      }
    }
    
    public static T RandomElementAt<T>(this IEnumerable<T> ie) {
      if (ie.Any() == false) return default(T);
      return ie.ElementAt(Random.Range(0, ie.Count()));
    }	
  }
}
