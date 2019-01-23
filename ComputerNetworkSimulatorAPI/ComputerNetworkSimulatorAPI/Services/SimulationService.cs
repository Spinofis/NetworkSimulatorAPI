using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.Database;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ComputerNetworkSimulatorContext context;

        public SimulationService(ComputerNetworkSimulatorContext context)
        {
            this.context = context;
        }

        public SimulationDTO SaveSimulation(SimulationDTO simulationDTO)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    Simulation simulation = context.Simulation.Where(x => x.Id == simulationDTO.Id).FirstOrDefault();
                    if (simulation != null)
                    {
                        simulation.DateEdit = DateTime.Now;
                        RemoveOldData(simulationDTO);
                    }
                    else
                    {
                        simulation = new Simulation();
                        simulation.DateAdd = DateTime.Now;
                        simulation.DateEdit = DateTime.Now;
                        context.SaveChanges();
                    }

                    AddOrUpdateSimulation(simulationDTO, simulation);

                    context.SaveChanges();
                    tran.Commit();
                    return null;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void RemoveOldData(SimulationDTO simulationDTO)
        {
            RemoveConnections(simulationDTO);
            RemoveRouters(simulationDTO);
            RemovePcs(simulationDTO);
            RemoveSwitches(simulationDTO);
        }


        private void RemoveConnections(SimulationDTO simulationDto)
        {
            var pcSwitches = (from pcSwitch in context.PcSwitch
                              join pc in context.Pc on pcSwitch.IdPc equals pc.Id
                              where pc.IdSim == simulationDto.Id
                              select pcSwitch).ToList();

            foreach (PcSwitch pcSwitch in pcSwitches)
            {
                PcSwitchDTO pcSwitchDTO = simulationDto.PcSwitches
                    .Where(x => x.Pc.NodeNumber == pcSwitch.IdPcNavigation.NodeNumber && x.Switch.NodeNumber == pcSwitch.IdSwitchNavigation.NodeNumber)
                    .FirstOrDefault();

                if (pcSwitchDTO == null)
                    context.Remove(pcSwitch);
            }


            var routerSwitches = (from routerSwitch in context.RouterSwitch
                                  join router in context.Router on routerSwitch.IdRouter equals router.Id
                                  where router.IdSim == simulationDto.Id
                                  select routerSwitch).ToList();

            foreach (RouterSwitch routerSwitch in routerSwitches)
            {
                RouterSwitchDTO routerSwitchDTO = simulationDto.RouterSwitches
                    .Where(x => x.Router.NodeNumber == routerSwitch.IdRouterNavigation.NodeNumber && x.Switch.NodeNumber == routerSwitch.IdSwitchNavigation.NodeNumber)
                    .FirstOrDefault();

                if (routerSwitchDTO == null)
                    context.Remove(routerSwitch);
            }
        }

        private void RemoveRouters(SimulationDTO simulationDto)
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

        private void RemovePcs(SimulationDTO simulationDto)
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

        private void RemoveSwitches(SimulationDTO simulationDto)
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

        private void AddOrUpdateSimulation(SimulationDTO simulationDto, Simulation simulation)
        {
            AddOrUpdatePcs(simulationDto, simulation);
            AddOrUpdateRouter(simulationDto.Routers, simulation);
            AddOrUpdateSwitch(simulationDto.Switches, simulation);
            AddPcSwitchConnection(simulationDto.PcSwitches, simulation);
            AddRouterSwitchConnection(simulationDto.RouterSwitches, simulation);
        }


        private void AddOrUpdatePcs(SimulationDTO simulationDto, Simulation simulation)
        {
            foreach (PcDTO pcDto in simulationDto.Pcs)
            {
                Pc pc = new Pc();
                pc.Id = pcDto.Id;
                pc.IdSimNavigation = simulation;
                pc.Ip = pcDto.Ip;
                pc.Mask = pcDto.Mask;
                pc.Name = pcDto.Name;
                pc.NodeNumber = pcDto.NodeNumber;
                pc.PcNumber = pcDto.PcNumber;
                pc.Gateway = pcDto.Gateway;
                if (pcDto.Id > 0)
                    context.Update(pc);
                else
                    context.Update(pc);
            }
            context.SaveChanges();
        }

        private void AddOrUpdateRouter(List<RouterDTO> routersDto, Simulation simulation)
        {
            foreach (RouterDTO routerDto in routersDto)
            {
                Router router = new Router();
                router.Id = routerDto.Id;
                router.IdSimNavigation = simulation;
                router.Name = routerDto.Name;
                router.RouterNumber = routerDto.RouterNumber;
                router.NodeNumber = routerDto.NodeNumber;
                if (router.Id > 0)
                    context.Update(router);
                else
                    context.Add(router);
                context.SaveChanges();
                AddOrUpdateRouterInterafces(routerDto.Interfaces, router);
            }
            context.SaveChanges();

        }

        private void AddOrUpdateRouterInterafces(List<RouterInterfaceDTO> interfcesDto, Router router)
        {
            foreach (RouterInterfaceDTO interfaceDto in interfcesDto)
            {
                RouterInterface routerInterface = new RouterInterface();
                routerInterface.Id = interfaceDto.Id;
                routerInterface.IdRouterNavigation = router;
                routerInterface.IpHost = interfaceDto.IpHost;
                routerInterface.IpNet = interfaceDto.IpNet;
                routerInterface.Mask = interfaceDto.Mask;
                routerInterface.Name = interfaceDto.Name;
                if (routerInterface.Id > 0)
                    context.Update(routerInterface);
                else
                    context.Add(routerInterface);
            }

        }

        private void AddOrUpdateSwitch(List<SwitchDTO> switchesDto, Simulation simulation)
        {
            foreach (SwitchDTO switchDto in switchesDto)
            {
                Switch @switch = new Switch();
                @switch.Id = switchDto.Id;
                @switch.IdSimNavigation = simulation;
                @switch.Name = switchDto.Name;
                @switch.NodeNumber = switchDto.NodeNumber;
                @switch.SwitchNumber = switchDto.SwitchNumber;
                if (@switch.Id > 0)
                    context.Update(@switch);
                else
                    context.Add(@switch);
            }

            context.SaveChanges();
        }

        private void AddPcSwitchConnection(List<PcSwitchDTO> pcSwitches, Simulation simulation)
        {
            foreach (PcSwitchDTO pcSwitchDto in pcSwitches)
            {
                Pc pc = context.Pc
                    .Where(x => x.NodeNumber == pcSwitchDto.Pc.NodeNumber && x.IdSimNavigation == simulation)
                    .FirstOrDefault();

                Switch @switch = context.Switch
                    .Where(x => x.NodeNumber == pcSwitchDto.Switch.NodeNumber && x.IdSimNavigation == simulation)
                    .FirstOrDefault();

                PcSwitch pcSwitch = context.PcSwitch
                    .Where(x => x.IdPcNavigation == pc && x.IdSwitchNavigation == x.IdSwitchNavigation)
                    .FirstOrDefault();

                if (pcSwitch == null)
                {
                    pcSwitch = new PcSwitch();
                    pcSwitch.IdPcNavigation = pc;
                    pcSwitch.IdSwitchNavigation = @switch;
                    context.Add(pcSwitch);
                }
            }
        }

        private void AddRouterSwitchConnection(List<RouterSwitchDTO> routerSwitchesDto, Simulation simulation)
        {
            foreach (RouterSwitchDTO routerSwitchDto in routerSwitchesDto)
            {
                Router router = context.Router
                    .Where(x => x.NodeNumber == routerSwitchDto.Router.NodeNumber && x.IdSimNavigation == simulation)
                    .FirstOrDefault();

                Switch @switch = context.Switch
                    .Where(x => x.NodeNumber == routerSwitchDto.Switch.NodeNumber && x.IdSimNavigation == simulation)
                    .FirstOrDefault();

                RouterSwitch routerSwitch = context.RouterSwitch
                    .Where(x => x.IdRouterNavigation == router && x.IdSwitchNavigation == x.IdSwitchNavigation)
                    .FirstOrDefault();

                if (routerSwitch == null)
                {
                    routerSwitch = new RouterSwitch();
                    routerSwitch.IdRouterNavigation = router;
                    routerSwitch.IdSwitchNavigation = @switch;
                    context.Add(routerSwitch);
                }
            }
        }
    }
}
