using TeamFootballAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace TeamFootballAPI.Hubs
{
    public class TeamHub : Hub
    {
        //public async Task SendTeam(Team team)
        //{
        //    await Clients.All.SendAsync("ReceiveTeam", team);
        //}
    }
}
