using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao
{
    public interface IPowderRepository<T> : CrudRepository<T>
    {
        Optional<T> findByPowderName(String powderName);

        bool existsByPowderName(string name);
    }
}
