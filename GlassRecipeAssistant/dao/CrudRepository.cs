using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao
{
    public interface CrudRepository<T>
    {
        void save(T entity);

        void saveAll(List<T> entities);

        Optional<T> findById(int id);

        bool existsById(int id);

        List<T> findAll();

        int count();

        void deleteById(int id);

        void delete(T entity);

        void deleteAll();
    }
}
