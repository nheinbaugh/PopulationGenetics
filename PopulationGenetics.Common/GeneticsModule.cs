using Ninject.Modules;
using PopulationGenetics.Library;
using PopulationGenetics.Library.Factories;
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
            Bind<IControlManager>().To<WpfControlManager>().InSingletonScope();
            Bind<IWorld>().To<World>();
            Bind<ILocusBank>().To<LocusBank>().InSingletonScope();
            Bind<IPersonFactory>().To<PersonFactory>().InSingletonScope();
            Bind<ILocusManager>().To<LocusManager>();
            Bind<IPerson>().To<Person>();
            Bind<IAllele>().To<Allele>();
            Bind<IGene>().To<Gene>();
        }
    }
}
