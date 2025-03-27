using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Interface;
using PaymentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            var bills = await _billRepository.GetAllBillsAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }

        [HttpPost]
        public async Task<ActionResult<Bill>> CreateBill(Bill bill)
        {
            await _billRepository.CreateBillAsync(bill);
            return CreatedAtAction(nameof(GetBill), new { id = bill.BillId }, bill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, Bill bill)
        {
            if (id != bill.BillId)
            {
                return BadRequest();
            }

            await _billRepository.UpdateBillAsync(bill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            await _billRepository.DeleteBillAsync(id);
            return NoContent();
        }
    }

}
