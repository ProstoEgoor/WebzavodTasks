using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_1._2._1.Services;

namespace Task_1._2._1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase {
        public IFileService FileService { get; }
        public FileController(IFileService fileService) {
            FileService = fileService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] int startRow, [FromQuery] int endRow) {
            string text;
            Exception e;
            if (startRow == 0 && endRow == 0) {
                (text, e) = await FileService.GetAsync();
            } else {
                (text, e) = await FileService.GetAsync(startRow, endRow);
            }

            if (text != null) {
                return Ok(text);
            } else {
                if (e is ArgumentOutOfRangeException) {
                    return NotFound(e.Message);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpGet("{row}")]
        public async Task<IActionResult> Get([FromRoute] int row) {
            (string text, Exception e) = await FileService.GetAsync(row);
            if (text != null) {
                return Ok(text);
            } else {
                if (e is ArgumentOutOfRangeException) {
                    return NotFound(e.Message);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] string text, [FromQuery] bool force) {
            (bool success, Exception e) = await FileService.PutAsync(text, force);
            if (success) {
                return Ok();
            } else {
                if (e is ArgumentException) {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, e.Message);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPut("{row}")]
        public async Task<IActionResult> Put([FromRoute] int row, [FromBody] string text) {
            (bool success, Exception e) = await FileService.PutAsync(text, row);
            if (success) {
                return Ok();
            } else {
                if (e is ArgumentOutOfRangeException) {
                    return NotFound(e.Message);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpDelete("{row}")]
        public async Task<IActionResult> Delete([FromRoute] int row) {
            (bool success, Exception e) = await FileService.DeleteAsync(row);
            if (success) {
                return Ok();
            } else {
                if (e is ArgumentOutOfRangeException) {
                    return NotFound(e.Message);
                } else {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }
    }
}
