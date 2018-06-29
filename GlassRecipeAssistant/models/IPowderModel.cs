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
        void addPowder(string powderName);

        void renamePowder(int index, string newName);

        List<string> findPowders();

        int getPowderId(int selectedIndex);

        string getPowderName(int powderId);

        event PowdersUpdateHandler PowdersUpdated;
    }
}
