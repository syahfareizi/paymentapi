using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public PaymentDetailsController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetPaymentDetails()
        {
            var paymentdetail = await _context.paymentdetail.ToListAsync();
            return Ok(paymentdetail);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentDetail(PaymentDetailItem data)
        {
            if (ModelState.IsValid)
            {
                await _context.paymentdetail.AddAsync(data);
                await _context.SaveChangesAsync();
                return Ok(data);

            }
            else
            {
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            }


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentdetail.FirstOrDefaultAsync(x => x.id == id);
            if (paymentDetail == null)
            {
                return NotFound();

            }
            return Ok(paymentDetail);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentDetail(int id, PaymentDetailItem data)
        {
            if (id != data.id)
            {
                return BadRequest();
            }
            var selectedPaymentDetail = await _context.paymentdetail.FirstOrDefaultAsync(x => x.id == id);
            if (selectedPaymentDetail == null)
                return NotFound();

            selectedPaymentDetail.cardOwnerName = data.cardOwnerName;
            selectedPaymentDetail.cardNumber = data.cardNumber;
            selectedPaymentDetail.expirationDate = data.expirationDate;
            selectedPaymentDetail.securityCode = data.securityCode;

            await _context.SaveChangesAsync();
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var selectedPaymentDetail = await _context.paymentdetail.FirstOrDefaultAsync(x => x.id == id);
            if (selectedPaymentDetail == null)
                return NotFound();

            _context.paymentdetail.Remove(selectedPaymentDetail);
            await _context.SaveChangesAsync();
            return Ok(selectedPaymentDetail);
        }
    }
}
