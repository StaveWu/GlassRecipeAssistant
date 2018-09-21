using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao
{
    public class Optional<T>
    {
        private T value;

        public Optional(T value)
        {
            this.value = value;
        }

        public bool isPresent()
        {
            return value != null;
        }

        public T get()
        {
            if (value == null)
            {
                throw new InvalidOperationException("No such element");
            }
            return value;
        }

        public void ifPresent(Action action)
        {
            action();
        }

        
    }
}
