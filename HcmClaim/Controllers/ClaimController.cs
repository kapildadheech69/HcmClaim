using AutoMapper;
using HcmClaim.Dto;
using HcmClaim.Modals;
using HcmClaim.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HcmClaim.Controllers
{
    [ApiController]
    [Route("HealthCare")]
    public class ClaimController:ControllerBase
    {
        private readonly IClaimRepo Repo;
        private readonly IMapper mapper;
        public ClaimController(IClaimRepo Repo, IMapper mapper)
        {
            this.Repo = Repo;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("SubmitClaim")]
        public ActionResult SubmitClaim(ClaimDto claimDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Claim claim = new Claim();
            claim = mapper.Map<Claim>(claimDto);
            claim.ClaimDate = DateTime.Now;

            Repo.AddClaim(claim);

            return Ok();
        }

        [HttpGet]
        [Route("GetClaims")]
        public ActionResult<Claim> getClaimsList()
        {
            var claims = Repo.GetClaims();
            if (claims.Count == 0)
                return NotFound("No claims in database");
            return Ok(claims);
        }
    }
}
