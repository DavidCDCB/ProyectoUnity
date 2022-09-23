using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public static class GameObjectExtensions
 {
     private static int _stars = 0;
 
     public static int Stars(this GameObject gameObject)
     {
         return _stars;
     }
     public static void AddStar(this GameObject gameObject, int numberToAdd)
     {
         _stars += numberToAdd;
     }
 }