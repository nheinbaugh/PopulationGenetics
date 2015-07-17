using Ninject.Modules;
using PopulationGenetics.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationGenetics.Common
{
    public class NinjectKernel : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorld>().To<World>();
        }
    }
}
