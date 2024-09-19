using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IBonfireAction
{
    void PlayerBonFireAction(BonFire bonfire);
    void EnemyBonFireAction(BonFire bonfire);
}

