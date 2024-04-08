using Microsoft.AspNetCore.Mvc;

namespace Piekļuves_darbs_2.Controllers
{
    [ApiController]
    [Route("[controller]")]//localhost:7278/User 
    public class UserController : ControllerBase //[controller] == User
    {
        private DatabaseManager _dbManager;

        public UserController(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        [HttpPost]
        [Route("api/users/addPoints")]
        public IActionResult AddPoints([FromBody] AddPointsRequest request)
        {
            _dbManager.AddPoints(request.UserId, request.Points);
            return Ok();
        }

        [HttpGet]
        [Route("api/users/getPoints")]
        public IActionResult GetPoints(string userId)
        {
            var points = _dbManager.GetPoints(userId);
            return Ok(points);
        }

        [HttpPost]
        [Route("api/users/spendPoints")]
        public IActionResult SpendPoints([FromBody] SpendPointsRequest request)
        {
            var success = _dbManager.SpendPoints(request.UserId, request.PointsToSpend);
            if (success)
                return Ok("Points spent successfully");
            else
                return BadRequest("Insufficient points");
        }
    }

    public class AddPointsRequest
    {
        public string UserId { get; set; }
        public int Points { get; set; }
    }

    public class SpendPointsRequest
    {
        public string UserId { get; set; }
        public int PointsToSpend { get; set; }
    }
}