using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Simulation.Shared;

namespace Simulation.Objects
{
    public class AdminPanel
    {
        private INotificationQueue queue { get; set; }
        private HashSet<Truck> fleet { get; set; }
        private Canvas2DContext ctx { get; set; }

        public AdminPanel(Canvas2DContext _ctx, INotificationQueue _q, HashSet<Truck> _fleet)
        {
            ctx = _ctx;
            queue = _q;
            fleet = _fleet;
        }

        public async Task RenderGraphics()
        {
            await this.renderFleetState();
        }

        private async Task renderNotifications()
        {

        }

        /// <summary>
        /// Lets us see the current state of each car
        /// </summary>
        /// <returns></returns>
        private async Task renderFleetState()
        {
            int i = 1;
            foreach (var truck in this.fleet)
            {
                await this.ctx.SetFontAsync("12px Roboto");
                await this.ctx.SetFillStyleAsync("purple");
                await this.ctx.FillTextAsync($"TruckId: {i} | State: {truck.State} | Condition: {truck.Condition}", 50, 50 * i);
                i++;
            }
        }
    }
}
