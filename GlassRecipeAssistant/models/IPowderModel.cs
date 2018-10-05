using GlassRecipeAssistant.dao.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.models
{
    public delegate void PowdersUpdateHandler();

    public interface IPowderModel
    {
        void addPowder(Powder powder);

        void renamePowder(int id, string newName);

        List<Powder> findPowders();

        bool contains(string powderName);

        event PowdersUpdateHandler PowdersUpdated;
    }
}
