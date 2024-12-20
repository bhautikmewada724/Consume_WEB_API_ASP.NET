using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Practice.Data;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        #region Configuration
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository) => _stateRepository = stateRepository;

        #endregion

        #region GetAllCountries
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var states = _stateRepository.SelectAllStates();
            return Ok(states);
        }
        #endregion

        #region GetStateById
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var state = _stateRepository.GetStateByID(id);
            if (state == null || !state.Any()) // Check if no data is returned
            {
                return NotFound(new { Message = "State not found." });
            }
            return Ok(state);
        }
        #endregion

        #region InsertState
        [HttpPost]
        public IActionResult InsertCountry([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }
            bool isInserted = _stateRepository.InsertState(state);

            if (isInserted)
            {
                return Ok(new { Message = "State Inserted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Inserting State");
        }
        #endregion

        #region UpdateState
        [HttpPut("{stateID}")]
        public IActionResult UpdateCountry(int stateID, [FromBody] StateModel state)
        {
            if (state == null || stateID != state.StateID)
            {
                return BadRequest();
            }
            var isUpdated = _stateRepository.UpdateState(state);

            if (isUpdated)
            {
                return Ok(new { Message = "State Updated Successfully" });
            }
            return NoContent();
        }
        #endregion

        #region DeleteState
        [HttpDelete]
        public IActionResult DeleteCountry(int stateID,StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }
            bool isDeleted = _stateRepository.DeleteState(stateID);

            if (isDeleted)
            {
                return Ok(new { Message = "State Deleted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Deleting Country");
        }
        #endregion

        
    }
}
