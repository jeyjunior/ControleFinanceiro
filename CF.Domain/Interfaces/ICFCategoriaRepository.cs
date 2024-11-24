﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.UW.Core.Interfaces;
using CF.Domain.Entities;

namespace CF.Domain.Interfaces
{
    public interface ICFCategoriaRepository : IRepository<CFCategoria>
    {
        IEnumerable<string> ObterCategoriaInicial();
    }
}
