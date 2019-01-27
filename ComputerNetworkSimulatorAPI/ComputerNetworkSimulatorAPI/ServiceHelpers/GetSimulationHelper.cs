using ComputerNetworkSimulatorAPI.Models.Database;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.ServiceHelpers
{
    public class GetSimulationHelper
    {
        private ComputerNetworkSimulatorContext context;

        public GetSimulationHelper(ComputerNetworkSimulatorContext context)
        {
            this.context = context;
        }

        public List<PcDTO> GetPcs(int simId)
        {
            return (from pc in context.Pc
                    where pc.IdSim == simId
                    select new PcDTO()
                    {
                        Id = pc.Id,
                        HostIdentity = pc.HostIdentity,
                        Name = pc.Name,
                        NodeNumber = pc.NodeNumber,
                        PcNumber = pc.PcNumber
                    }).ToList();
        }

        public List<SwitchDTO> GetSwitches(int simId)
        {
            return (from _switch in context.Switch
                    where _switch.IdSim == simId
                    select new SwitchDTO()
                    {
                        Id = _switch.Id,
                        Name = _switch.Name,
                        NodeNumber = _switch.NodeNumber,
                        SwitchNumber = _switch.SwitchNumber,
                        HostIdentity = _switch.HostIdentity
                    }).ToList();
        }

        public List<RouterDTO> GetRouters(int simId)
        {
            var routers = (from router in context.Router
                           where router.IdSim == simId
                           select new RouterDTO()
                           {
                               Id = router.Id,
                               Name = router.Name,
                               NodeNumber = router.NodeNumber,
                               RouterNumber = router.RouterNumber,
                               HostIdentity = router.HostIdentity
                           }).ToList();


            return routers;
        }



        public List<LinkDTO> GetLinks(int simId)
        {
            var links = (from link in context.Links
                         where link.IdSim == simId
                         select new LinkDTO()
                         {
                             Id = link.Id,
                             IdSim = simId,
                             NodeNumber1 = link.NodeNumber1,
                             NodeNumber2 = link.NodeNumber2
                         }).ToList();
            return links;
        }
    }
}
