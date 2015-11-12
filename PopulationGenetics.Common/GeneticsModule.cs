using Ninject.Modules;
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
            Bind<IMainWindow>().To<IMainWindow>();
            Bind<ILocusComboBox>().To<LocusComboBox>();
            Bind<IAlleleSelectionChangedEventArgs>().To<AlleleSelectionChangedEventArgs>();
            Bind<IRandomGenerator>().To<TrulyRandomGenerator>().InSingletonScope();
            Bind<IControlManager>().To<WpfControlManager>().InSingletonScope();
            Bind<IPopulation>().To<Population>().InSingletonScope();
            Bind<IWorld>().To<World>().InSingletonScope();
            Bind<IGeneBank>().To<GeneBank>().InSingletonScope();
            Bind<IMortalityCurve>().To<MortalityCurve>().InSingletonScope();
            Bind<IPersonFactory>().To<PersonFactory>().InSingletonScope();
            Bind<IAlleleManager>().To<AlleleManager>();
            Bind<IPerson>().To<Person>();
            Bind<IAllele>().To<Allele>();
            Bind<IGene>().To<Gene>();
        }
    }
}
