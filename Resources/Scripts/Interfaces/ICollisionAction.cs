using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ICollisionAction
{
     void CollisionWithPlayer(GameObject gameObject);
     void CollisionWithEnemy(GameObject gameObject);
}

