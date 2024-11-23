﻿using agilium.api.business.Interfaces.IRepository;
using agilium.api.business.Interfaces;
using agilium.api.business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using agilium.api.infra.Context;

namespace agilium.api.infra.Repository
{
    public class PerdaRepository : Repository<Perda>, IPerdaRepository
    {
        public PerdaRepository(AgiliumContext db) : base(db)
        {
        }
    }
}