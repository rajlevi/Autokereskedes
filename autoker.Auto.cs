﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025. 05. 04. 21:20:48
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Autoker
{
    public partial class Auto {

        public Auto()
        {
            OnCreated();
        }

        public short AutoId { get; set; }

        public string Marka { get; set; }

        public string Kivitel { get; set; }

        public string Evjarat { get; set; }

        public string Uzemanyag { get; set; }

        public string Szin { get; set; }

        public short KereskedesId { get; set; }

        public virtual Kereskedes Kereskedes1 { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
