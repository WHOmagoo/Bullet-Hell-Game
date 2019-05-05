using System;
using System.Collections.Generic;

namespace BulletHell
{
    public interface Parser
    {
        List<Encounter>  getEncounterList();
        void Parse();

    }
}
