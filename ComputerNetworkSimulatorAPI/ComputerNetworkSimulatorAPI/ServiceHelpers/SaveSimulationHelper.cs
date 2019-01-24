using ComputerNetworkSimulatorAPI.Models.Database;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using System.Collections.Generic;
using System.Linq;

namespace ComputerNetworkSimulatorAPI.ServiceHelpers
{
    public class SaveSimulationHelper
    {
        private ComputerNetworkSimulatorContext context;

        public SaveSimulationHelper(ComputerNetworkSimulatorContext context)
        {
            this.context = context;
        }

        public void RemoveOldData(SimulationDTO simulationDTO)
        {
            RemoveConnections(simulationDTO);
            RemoveRouters(simulationDTO);
            RemovePcs(simulationDTO);
            RemoveSwitches(simulationDTO);
        }


        public void RemoveConnections(SimulationDTO simulationDto)
        {
            var links = (from link in context.Links
                         where link.IdSim == simulationDto.Id
                         select link).ToList();

            if (simulationDto.Links != null)
                links.ForEach(x =>
                {
                    LinkDTO linkDto1 = simulationDto.Links
                    .Where(y => y.NodeNumber1 == x.NodeNumber1 && y.NodeNumber2 == x.NodeNumber2)
                    .FirstOrDefault();

                    LinkDTO linkDto2 = simulationDto.Links
                    .Where(y => y.NodeNumber1 == x.NodeNumber2 && y.NodeNumber2 == x.NodeNumber1)
                    .FirstOrDefault();

                    if (linkDto1 == null && linkDto2 == null)
                        context.Remove(x);
                });

            //var pcSwitches = (from pcSwitch in context.PcSwitch
            //                  join pc in context.Pc on pcSwitch.IdPc equals pc.Id
            //                  where pc.IdSim == simulationDto.Id
            //                  select pcSwitch).ToList();

            //foreach (PcSwitch pcSwitch in pcSwitches)
            //{
            //    PcSwitchDTO pcSwitchDTO = simulationDto.PcSwitches
            //        .Where(x => x.Pc.NodeNumber == pcSwitch.IdPcNavigation.NodeNumber && x.Switch.NodeNumber == pcSwitch.IdSwitchNavigation.NodeNumber)
            //        .FirstOrDefault();

            //    if (pcSwitchDTO == null)
            //        context.Remove(pcSwitch);
            //}


            //var routerSwitches = (from routerSwitch in context.RouterSwitch
            //                      join router in context.Router on routerSwitch.IdRouter equals router.Id
            //                      where router.IdSim == simulationDto.Id
            //                      select routerSwitch).ToList();

            //foreach (RouterSwitch routerSwitch in routerSwitches)
            //{
            //    RouterSwitchDTO routerSwitchDTO = simulationDto.RouterSwitches
            //        .Where(x => x.Router.NodeNumber == routerSwitch.IdRouterNavigation.NodeNumber && x.Switch.NodeNumber == routerSwitch.IdSwitchNavigation.NodeNumber)
            //        .FirstOrDefault();

            //    if (routerSwitchDTO == null)
            //        context.Remove(routerSwitch);
            //}
        }

        public void RemoveRouters(SimulationDTO simulationDto)
        {
            //var routerInterfaces = (from interfaces in context.RouterInterface
            //                        join router in context.Router on interfaces.IdRouter equals router.Id
            //                        where router.IdSim == simulationDto.Id
            //                        select interfaces).ToList();



            var routers = (from router in context.Router
                           where router.IdSim == simulationDto.Id
                           select router).ToList();

            routers.ForEach(x =>
            {
                if (simulationDto.Routers.Where(y => y.NodeNumber == x.NodeNumber).FirstOrDefault() == null)
                {
                    x.RouterInterface
                    .ToList()
                    .ForEach(y => context.Remove(y));
                    context.Remove(x);
                }
                else
                {
                    x.RouterInterface
                    .ToList()
                    .ForEach(y =>
                    {
                        RouterDTO routerDto = simulationDto.Routers
                        .Where(z => z.NodeNumber == x.NodeNumber)
                        .FirstOrDefault();

                        if (routerDto != null && routerDto.Interfaces.Where(c => c.Id == y.Id).FirstOrDefault() == null)
                            context.Remove(y);
                    });
                }
            });
        }

        public void RemovePcs(SimulationDTO simulationDto)
        {
            var pcs = (from pc in context.Pc
                       where pc.IdSim == simulationDto.Id
                       select pc).ToList();

            pcs.ForEach(x =>
            {
                if (simulationDto.Pcs.Where(y => y.NodeNumber == x.NodeNumber).FirstOrDefault() == null)
                    context.Remove(x);
            });
        }

        public void RemoveSwitches(SimulationDTO simulationDto)
        {
            var switches = (from sw in context.Switch
                            where sw.IdSim == simulationDto.Id
                            select sw).ToList();

            switches.ForEach(x =>
            {
                if (simulationDto.Switches.Where(y => y.NodeNumber == x.NodeNumber).FirstOrDefault() == null)
                    context.Remove(x);
            });
        }

        public void AddOrUpdateSimulation(SimulationDTO simulationDto, Simulation simulation)
        {
            AddOrUpdatePcs(simulationDto, simulation);
            AddOrUpdateRouter(simulationDto.Routers, simulation);
            AddOrUpdateSwitch(simulationDto.Switches, simulation);
            AddConnections(simulationDto.Links, simulation);
            //AddPcSwitchConnection(simulationDto.PcSwitches, simulation);
            //AddRouterSwitchConnection(simulationDto.RouterSwitches, simulation);
        }


        public void AddOrUpdatePcs(SimulationDTO simulationDto, Simulation simulation)
        {
            foreach (PcDTO pcDto in simulationDto.Pcs)
            {
                Pc pc = context.Pc.Where(x => x.Id == pcDto.Id).FirstOrDefault();
                if (pc == null)
                    pc = new Pc();
                pc.Id = pcDto.Id;
                pc.IdSimNavigation = simulation;
                pc.Ip = pcDto.Ip;
                pc.Mask = pcDto.Mask;
                pc.Name = pcDto.Name;
                pc.NodeNumber = pcDto.NodeNumber;
                pc.PcNumber = pcDto.PcNumber;
                pc.Gateway = pcDto.Gateway;
                if (pcDto.Id == 0)
                    context.Add(pc);
                //else
                //    context.Update(pc);
            }
            context.SaveChanges();
        }

        public void AddOrUpdateRouter(List<RouterDTO> routersDto, Simulation simulation)
        {
            foreach (RouterDTO routerDto in routersDto)
            {
                Router router = context.Router.Where(x => x.Id == routerDto.Id).FirstOrDefault();
                if (router == null)
                    router = new Router();
                router.Id = routerDto.Id;
                router.IdSimNavigation = simulation;
                router.Name = routerDto.Name;
                router.RouterNumber = routerDto.RouterNumber;
                router.NodeNumber = routerDto.NodeNumber;
                if (routerDto.Id == 0)
                    context.Add(router);
                context.SaveChanges();
                AddOrUpdateRouterInterafces(routerDto.Interfaces, router);
            }
            context.SaveChanges();

        }

        public void AddOrUpdateRouterInterafces(List<RouterInterfaceDTO> interfcesDto, Router router)
        {
            foreach (RouterInterfaceDTO interfaceDto in interfcesDto)
            {
                RouterInterface routerInterface = context.RouterInterface.Where(x => x.Id == interfaceDto.Id).FirstOrDefault();
                if (routerInterface == null)
                    routerInterface = new RouterInterface();
                routerInterface.Id = interfaceDto.Id;
                routerInterface.IdRouterNavigation = router;
                routerInterface.IpHost = interfaceDto.IpHost;
                routerInterface.IpNet = interfaceDto.IpNet;
                routerInterface.Mask = interfaceDto.Mask;
                routerInterface.Name = interfaceDto.Name;
                routerInterface.ConnectedNodeNumber = interfaceDto.connectedNodeNumber;
                if (interfaceDto.Id == 0)
                    context.Add(routerInterface);
            }

        }

        public void AddOrUpdateSwitch(List<SwitchDTO> switchesDto, Simulation simulation)
        {
            foreach (SwitchDTO switchDto in switchesDto)
            {
                Switch @switch = context.Switch.Where(x => x.Id == switchDto.Id).FirstOrDefault();
                if (@switch == null)
                    @switch = new Switch();
                @switch.Id = switchDto.Id;
                @switch.IdSimNavigation = simulation;
                @switch.Name = switchDto.Name;
                @switch.NodeNumber = switchDto.NodeNumber;
                @switch.SwitchNumber = switchDto.SwitchNumber;
                if (switchDto.Id == 0)
                    context.Add(@switch);
            }

            context.SaveChanges();
        }

        private void AddConnections(List<LinkDTO> linksDto, Simulation simulation)
        {
            linksDto.ForEach(x =>
            {
                Links link1 = simulation.Links
                .Where(y => y.NodeNumber1 == x.NodeNumber1 && y.NodeNumber2 == x.NodeNumber2)
                .FirstOrDefault();

                Links link2 = simulation.Links
                .Where(y => y.NodeNumber1 == x.NodeNumber2 && y.NodeNumber2 == x.NodeNumber1)
                .FirstOrDefault();

                if (link1 == null && link2 == null)
                    context.Add(new Links()
                    {
                        IdSimNavigation = simulation,
                        NodeNumber1 = x.NodeNumber1,
                        NodeNumber2 = x.NodeNumber2
                    });
            });
        }

        //public void AddPcSwitchConnection(List<PcSwitchDTO> pcSwitches, Simulation simulation)
        //{
        //    foreach (PcSwitchDTO pcSwitchDto in pcSwitches)
        //    {
        //        Pc pc = context.Pc
        //            .Where(x => x.NodeNumber == pcSwitchDto.Pc.NodeNumber && x.IdSimNavigation == simulation)
        //            .FirstOrDefault();

        //        Switch @switch = context.Switch
        //            .Where(x => x.NodeNumber == pcSwitchDto.Switch.NodeNumber && x.IdSimNavigation == simulation)
        //            .FirstOrDefault();

        //        PcSwitch pcSwitch = context.PcSwitch
        //            .Where(x => x.IdPcNavigation == pc && x.IdSwitchNavigation == x.IdSwitchNavigation)
        //            .FirstOrDefault();

        //        if (pcSwitch == null)
        //        {
        //            pcSwitch = new PcSwitch();
        //            pcSwitch.IdPcNavigation = pc;
        //            pcSwitch.IdSwitchNavigation = @switch;
        //            context.Add(pcSwitch);
        //        }
        //    }
        //}

        //public void AddRouterSwitchConnection(List<RouterSwitchDTO> routerSwitchesDto, Simulation simulation)
        //{
        //    foreach (RouterSwitchDTO routerSwitchDto in routerSwitchesDto)
        //    {
        //        Router router = context.Router
        //            .Where(x => x.NodeNumber == routerSwitchDto.Router.NodeNumber && x.IdSimNavigation == simulation)
        //            .FirstOrDefault();

        //        Switch @switch = context.Switch
        //            .Where(x => x.NodeNumber == routerSwitchDto.Switch.NodeNumber && x.IdSimNavigation == simulation)
        //            .FirstOrDefault();

        //        RouterSwitch routerSwitch = context.RouterSwitch
        //            .Where(x => x.IdRouterNavigation == router && x.IdSwitchNavigation == x.IdSwitchNavigation)
        //            .FirstOrDefault();

        //        if (routerSwitch == null)
        //        {
        //            routerSwitch = new RouterSwitch();
        //            routerSwitch.IdRouterNavigation = router;
        //            routerSwitch.IdSwitchNavigation = @switch;
        //            context.Add(routerSwitch);
        //        }
        //    }
        //}
    }
}
