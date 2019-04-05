using System;
using System.Collections.Generic;

namespace BulletHell
{
    public interface Parser
    {
        Dictionary<string, GameEngine.Enemy> getEnemyPrefabs();
        Wave[] getWaves();
        void Parse();

    }
}
