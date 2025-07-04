using Microsoft.AspNetCore.Mvc;
using ProofOfReserveAPI.Services;

namespace ProofOfReserveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProofController : ControllerBase
    {
        private readonly IUserProofService _proofService;

        public ProofController(IUserProofService proofService)
        {
            _proofService = proofService;
        }

        /// <summary>
        /// Returns the Merkle Root hash for all current user balances.
        /// </summary>
        [HttpGet("root")]
        public ActionResult<string> GetMerkleRoot()
        {
            var root = _proofService.GetMerkleRootHex();
            return Ok(root);
        }

        /// <summary>
        /// Returns a Merkle Proof path for the given user ID.
        /// </summary>
        [HttpGet("{userId:int}")]
        public IActionResult GetProofForUser(int userId)
        {
            var result = _proofService.GetProofForUser(userId);
            return result is null ? NotFound($"User ID {userId} not found.") : Ok(result);
        }
    }
}
