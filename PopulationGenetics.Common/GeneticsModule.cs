﻿using Ninject.Modules;
using PopulationGenetics.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationGenetics.Common
{
    public class GeneticsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>();
            Bind<IGeneBank>().To<GeneBank>().InSingletonScope();
            Bind<ILocusManager>().To<LocusManager>();
            Bind<IPerson>().To<Person>();
            Bind<IAllele>().To<Allele>();
            Bind<IGene>().To<Gene>();
        }
    }
}
