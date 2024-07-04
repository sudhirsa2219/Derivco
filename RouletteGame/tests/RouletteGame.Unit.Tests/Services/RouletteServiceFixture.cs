using RouletteGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteGame.Unit.Tests.Services
{
    public class RouletteServiceFixture : IDisposable
    {
        public RouletteService Service { get; private set; }
        public RouletteServiceFixture()
        {
            Service = new RouletteService();
        }
        public void Dispose()
        {
            // Cleanup if needed
            Service = null;
        }
    }
}
