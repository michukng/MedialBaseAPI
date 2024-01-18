using MedicalBase.DTOS.Patient;
using MedicalBase.Models;
using MedicalBase.Services.PatientService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBase.Controllers
{
    [ApiController]
    [Route("api/Patients")]
    public class PatientController(IPatientService patientService) : ControllerBase
    {
        private readonly IPatientService _patientService = patientService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> GetPatients()
        {
            return Ok(await _patientService.GetAllPatients());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> GetPatient(int id)
        {
            var response = await _patientService.GetPatient(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{pesel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> GetPatientByPesel(string pesel)
        {
            var response = await _patientService.GetPatientByPesel(pesel);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> DeletePatient(int id)
        {
            var response = await _patientService.DeletePatient(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> AddPatient([FromBody] AddPatientDTO patient)
        {
            var response = await _patientService.AddPatient(patient);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<GetPatientDTO>>> UpdatePatient([FromBody] UpdatePatientDTO updatedPatient)
        {
            var response = await _patientService.UpdatePatient(updatedPatient);

            if (response.Message == "Patient not found.")
            {
                return NotFound(response);
            }

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}