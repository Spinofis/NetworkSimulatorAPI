using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.Database;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using ComputerNetworkSimulatorAPI.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ComputerNetworkSimulatorContext context;
        private SaveSimulationHelper saveSimulationHelper;
        private GetSimulationHelper getSimulationHelper;

        public SimulationService(ComputerNetworkSimulatorContext context)
        {
            this.context = context;
            saveSimulationHelper = new SaveSimulationHelper(context);
            getSimulationHelper = new GetSimulationHelper(context);
        }

        #region MainMethods

        public List<SimulationDTO> GetSimulationList()
        {
            return (from sim in context.Simulation
                    select new SimulationDTO()
                    {
                        Id = sim.Id,
                        DateAdd = sim.DateAdd,
                        DateEdit = sim.DateEdit
                    }).ToList();
        }

        public SimulationDTO GetSimulation(int simId)
        {
            SimulationDTO simulationDTO = new SimulationDTO();
            Simulation simulation = context.Simulation.Where(x => x.Id == simId).FirstOrDefault();
            if (simulation == null)
                return null;
            simulationDTO.Id = simulation.Id;
            simulationDTO.DateAdd = simulation.DateAdd;
            simulationDTO.DateEdit = simulation.DateEdit;
            simulationDTO.Pcs = getSimulationHelper.GetPcs(simulationDTO.Id);
            simulationDTO.Routers = getSimulationHelper.GetRouters(simulationDTO.Id);
            simulationDTO.Switches = getSimulationHelper.GetSwitches(simulationDTO.Id);
            simulationDTO.Links = getSimulationHelper.GetLinks(simulationDTO.Id);
            //simulationDTO.PcSwitches = getSimulationHelper.GetPcSwitches(simulationDTO.Pcs, simulationDTO.Switches, simulationDTO.Id);
            //simulationDTO.RouterSwitches = getSimulationHelper.GetRouterSwitches(simulationDTO.Routers, simulationDTO.Switches, simulationDTO.Id);
            return simulationDTO;
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
                        saveSimulationHelper.RemoveOldData(simulationDTO);
                    }
                    else
                    {
                        simulation = new Simulation();
                        simulation.DateAdd = DateTime.Now;
                        simulation.DateEdit = DateTime.Now;
                        context.SaveChanges();
                    }

                    saveSimulationHelper.AddOrUpdateSimulation(simulationDTO, simulation);

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

        public bool PingHost(string hostName)
        {
            try
            {
                Ping pingSender = new Ping();
                string data = "aaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 10000;
                PingOptions pingOptions = new PingOptions(64, true);
                PingReply pingReplay = pingSender.Send(hostName, timeout, buffer, pingOptions);

                if (pingReplay.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (PingException ex)
            {
                return false;
            }
        }

        #endregion

    }
}
