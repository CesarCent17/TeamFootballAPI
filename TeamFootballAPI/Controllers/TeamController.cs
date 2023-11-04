using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TeamFootballAPI.Hubs;
using TeamFootballAPI.Models;
using TeamFootballAPI.Models.Dto;
using TeamFootballAPI.Services;

namespace TeamFootballAPI.Controllers

{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;
        private readonly IHubContext<TeamHub> _hubContext;
        public TeamController(TeamService teamService, IHubContext<TeamHub> hubContext)
        {
            _teamService = teamService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public ActionResult<List<Team>> GetAllTeams()
        {
            return _teamService.Teams.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Team> GetTeamById(Guid id)
        {
            var team = _teamService.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(TeamCreateDto team)
        {
            var teamNew = new Team()
            {   
                Id = Guid.NewGuid(),
                Name = team.Name,
                City = team.City,
                YearFounded = team.YearFounded,
            };
            _teamService.Teams.Add(teamNew);
            await _hubContext.Clients.All.SendAsync("ReceiveTeam", teamNew);
            return CreatedAtAction(nameof(GetTeamById), new { id = teamNew.Id }, teamNew);
        }

    }
}
