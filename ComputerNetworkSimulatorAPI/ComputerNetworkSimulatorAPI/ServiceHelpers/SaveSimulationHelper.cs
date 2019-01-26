﻿using ComputerNetworkSimulatorAPI.Models.Database;
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
            context.SaveChanges();
        }

        public void RemoveRouters(SimulationDTO simulationDto)
        {
            var routers = (from router in context.Router
                           where router.IdSim == simulationDto.Id
                           select router).ToList();

            routers.ForEach(x =>
            {
                if (simulationDto.Routers.Where(y => y.NodeNumber == x.NodeNumber).FirstOrDefault() == null)
                {
                    var interfaces = (from _interface in context.RouterInterface
                                      where _interface.IdRouter == x.Id
                                      select _interface).ToList();

                    RemoveRoutersInterface(interfaces);
                    context.Remove(x);
                }
                //else
                //{
                //    x.RouterInterface
                //    .ToList()
                //    .ForEach(y =>
                //    {
                //        RouterDTO routerDto = simulationDto.Routers
                //        .Where(z => z.NodeNumber == x.NodeNumber)
                //        .FirstOrDefault();

                //        if (routerDto != null && routerDto.Interfaces.Where(c => c.Id == y.Id).FirstOrDefault() == null)
                //            context.Remove(y);
                //    });
                //}
            });
            context.SaveChanges();
        }

        private void RemoveRoutersInterface(List<RouterInterface> routerInterfaces)
        {
            routerInterfaces.ForEach(x => context.Remove(x));
            context.SaveChanges();
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
            context.SaveChanges();

        }

        public void RemoveSwitches(SimulationDTO simulationDto)
        {
            var switches = (from sw in context.Switch
                            where sw.IdSim == simulationDto.Id
                            select sw).ToList();

            switches.ForEach(x =>
            {
                if (simulationDto.Switches.Where(y => y.NodeNumber == x.NodeNumber).FirstOrDefault() == null)
                {
                    var interfaces = (from router in context.Router
                                      join @interface in context.RouterInterface on router.Id equals @interface.IdRouter
                                      where router.IdSim == simulationDto.Id
                                      && @interface.ConnectedNodeNumber == x.NodeNumber
                                      select @interface).ToList();

                    RemoveRoutersInterface(interfaces);

                    context.Remove(x);
                }
            });
            context.SaveChanges();

        }

        public void AddOrUpdateSimulation(SimulationDTO simulationDto, Simulation simulation)
        {
            AddOrUpdatePcs(simulationDto, simulation);
            AddOrUpdateRouter(simulationDto.Routers, simulation);
            AddOrUpdateSwitch(simulationDto.Switches, simulation);
            AddConnections(simulationDto.Links, simulation);
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
            context.SaveChanges();
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
            context.SaveChanges();
        }
    }
}
