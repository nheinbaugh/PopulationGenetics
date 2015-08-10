﻿using Ninject.Modules;
using PopulationGenetics.Library;
using PopulationGenetics.Library.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;
using PopulationGenetics.WpfBindings;

namespace PopulationGenetics.Common
{
    public class GeneticsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IControlManager>().To<WpfControlManager>().InSingletonScope();
            Bind<IPopulation>().To<Population>();
            Bind<IWorld>().To<World>();
            Bind<ILocusBank>().To<LocusBank>().InSingletonScope();
            Bind<IPersonFactory>().To<PersonFactory>().InSingletonScope();
            Bind<IAlleleManager>().To<AlleleManager>();
            Bind<IPerson>().To<Person>();
            Bind<IAllele>().To<Allele>();
            Bind<IGene>().To<Gene>();
        }
    }
}
