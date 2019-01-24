using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Interfaces
{
    public interface ISimulationService
    {
        SimulationDTO SaveSimulation(SimulationDTO simulation);
        List<SimulationDTO> GetSimulationList();
        SimulationDTO GetSimulation(int simId);
    }
}
