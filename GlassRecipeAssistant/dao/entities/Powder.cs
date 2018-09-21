﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao.entities
{
    public class Powder
    {
        public int Id { get; } // only generated by database
        public String PowderName { get; set; }

        public Powder(String powderName)
        {
            Id = -1;
            PowderName = powderName;
        }

        public Powder(int id, String powderName)
        {
            Id = id;
            PowderName = powderName;
        }

        public override string ToString()
        {
            return "Powder = " + PowderName;
        }
    }
}