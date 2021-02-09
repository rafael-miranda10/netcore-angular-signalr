using MaquininhaTheos.CrossCuting.Resources;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.TheosHub
{
    public class TheosMaquininhaHub : Hub
    {
        private readonly Random _random = new Random();
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("OnConnectedHub", Resource.HubConectado);
        }
        public async Task newExecuteValue(int limit)
        {
            var randomValue = _random.Next(0, 100);
            await Clients.Caller.SendAsync("newExecuteValue", randomValue);
        }

    }
}
