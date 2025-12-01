using Microsoft.AspNetCore.Mvc;
using CartridgeWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CartridgeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartridgesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartridgesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCartridges()
        {
            try
            {
                var cartridges = _context.Cartridges.ToList();
                return Ok(cartridges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        //[Authorize]
        public IActionResult CreateCartridge([FromBody] Cartridge cartridge)
        {
            try
            {
                _context.Cartridges.Add(cartridge);
                _context.SaveChanges();
                return Ok(new { message = "Картридж добавлен!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Ошибка: " + ex.Message });
            }
        }
        [HttpPost("book/{id}")]
        public IActionResult BookCartridge(int id)
        {
            try
            {
                var cartridge = _context.Cartridges.Find(id);
                if (cartridge == null)
                    return NotFound(new { message = "Картридж не найден" });

                if (cartridge.AvailableQuantity > 0)
                {
                    cartridge.BookedQuantity++;
                    _context.SaveChanges();
                    return Ok(new { message = "Картридж успешно забронирован!" });
                }

                return BadRequest(new { message = "Нет свободных картриджей" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка сервера: " + ex.Message });
            }
        }

        [HttpPost("cancel/{id}")]
        public IActionResult CancelBooking(int id)
        {
            try
            {
                var cartridge = _context.Cartridges.Find(id);
                if (cartridge == null)
                    return NotFound(new { message = "Картридж не найден" });

                if (cartridge.BookedQuantity > 0)
                {
                    cartridge.BookedQuantity--;
                    _context.SaveChanges();
                    return Ok(new { message = "Бронирование отменено!" });
                }

                return BadRequest(new { message = "Нет забронированных картриджей" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка сервера: " + ex.Message });
            }
        }

        // Удаление картриджа
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteCartridge(int id)
        {
            try
            {
                var cartridge = _context.Cartridges.Find(id);
                if (cartridge == null)
                    return NotFound(new { message = "Картридж не найден" });

                _context.Cartridges.Remove(cartridge);
                _context.SaveChanges();
                return Ok(new { message = "Картридж удален!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка сервера: " + ex.Message });
            }
        }
    }
}